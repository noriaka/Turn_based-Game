using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameArchitecture : Architecture<GameArchitecture>
{
    protected override void Init()
    {
        RegisterModel(new ConstantModel());
        RegisterUtility(new CommonUtil());

        RegisterUtility(new ResUtil());
        
        RegisterModel(new PetModels());
        RegisterModel(new SkillModels());
        RegisterModel(new PlayerModel());
    }
}

public class GameController : MonoBehaviour, IController
{
    void Start()
    {
        BattleController.Instance.Init();
        //var pModels = this.GetModel<PetModels>().petModels;
        //Debug.Log(pModels[5277].Name);
        //var player = this.GetModel<PlayerModel>();
        //Debug.Log(player.Name);
        //var sModels = this.GetModel<SkillModels>().skillModels;
        //Debug.Log(sModels[1].Name);
    }
    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
