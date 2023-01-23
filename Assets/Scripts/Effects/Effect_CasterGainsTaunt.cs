using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Caster Gains Taunt", menuName = "Effect/CasterGainsTaunt")]
public class Effect_CasterGainsTaunt : CardEffect_ScriptableObj
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
        GameObject Caster;
        if( CardProperty.teamSide == GameReferences.TeamSide.Player)
        {
            Caster = CreatureSelection_p.currentSelectedCreature;
        }
        else // if TeamSide.Enemy
        {
            Caster = CreatureSelection_e.currentSelectedCreature;
        }

        Caster.GetComponent<CreatureProperty>().SetTaunt(true);
    }
}
