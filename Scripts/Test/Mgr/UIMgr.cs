using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UIMgr : BaseMgr<UIMgr>
{
    //public GameObject healthBarPrefab; // Ѫ��Ԥ�Ƽ�
    //public List<Transform> petTransforms = new List<Transform>(6);

    //private List<PetModel> pets = new List<PetModel>();
    //private List<HPController> hpControllers = new List<HPController>();

    //void Start()
    //{
    //    InitializeMonstersAndHealthBars();
    //}

    //private void InitializeMonstersAndHealthBars()
    //{
    //    // ���贴�� 6 �������ʾ��
    //    for (int i = 0; i < 6; i++)
    //    {
    //        // ������������
    //        PetModel pet = new PetModel("Pet " + (i + 1), 100 + i * 10);
    //        pets.Add(pet);

    //        // ʵ����Ѫ����ͼ
    //        GameObject hpBarObj = Instantiate(healthBarPrefab, petTransforms[i]);
    //        HPBarView healthBarView = hpBarObj.GetComponent<HPBarView>();

    //        // ��������ʼ�� HealthController
    //        HPController hpController = hpBarObj.AddComponent<HPController>();
    //        hpController.Initialize(pet, healthBarView);

    //        // ����������ӵ��б���
    //        hpControllers.Add(hpController);
    //    }
    //}
}
