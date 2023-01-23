using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Discards", menuName = "Effect/TargetDiscards")]
public class Effect_TargetDiscards : CardEffect_ScriptableObj
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
        for (int i = 0; i < modifierAmount; i++)
        {
            if(target.GetComponent<CreatureProperty>().TeamSide == GameReferences.TeamSide.Enemy)
            {
                var EnemyHand = target.GetComponent<CreatureDeckInfo>().enemyHand_list;
                var EnemyGrave = target.GetComponent<CreatureDeckInfo>().enemyGrave_list;
                var DiscardDisplay = target.GetComponent<CreatureDeckInfo>().e_DiscardDisplay;
                if(EnemyHand.Count < 1) return;

                int i_Random = Random.Range(0,EnemyHand.Count);
                DiscardDisplay.GetComponent<CardProperty>().cardData = EnemyHand[i_Random];
                DiscardDisplay.GetComponent<DisplayCardCast>().SendToGrave_Animation();
                
                EnemyGrave.Add(EnemyHand[i_Random]);
                EnemyHand.Remove(EnemyHand[i_Random]);
            }
            else
            {
                var PlayerHand = target.GetComponent<CreatureDeckInfo>().playerHand_list;
                var PlayerGrave = target.GetComponent<CreatureDeckInfo>().playerGrave_list;
                var DiscardDisplay = target.GetComponent<CreatureDeckInfo>().p_DiscardDisplay;
                if(PlayerHand.Count < 1) return;

                int i_Random = Random.Range(0,PlayerHand.Count);
                DiscardDisplay.GetComponent<CardProperty>().cardData = PlayerHand[i_Random];
                DiscardDisplay.GetComponent<DisplayCardCast>().SendToGrave_Animation();
                
                PlayerGrave.Add(PlayerHand[i_Random]);
                PlayerHand.Remove(PlayerHand[i_Random]);
            }
        }
    }
}
