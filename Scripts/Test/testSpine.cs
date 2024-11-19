using Spine.Unity;
using Spine.Unity.Prototyping;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class testSpine : MonoBehaviour
{
    public Button hurtButton;
    public Button attackButton;
    public Button buffButton;

    public GameObject spineAnimationObject; // ָ�� SpineAnimation ����
    public Button activateButton; // ָ��ť����

    public Dropdown selectPets;

    private int curPetIdx;
    private Animator curAnimator;
    private AnimatorController battleAni;
    private List<AnimatorController> aniControllers = new List<AnimatorController>();
    public List<Animator> animators;

    private Vector3 attackPos = Vector3.zero;
    private Vector3 originPos = Vector3.zero;
    public Transform attackTrans;

    void Start()
    {
        battleAni = Resources.Load<AnimatorController>("Common/battleController");
        aniControllers.Add(Resources.Load<AnimatorController>(getPath(5427)));
        aniControllers.Add(Resources.Load<AnimatorController>(getPath(5277)));
        aniControllers.Add(Resources.Load<AnimatorController>(getPath(5465)));
        aniControllers.Add(Resources.Load<AnimatorController>(getPath(5438)));
        aniControllers.Add(Resources.Load<AnimatorController>(getPath(5422)));
        aniControllers.Add(Resources.Load<AnimatorController>(getPath(5334)));

        curPetIdx = 0;
        curAnimator = animators[curPetIdx];

        if (curAnimator != null && battleAni != null)
        {
            curAnimator.runtimeAnimatorController = battleAni;
            attackPos = attackTrans.position;
            curAnimator.SetBool("is_hurt", false);
            curAnimator.SetBool("is_attack", false);
            curAnimator.SetBool("is_buff", false);

            // ���ð�ť�ĵ���¼�
            hurtButton.onClick.AddListener(TriggerHurt);
            attackButton.onClick.AddListener(TriggerAttack);
            activateButton.onClick.AddListener(ActivateAndPlayAnimation);
            buffButton.onClick.AddListener(TriggerBuff);
            selectPets.onValueChanged.AddListener(SelectPets);
        }
    }

    string getPath(int petID)
    {
        return $"SpineRes/aola_{petID}/pet{petID}/pet{petID}_Controller";
    }

    void ReplaceAnimation(AnimatorController aniController, int petID, int idx)
    {
        curAnimator.runtimeAnimatorController = aniControllers[curPetIdx];
        curPetIdx = idx;
        curAnimator = animators[curPetIdx];
        curAnimator.runtimeAnimatorController = battleAni;
        AnimationClip[] newClips = Test.GetAniClips(getPath(petID));

        for (int i = 0; i < newClips.Length; i++)
        {
            var states = aniController.layers[0].stateMachine.states;
            states[i].state.motion = newClips[i];
        }

        Debug.Log("�л��ɹ�");
    }

    void SelectPets(int idx)
    { 
        switch (idx)
        {
            case 0:
                ReplaceAnimation(battleAni, 5427, 0);
                break;
            case 1:
                ReplaceAnimation(battleAni, 5277, 1);
                break;
            case 2:
                ReplaceAnimation(battleAni, 5465, 2);
                break;
            case 3:
                ReplaceAnimation(battleAni, 5438, 3);
                break;
            case 4:
                ReplaceAnimation(battleAni, 5422, 4);
                break;
            case 5:
                ReplaceAnimation(battleAni, 5334, 5);
                break;
        }
    }

    void Update()
    {
        // ��鵱ǰ����״̬
        AnimatorStateInfo stateInfo = curAnimator.GetCurrentAnimatorStateInfo(0);

        // ������� hurt ״̬�����Ҷ���������ϣ����ò���
        if (stateInfo.IsName("1_2") && stateInfo.normalizedTime >= 0.8f)
        {
            ResetParameters();
        }

        // ������� attack ״̬�����Ҷ���������ϣ����ò���
        if (stateInfo.IsName("1_4") && stateInfo.normalizedTime >= 0.95f)
        {
            curAnimator.gameObject.transform.DOMove(originPos, 0.5f);
            ResetParameters();
        }

        if (stateInfo.IsName("1_6"))
        {
            ResetParameters();
        }
    }

    public void ResetParameters()
    {
        curAnimator.SetBool("is_hurt", false);
        curAnimator.SetBool("is_attack", false);
        curAnimator.SetBool("is_buff", false);
    }


    void TriggerHurt()
    {
        //UIMgr.Instance.TakeDamage();
        curAnimator.SetBool("is_hurt", true);
        curAnimator.SetBool("is_attack", false);
        curAnimator.SetBool("is_buff", false);
    }

    void TriggerAttack()
    {
        originPos = curAnimator.gameObject.transform.position;

        // �ƶ�������λ�ã�������ɺ�ִ�к����߼�
        curAnimator.gameObject.transform.DOMove(attackPos, 0.5f).OnComplete(() =>
        {
            curAnimator.SetBool("is_attack", true);
            curAnimator.SetBool("is_hurt", false);
            curAnimator.SetBool("is_buff", false);
        });
    }

    void TriggerBuff()
    {
        curAnimator.SetBool("is_buff", true);
        curAnimator.SetBool("is_attack", false);
        curAnimator.SetBool("is_hurt", false);
    }

    void ActivateAndPlayAnimation()
    {
        // ���� SpineAnimation ����
        curAnimator.gameObject.SetActive(false);
        spineAnimationObject.SetActive(true);

        // ��ȡ SpineAnimation ��������Ŷ���
        var spineAnimation = spineAnimationObject.GetComponent<SkeletonAnimation>();
        if (spineAnimation != null)
        {
            spineAnimation.AnimationState.SetAnimation(0, "0", false); // �滻 "yourAnimationName" Ϊ��Ҫ���ŵĶ�������
            StartCoroutine("CloseAni");
        }
    }

    IEnumerator CloseAni()
    {
        yield return new WaitForSeconds(4.0f);
        spineAnimationObject.SetActive(false);
        curAnimator.gameObject.SetActive(true);
    }
}
