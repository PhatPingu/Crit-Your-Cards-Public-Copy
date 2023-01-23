using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Caster Gain Retaliate", menuName = "Effect/CasterGainRetaliate")]
public class Effect_CasterGainRetaliate : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b><u>Retaliate</u></b>\nWhen target of an <b>Attack</b>, creature <b>Attacks</b> back";
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

        Caster.GetComponent<CreatureBuffDebuffManager>().AddTickToBuffDebuff(retaliate: 1);
        Caster.GetComponent<CreatureBuffDisplay>().ShowRetaliate(true);
    }
}