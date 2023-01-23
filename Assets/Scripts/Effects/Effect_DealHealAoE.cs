using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal Heal AoE", menuName = "Effect/DealHeal_aoe")]
public class Effect_DealHealAoE : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b><u>Heal</u></b>\nHealing done is equal to caster'a Healing Power";
    }
    
    public override void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerList, GameObject[] enemyList)
    {
        var CreatureProperty = target.GetComponent<CreatureProperty>();
        
        if(overrideTarget)
        {
            if(CreatureProperty.TeamSide == GameReferences.TeamSide.Player)
            {
                CreatureProperty = CreatureSelection_p.currentSelectedCreature.GetComponent<CreatureProperty>();
            }
        }

        if(CreatureProperty.TeamSide == GameReferences.TeamSide.Player)
        {
            var i_creature = CreatureSelection_p.p_aliveCreatures;
            for (int i = 0; i < CreatureSelection_p.p_aliveCreatures.Count; i++)
            {
                if(creatureHeal < 0) creatureHeal = 0;
                playerList[i_creature[i]].GetComponent<CreatureProperty>().currentCreatureHealth += creatureHeal;
            }
        }
        else
        {
            var i_creature = CreatureSelection_e.e_aliveCreatures;
            for (int i = 0; i < CreatureSelection_e.e_aliveCreatures.Count; i++)
            {
                if(creatureHeal < 0) creatureHeal = 0;
                enemyList[i_creature[i]].GetComponent<CreatureProperty>().currentCreatureHealth += creatureHeal;
            }
        }
    }
}
