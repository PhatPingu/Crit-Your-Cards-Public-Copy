using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Caster Gains HP", menuName = "Effect/CasterGainsHP")]
public class Effect_CasterGainsHP : CardEffect_ScriptableObj
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

        Caster.GetComponent<CreatureProperty>().currentCreatureHealth += modifierAmount;

        Caster.GetComponent<CreatureAnimation>().Heal_Animation(true, target);
    }
}
