using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal Damage", menuName = "Effect/DealDamaga")]
public class Effect_DealSpellDamage : CardEffect_ScriptableObj
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
        target.GetComponent<CreatureProperty>().currentCreatureHealth -= modifierAmount;
        
        target.GetComponent<CreatureDmgDisplay>().spellDamage_text.text = modifierAmount.ToString();
        target.GetComponent<CreatureDmgDisplay>().DisplaySpellDamage(CardProperty.cardColor);

        
        var Caster = CreatureSelection_p.currentSelectedCreature;
        if(CardProperty.teamSide == GameReferences.TeamSide.Enemy)
        {
            Caster = CreatureSelection_e.currentSelectedCreature;
        }

        Caster.GetComponent<CreatureAnimation>().Spell_Animation(true, target);
    }
}