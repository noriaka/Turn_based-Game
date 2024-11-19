using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HpController : MonoBehaviour, IController
{
    private HpPanelView hpPanelView;
    private int bindPid;
    private int curHp;
    private int maxHp;
    private PetModel petModel;

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
    public event Action<int> OnHpChanged;
    public void Init(int pid)
    {
        bindPid = pid;
        petModel = this.GetModel<PetModels>().petModels[bindPid];
        hpPanelView = GetComponent<HpPanelView>();
        curHp = petModel.Hp;
        maxHp = curHp;
        hpPanelView.Init(curHp, maxHp, petModel.Element);

        petModel.OnHpChanged += UpdateInfo;
    }

    public void UpdateInfo(int curHp)
    {
        hpPanelView.UpdateInfo(curHp, maxHp);
    }

    private void OnDestroy()
    {
        if (petModel != null)
        {
            petModel.OnHpChanged -= UpdateInfo;
        }
    }

    //private BaseComponent baseComponent; // ��ǰ���Ƶľ�������ģ��
    //private HPBarView healthBarView; // ��ǰ���Ƶ�Ѫ����ͼ

    //public void Initialize(PetModel model, HPBarView view)
    //{
    //    petModel = model;
    //    healthBarView = view;

    //    // ���� HP �仯�¼�
    //    petModel.OnHPChanged += UpdateHealthBar;

    //    // ��ʼ��Ѫ����ͼ
    //    UpdateHealthBar(petModel.HP, petModel.MaxHP);
    //}

    //private void UpdateHealthBar(int currentHP, int maxHP)
    //{
    //    healthBarView.SetHealth(currentHP, maxHP);
    //}

    //private void OnDestroy()
    //{
    //    // �Ƴ��¼������������ڴ�й©
    //    if (petModel != null)
    //        petModel.OnHPChanged -= UpdateHealthBar;
    //}
}
