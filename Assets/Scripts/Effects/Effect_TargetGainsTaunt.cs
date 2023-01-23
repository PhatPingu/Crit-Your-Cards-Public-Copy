using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Gains Taunt", menuName = "Effect/TargetGainsTaunt")]
public class Effect_TargetGainsTaunt : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b><u>Taunt</u></b>\nOther allies can not be targeted unless they also have <b>Taunt</b>.";
    }
    
    public override void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerList, GameObject[] enemyList)
    {
        target.GetComponent<CreatureProperty>().SetTaunt(true);
    }
}
