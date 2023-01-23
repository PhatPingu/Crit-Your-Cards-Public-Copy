using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Apply Sleep to Target", menuName = "Effect/ApplySleepToTarget")]
public class Effect_ApplySleepToTarget : CardEffect_ScriptableObj
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
        target.GetComponent<CreatureBuffDebuffManager>().SetBuffDebuffTick(sleep: 1);
        target.GetComponent<CreatureBuffDisplay>().ShowSleep(true);
        
        if(target.GetComponent<CreatureProperty>().TeamSide == GameReferences.TeamSide.Player)
        {
            CreatureSelection_p.UpdateSleepCreatureList();
            CreatureSelection_p.UpdateAvailabilityDisplay();
        }
        else
        {
            CreatureSelection_e.UpdateSleepCreatureList();
            CreatureSelection_e.UpdateAvailabilityDisplay();
        }
        
        
    }
}
