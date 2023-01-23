using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal Heal", menuName = "Effect/DealHeal")]
public class Effect_DealHeal : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b>Heal</b>\nHealing done is equal to caster'a Healing Power";
    }

    public override void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerList, GameObject[] enemyList)
    {
        if(creatureHeal < 0) creatureHeal = 0;
        target.GetComponent<CreatureProperty>().currentCreatureHealth += creatureHeal;


        var Caster = CreatureSelection_p.currentSelectedCreature;
        if(CardProperty.teamSide == GameReferences.TeamSide.Enemy)
        {
            Caster = CreatureSelection_e.currentSelectedCreature;
        }

        Caster.GetComponent<CreatureAnimation>().Heal_Animation(true, target);
    }
}
