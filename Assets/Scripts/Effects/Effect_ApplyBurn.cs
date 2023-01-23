using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Apply Burn", menuName = "Effect/ApplyBurn")]
public class Effect_ApplyBurn : CardEffect_ScriptableObj
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
        target.GetComponent<CreatureBuffDebuffManager>().AddTickToBuffDebuff(burn: 1);
        target.GetComponent<CreatureBuffDebuffManager>().burnJustApplied = true;
        target.GetComponent<CreatureBuffDisplay>().ShowBurn(true);
    }
}
