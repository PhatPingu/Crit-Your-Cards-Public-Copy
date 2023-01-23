using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decrease Attack", menuName = "Effect/DecreaseAttack")]
public class Effect_DecreaseAttack : CardEffect_ScriptableObj
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
        if (target.GetComponent<CreatureProperty>().currentCreatureAttack - modifierAmount >= 0)
        {
            target.GetComponent<CreatureProperty>().currentCreatureAttack -= modifierAmount;
            target.GetComponent<CreatureDmgDisplay>().attack_text.text = creatureAttack.ToString();
        }

        
        var Caster = CreatureSelection_p.currentSelectedCreature;
        if(CardProperty.teamSide == GameReferences.TeamSide.Enemy)
        {
            Caster = CreatureSelection_e.currentSelectedCreature;
        }

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