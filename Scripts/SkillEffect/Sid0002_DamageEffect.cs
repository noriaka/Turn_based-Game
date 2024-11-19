using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sid0002_DamageEffect : ISkillEffect
{
    private int damageVal;
    private BattleController instance;
    public void ApplyEffect(int caster_id, List<int> targets_id)
    {
        foreach (var target_id in targets_id)
        {
            instance.GetModel<PetModels>().petModels[target_id].Hp -= damageVal;
        }
    }

    public bool CheckCondition(int caster_id, List<int> targets_id)
    {
        if (targets_id.Count != 0) 
            return true;
        else return false;
    }
}
