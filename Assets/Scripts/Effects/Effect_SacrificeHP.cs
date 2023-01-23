using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sacrifice HP", menuName = "Effect/SacrificeHP")]
public class Effect_SacrificeHP : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return base.Tooltip();
    }

    public override void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerList, GameObject[] enemyList)
    {
        var Caster = CreatureSelection_p.currentSelectedCreature;
        if(CardProperty.teamSide == GameReferences.TeamSide.Enemy)
        {
            Caster = CreatureSelection_e.currentSelectedCreature;
        }

        Caster.GetComponent<CreatureProperty>().currentCreatureHealth -= modifierAmount;
        
        Caster.GetComponent<CreatureDmgDisplay>().spellDamage_text.text = modifierAmount.ToString();
        Caster.GetComponent<CreatureDmgDisplay>().DisplaySpellDamage(CardProperty.cardColor);

        Caster.GetComponent<CreatureAnimation>().Spell_Animation(true, target);
    }
}