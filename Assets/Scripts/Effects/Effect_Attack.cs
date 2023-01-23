using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Effect/Attack")]
public class Effect_Attack : CardEffect_ScriptableObj
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
        var finalDamageDealt = creatureAttack - target.GetComponent<CreatureProperty>().currentCreatureDefense;

        if(finalDamageDealt < 0) finalDamageDealt = 0;
    
        target.GetComponent<CreatureProperty>().currentCreatureHealth -= finalDamageDealt;
        
        target.GetComponent<CreatureDmgDisplay>().attack_text.text = creatureAttack.ToString();
        target.GetComponent<CreatureDmgDisplay>().damage_text.text = finalDamageDealt.ToString();
        target.GetComponent<CreatureDmgDisplay>().DisplayAttackAndDefense(true);

        
        var Caster = CreatureSelection_p.currentSelectedCreature;
        if(CardProperty.teamSide == GameReferences.TeamSide.Enemy)
        {
            Caster = CreatureSelection_e.currentSelectedCreature;
        }

        Caster.GetComponent<CreatureAnimation>().Attack_Animation(true, target);
    }
}
