using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hide Allies", menuName = "Effect/HideAllies")]
public class Effect_HideAllies : CardEffect_ScriptableObj
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
        if(CardProperty.teamSide == GameReferences.TeamSide.Player)
        {
            var i_creature = CreatureSelection_p.p_aliveCreatures;
            for (int i = 0; i < CreatureSelection_p.p_aliveCreatures.Count; i++)
            {
                playerList[i_creature[i]].GetComponent<CreatureProperty>().SetHidden(true);
            }
        }
        else // if TeamSide.Enemy
        {
            var i_creature = CreatureSelection_e.e_aliveCreatures;
            for (int i = 0; i < CreatureSelection_e.e_aliveCreatures.Count; i++)
            {
                enemyList[i_creature[i]].GetComponent<CreatureProperty>().SetHidden(true);
            }
        }
    }
}
