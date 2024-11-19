using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetModel : ICanGetUtility
{
    private int pid;
    private string name;
    private int hp;
    private int phyAtk;
    private int phyDef;
    private int magAtk;
    private int magDef;
    private int speed;
    private List<int> skills;
    private ConstantModel.ElementType element;
    private string petAniPath;
    private string petBattleAniPath;

    public event Action<int> OnHpChanged;

    public int PID { get => pid; set => pid = value; }
    public string Name { get => name; set => name = value; }
    public int Hp
    {
        get => hp; set
        {
            if (hp != value)
            {
                hp = Math.Clamp(value, 0, int.MaxValue);
                OnHpChanged?.Invoke(hp);
            }
        }
    }
    public int PhyAtk { get => phyAtk; set => phyAtk = value; }
    public int PhyDef { get => phyDef; set => phyDef = value; }
    public int MagAtk { get => magAtk; set => magAtk = value; }
    public int MagDef { get => magDef; set => magDef = value; }
    public int Speed { get =>  speed; set => speed = value; }
    public List<int> Skills { get => skills; set => skills = value; }
    public string PetAniPath { get => petAniPath; set => petAniPath = value; }
    public string PetBattleAniPath { get => petBattleAniPath; set => petBattleAniPath = value; }
    public ConstantModel.ElementType Element { get => element; set => element = value; }

    public PetModel(int pid)
    {
        PID = pid;
        OnInit();
    }

    private void OnInit()
    {
        this.GetUtility<ResUtil>().LoadPetConfig(PID, this);
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}


public class PetModels : AbstractModel, ICanGetModel
{
    public Dictionary<int, PetModel> petModels;
    protected override void OnInit()
    {
        petModels = this.GetUtility<ResUtil>().LoadPetModels();
    }
}