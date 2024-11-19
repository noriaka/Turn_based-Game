using QFramework;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine;

public class BattleController : MonoSingleton<BattleController>, IController
{
    private BattlePanelView battlePanelView;
    private CommonUtil commonUtil;
    private ResUtil resUtil;
    private ConstantModel model;

    public Dictionary<int, GameObject> curBattlePetsPrefab;
    public Dictionary<int, SkillModel> skillModels;
    public Dictionary<int, List<GameObject>> skillPanelDic;
    public Dictionary<int, int> curPidPosDic;

    public Queue<int> pidQue;
    public int curPid;
    public SkeletonGraphic petBattleAni;
    public Vector2 prePos;

    public int playerBattleEnergy;
    public int enemyBattleEnergy;
    public ConstantModel.PlayerType curPlayerType;

    public int CurPid
    {
        get => curPid; set
        {
            if (curPid != value)
            {
                curPid = value;
                OnCurPidValueChanged?.Invoke();
            }
        }
    }

    public event Action OnCurPidValueChanged;

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }

    public void Init()
    {
        model = GameArchitecture.Interface.GetModel<ConstantModel>();
        resUtil = this.GetUtility<ResUtil>();
        commonUtil = this.GetUtility<CommonUtil>();
        battlePanelView = Instance.GetComponent<BattlePanelView>();
        pidQue = new Queue<int>(6);
        skillModels = this.GetModel<SkillModels>().skillModels;
        curBattlePetsPrefab = new Dictionary<int, GameObject>();
        skillPanelDic = new Dictionary<int, List<GameObject>>();
        curPidPosDic = new Dictionary<int, int>();
 
        playerBattleEnergy = 50;
        enemyBattleEnergy = 50;

        OnCurPidValueChanged += UpdateSkillBtnPanel;
        
        EnterBattle();
    }

    private void UpdateSkillBtnPanel()
    {
        if (skillPanelDic != null)
        {
            foreach (var sDic in skillPanelDic)
            {
                if (sDic.Key != CurPid)
                {
                    foreach (var sPanel in sDic.Value)
                    {
                        sPanel.gameObject.SetActive(false);
                    }
                } else
                {
                    foreach (var sPanel in sDic.Value)
                    {
                        sPanel.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void GetNextPid()
    {
        if (pidQue.Count > 0)
        {
            CurPid = pidQue.Dequeue();
            pidQue.Enqueue(CurPid);
        }
    }

    public void TestBattle()
    {
        petBattleAni = curBattlePetsPrefab[CurPid].GetComponent<SkeletonGraphic>();
        prePos = petBattleAni.GetComponent<RectTransform>().localPosition;
        petBattleAni.rectTransform.DOLocalMove(model.BattlePosOffsetDic[curPidPosDic[CurPid]], 0.5f).OnComplete(() =>
        {
            petBattleAni.AnimationState.SetAnimation(0, "1_4", false);
            petBattleAni.AnimationState.Complete += TestAttackComplete;
        });
    }

    public void TestAttackComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "1_4")
        {
            petBattleAni.AnimationState.SetAnimation(0, "1_1", true);

            petBattleAni.AnimationState.Complete -= TestAttackComplete;
            petBattleAni.rectTransform.DOLocalMove(prePos, 0.5f).OnComplete(() =>
            {
                GetNextPid();
            });
        }
    }

    public void EnterBattle()
    {
        foreach (var pet in this.GetModel<PlayerModel>().CurBattlePets)
        {
            int idx = pet.Key - 1;
            int pid = pet.Value;
            curPidPosDic.Add(pid, idx);
            GameObject petPrefab = resUtil.InitPetBattleAniObj(pid);
            petPrefab = Instantiate(petPrefab, battlePanelView.battlePos[idx]);
            petPrefab.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            petPrefab.GetComponent<RectTransform>().localPosition = model.PosFieldOffset;
            curBattlePetsPrefab.Add(pid, petPrefab);

            PetModel petModel = this.GetModel<PetModels>().petModels[pid];

            GameObject hpPanel = resUtil.InitPrefab(model.HpPanelPath);
            hpPanel = Instantiate(hpPanel, petPrefab.transform);
            hpPanel.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            hpPanel.GetComponent<RectTransform>().localPosition = model.HpOffset;
            hpPanel.GetComponent<HpController>().Init(pid);

            // Init skill btns
            int i = 0;
            List<GameObject> skillPanels = new();
            foreach (var sid in petModel.Skills)
            {
                GameObject btnSkillPanel = resUtil.InitPrefab(model.SkillPanelPath);
                if (btnSkillPanel != null)
                {
                    btnSkillPanel = Instantiate(btnSkillPanel, battlePanelView.skillBtnPos[i]);
                    btnSkillPanel.GetComponent<SkillPanelController>().Init(sid);
                    skillPanels.Add(btnSkillPanel);

                    // Test
                    Button btnSkill = btnSkillPanel.GetComponent<Button>();
                    btnSkill.onClick.AddListener(() =>
                    {
                        TestBattle();
                    });

                    i++;
                }
            }
            skillPanelDic.Add(pid, skillPanels);
            pidQue.Enqueue(pid);
        }
        CurPid = pidQue.Dequeue();
        pidQue.Enqueue(CurPid);
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    foreach (var pet in curBattlePets)
        //    {
        //        int pid = pet.Value;
        //        PetModel petModel = this.GetModel<PetModels>().petModels[pid];
        //        petModel.Hp -= 100;
        //    }
        //}
    }

    protected override void OnDestroy()
    {
        OnCurPidValueChanged -= UpdateSkillBtnPanel;
    }
}
