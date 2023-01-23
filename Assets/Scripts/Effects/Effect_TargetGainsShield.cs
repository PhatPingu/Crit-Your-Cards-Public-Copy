using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Gains Shield", menuName = "Effect/TargetGainsShield")]
public class Effect_TargetGainsShield : CardEffect_ScriptableObj
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
        target.GetComponent<CreatureProperty>().SetShielded(true);
    }
}
