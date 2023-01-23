using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Caster Gains Shield", menuName = "Effect/CasterGainsShield")]
public class Effect_CasterGainsShield : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b><u>Shield</u></b>\nProtects creature from incoming damage.\nAfter protecting once, it's consumed.";
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

        Caster.GetComponent<CreatureProperty>().SetShielded(true);
    }
}
