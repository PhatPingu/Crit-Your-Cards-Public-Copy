using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Caster Gain Mana", menuName = "Effect/CasterGainMana")]
public class Effect_CasterGainsMana : CardEffect_ScriptableObj
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
        if( CardProperty.teamSide == GameReferences.TeamSide.Player)
        {
            var i_GameManager = GameObject.Find("Game Manager");
            i_GameManager.GetComponent<ManaManager>().GainMana_Player(modifierAmount);
        }
        else
        {
            var i_GameManager = GameObject.Find("Game Manager");
            i_GameManager.GetComponent<ManaManager>().GainMana_Enemy(modifierAmount);
        }
    }
}