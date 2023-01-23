using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureTargetingManagement : MonoBehaviour
{
    [SerializeField] CreatureSelection_e CreatureSelection_e;
    [SerializeField] CreatureSelection_p CreatureSelection_p;

    
    List<int> TargetableEnemyCreatures()  // Right now, this accounts for Taunt. Use IfTauntPresent_isTargetable() to return check.
    {
        var e_creatureList =    CreatureSelection_e.e_creatureList;
        var e_aliveCreatures =  CreatureSelection_e.e_aliveCreatures;
        var _targetableEnemies = new List<int>();

        for(int i = 0; i < e_creatureList.Count; i++)
        {
            int creatureTeamPosition = e_creatureList[i].GetComponent<CreatureProperty>().creatureTeamPosition;
            bool isTaunting = e_creatureList[i].GetComponent<CreatureBuffDebuffManager>().taunting;
            bool isHidden = e_creatureList[i].GetComponent<CreatureBuffDebuffManager>().hidden;

            if (isTaunting&& !isHidden && e_aliveCreatures.Contains(creatureTeamPosition) )
            {
                _targetableEnemies.Add(creatureTeamPosition);
            }
        }
        if (_targetableEnemies.Count == 0)   // This means there are no creatures Taunting
            _targetableEnemies = e_aliveCreatures;

        return _targetableEnemies;
    }

    public bool IfTauntPresent_isTargetable(GameObject target)
    {
        var i_target = target.GetComponent<CreatureProperty>().creatureTeamPosition;
        if(TargetableEnemyCreatures().Contains(i_target)) // This makes a short-list, where there are creatures with TAUNT present
            return true;
        else
            return false;
    }
}
