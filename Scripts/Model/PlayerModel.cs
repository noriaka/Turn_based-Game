using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : AbstractModel, ICanGetModel
{
    private int uid;
    private string name;
    private List<int> ownedPets;
    private Dictionary<int, int> curBattlePets;

    public int Uid { get => uid; set => uid = value; }
    public string Name { get => name; set => name = value; }
    public List<int> OwnedPets { get => ownedPets; set => ownedPets = value; }
    public Dictionary<int, int> CurBattlePets { get => curBattlePets; set => curBattlePets = value; }


    protected override void OnInit()
    {
        this.GetUtility<ResUtil>().LoadPlayerConfig(this);
    }
}
