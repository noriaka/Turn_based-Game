using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sid0001_CostEnergyEffect : ISkillEffect
{
    private int cost;
    private BattleController instance;
    public Sid0001_CostEnergyEffect(int cost)
    {
        this.cost = cost;
        instance = BattleController.Instance;
    }

    public void ApplyEffect(int caster_id, List<int> targets_id)
    {
        if (instance != null)
        {
            if (instance.curPlayerType == ConstantModel.PlayerType.player)
                instance.playerBattleEnergy -= cost;
            else
                instance.enemyBattleEnergy -= cost;
        }
    }

    public bool CheckCondition(int caster_id, List<int> targets_id)
    {
        if (instance != null)
        {
            if (instance.curPlayerType == ConstantModel.PlayerType.player)
            {
                if (cost <= instance.playerBattleEnergy)
                    return true;
                else
                {
                    Debug.Log("能量不足！");
                    return false;
                }
                
            }
            else
            {
                if (cost <= instance.enemyBattleEnergy)
                    return true;
                else
                {
                    Debug.Log("能量不足！");
                    return false;
                }
            }
        }
        return false;
    }
}
