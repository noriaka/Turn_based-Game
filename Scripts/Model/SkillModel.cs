using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModel : ICanGetUtility
{
    private int sid;
    private string name;
    private string tag;
    private int power;
    private ConstantModel.ElementType element;
    private string target;
    private int targetNum;
    private int cost;
    private string desc;

    public int Sid { get => sid; set => sid = value; }
    public string Name { get => name; set => name = value; }
    public string Tag { get => tag; set => tag = value; }
    public int Power { get => power; set => power = value; }
    public ConstantModel.ElementType Element { get => element; set => element = value; }
    public string Target { get => target; set => target = value; }
    public int TargetNum { get => targetNum; set => targetNum = value; }
    public int Cost { get => cost; set => cost = value; }
    public string Desc { get => desc; set => desc = value; }

    public SkillModel(int sid)
    {
        Sid = sid;
        OnInit();
    }

    private void OnInit()
    {
        this.GetUtility<ResUtil>().LoadSkillConfig(Sid, this);
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}

public class SkillModels : AbstractModel, ICanGetModel
{
    public Dictionary<int, SkillModel> skillModels;
    protected override void OnInit()
    {
        skillModels = this.GetUtility<ResUtil>().LoadSkillModels();
    }
}
