using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelController : MonoBehaviour, IController
{
    private SkillPanelView skillPanelView;
    private int bindSid;
    private SkillModel skillModel;
    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }

    public void Init(int sid)
    {
        bindSid = sid;
        skillModel = this.GetModel<SkillModels>().skillModels[bindSid];
        skillPanelView = GetComponent<SkillPanelView>();

        skillPanelView.Init(skillModel.Element, skillModel.Name, skillModel.Power,
                            skillModel.Cost, skillModel.Desc);
        //skillPanelView.btnSkill.onClick.AddListener()
    }
}
