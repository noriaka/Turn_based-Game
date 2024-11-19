using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UIMgr : BaseMgr<UIMgr>
{
    //public GameObject healthBarPrefab; // 血条预制件
    //public List<Transform> petTransforms = new List<Transform>(6);

    //private List<PetModel> pets = new List<PetModel>();
    //private List<HPController> hpControllers = new List<HPController>();

    //void Start()
    //{
    //    InitializeMonstersAndHealthBars();
    //}

    //private void InitializeMonstersAndHealthBars()
    //{
    //    // 假设创建 6 个精灵的示例
    //    for (int i = 0; i < 6; i++)
    //    {
    //        // 创建精灵数据
    //        PetModel pet = new PetModel("Pet " + (i + 1), 100 + i * 10);
    //        pets.Add(pet);

    //        // 实例化血条视图
    //        GameObject hpBarObj = Instantiate(healthBarPrefab, petTransforms[i]);
    //        HPBarView healthBarView = hpBarObj.GetComponent<HPBarView>();

    //        // 创建并初始化 HealthController
    //        HPController hpController = hpBarObj.AddComponent<HPController>();
    //        hpController.Initialize(pet, healthBarView);

    //        // 将控制器添加到列表中
    //        hpControllers.Add(hpController);
    //    }
    //}
}
