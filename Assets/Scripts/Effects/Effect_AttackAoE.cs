using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack All Enemies", menuName = "Effect/Attack_aoe")]
public class Effect_AttackAoE : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b><u>Attack</u></b>\nDamage done is equal to caster'a Attack Power minus target's Defense Power";
    }
    
    public override void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerList, GameObject[] enemyList)
    {
        int totalDamageDealt = creatureAttack;
        
        var TargetCreatureProperty = target.GetComponent<CreatureProperty>();
        if(TargetCreatureProperty.TeamSide == GameReferences.TeamSide.Player)
        {
            var i_creature = CreatureSelection_p.p_aliveCreatures;
            for (int i = 0; i < CreatureSelection_p.p_aliveCreatures.Count; i++)
            {
                var i_target = playerList[i_creature[i]];
                var finalDamageDealt = totalDamageDealt - i_target.GetComponent<CreatureProperty>().currentCreatureDefense;
                if(finalDamageDealt < 1) finalDamageDealt = 1;
                
                i_target.GetComponent<CreatureDmgDisplay>().attack_text.text = totalDamageDealt.ToString();
                i_target.GetComponent<CreatureDmgDisplay>().damage_text.text = finalDamageDealt.ToString();
                i_target.GetComponent<CreatureDmgDisplay>().DisplayAttackAndDefense(true);

                i_target.GetComponent<CreatureProperty>().currentCreatureHealth -= finalDamageDealt;
            }
        }
        else // if target TeamSide.Enemy
        {
            var i_creature = CreatureSelection_e.e_aliveCreatures;
            for (int i = 0; i < CreatureSelection_e.e_aliveCreatures.Count; i++)
            {
                var i_target = enemyList[i_creature[i]];
                var finalDamageDealt = totalDamageDealt - i_target.GetComponent<CreatureProperty>().currentCreatureDefense;
                if(finalDamageDealt < 1) finalDamageDealt = 1;

                i_target.GetComponent<CreatureDmgDisplay>().attack_text.text = totalDamageDealt.ToString();
                i_target.GetComponent<CreatureDmgDisplay>().damage_text.text = finalDamageDealt.ToString();
                i_target.GetComponent<CreatureDmgDisplay>().DisplayAttackAndDefense(true);

                i_target.GetComponent<CreatureProperty>().currentCreatureHealth -= finalDamageDealt;
            }
        }

        var Caster = CreatureSelection_p.currentSelectedCreature;
        if(CardProperty.teamSide == GameReferences.TeamSide.Enemy)
        {
            Caster = CreatureSelection_e.currentSelectedCreature;
        }

        Caster.GetComponent<CreatureAnimation>().Attack_Animation(true, target);
        
    }
}
