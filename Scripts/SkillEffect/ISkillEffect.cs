using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillEffect
{
    public bool CheckCondition(int caster_id, List<int> targets_id);
    public void ApplyEffect(int caster_id, List<int> targets_id);
}
