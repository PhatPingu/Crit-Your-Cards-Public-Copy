using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal Damage AoE", menuName = "Effect/DealDamageAoE")]
public class Effect_DealSpellDamageAoE : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b><u>Spell Damage</u></b>\n<b>Spell Damage</b> ignores the target's Defense Power";
    }
    
    public override void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerList, GameObject[] enemyList)
    {
        var TargetCreatureProperty = target.GetComponent<CreatureProperty>();
        if(TargetCreatureProperty.TeamSide == GameReferences.TeamSide.Player)
        {
            var i_creature = CreatureSelection_p.p_aliveCreatures;
            for (int i = 0; i < CreatureSelection_p.p_aliveCreatures.Count; i++)
            {
                var i_target = playerList[i_creature[i]];
                
                target.GetComponent<CreatureDmgDisplay>().spellDamage_text.text = modifierAmount.ToString();
                target.GetComponent<CreatureDmgDisplay>().DisplaySpellDamage(CardProperty.cardColor);

                i_target.GetComponent<CreatureProperty>().currentCreatureHealth -= modifierAmount;
            }
        }
        else
        {
            var i_creature = CreatureSelection_e.e_aliveCreatures;
            for (int i = 0; i < CreatureSelection_e.e_aliveCreatures.Count; i++)
            {
                var i_target = enemyList[i_creature[i]];

                target.GetComponent<CreatureDmgDisplay>().spellDamage_text.text = modifierAmount.ToString();
                target.GetComponent<CreatureDmgDisplay>().DisplaySpellDamage(CardProperty.cardColor);

                i_target.GetComponent<CreatureProperty>().currentCreatureHealth -= modifierAmount;
            }
        }
        
    }
}