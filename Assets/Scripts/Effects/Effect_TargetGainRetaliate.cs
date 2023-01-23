using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Gain Retaliate", menuName = "Effect/TargetGainRetaliate")]
public class Effect_TargetGainRetaliate : CardEffect_ScriptableObj
{
    public override string Tooltip()
    {
        return "<b><u>Retaliate</u></b>\nWhen target of an <b>Attack</b>, creature <b>Attacks</b> back";
    }
    
    public override void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerList, GameObject[] enemyList)
    {
        target.GetComponent<CreatureBuffDebuffManager>().AddTickToBuffDebuff(retaliate: 1);
        target.GetComponent<CreatureBuffDisplay>().ShowRetaliate(true);
    }
}