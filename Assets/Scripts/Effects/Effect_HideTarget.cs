using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hide Target", menuName = "Effect/HideTarget")]
public class Effect_HideTarget : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b><u>Hide</u></b>\nHidden creatures can not be targeted.\nHidden creatures stay hidden util they perform an action.";
    }
    
    public override void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerList, GameObject[] enemyList)
    {
        target.GetComponent<CreatureProperty>().SetHidden(true);
    }
}
