using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Increase Caster Attack", menuName = "Effect/IncreaseCasterAttack")]
public class Effect_IncreaseCasterAttack : CardEffect_ScriptableObj
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

        Caster.GetComponent<CreatureProperty>().currentCreatureAttack += modifierAmount;
        
        Caster.GetComponent<CreatureDmgDisplay>().attack_text.text = creatureAttack.ToString();

        

        if (CardProperty.allowedTarget == GameReferences.AllowedTarget.Self)
        {
            Caster.GetComponent<CreatureAnimation>().Spell_Animation(true, target);
        }
        else
        {
            Caster.GetComponent<CreatureAnimation>().Heal_Animation(true, target);
        }
    }
}