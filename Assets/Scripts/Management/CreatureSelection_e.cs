using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureSelection_e : MonoBehaviour
{
    [SerializeField] GameReferences GameReferences;
    [SerializeField] BattleVictoryManager BattleVictoryManager;
    [SerializeField] CreaturePositioningManager CreaturePositioningManager;
    
    public GameObject currentSelectedCreature;

    public List<GameObject> e_creatureList;

    public List<int> e_aliveCreatures;
    public List<int> e_deadCreatures;
    public List<int> e_avaliableCreatures;
    public List<int> e_notAvaliableCreatures;

    public List<GameObject> e_aliveCreatures_list;
    
    void Start()
    {
        e_creatureList = new List<GameObject>();
        int position = 0;
        foreach (GameObject creature in GameReferences.e_Creature_array)
        {
            if(creature.activeInHierarchy)
            {
                e_creatureList.Add(creature);
                creature.GetComponent<CreatureProperty>().creatureTeamPosition = position;
                position ++;
            }
        }

        e_aliveCreatures = new List<int>();
        foreach (GameObject creature in e_creatureList)
        {
            e_aliveCreatures.Add(creature.GetComponent<CreatureProperty>().creatureTeamPosition);
        }

        e_deadCreatures = new List<int>();
        e_avaliableCreatures = new List<int>(e_aliveCreatures);
        e_notAvaliableCreatures = new List<int>();


        //This is a better alternative to public List<int> e_aliveCreatures
        //Not being used yet. REFACTOR
        e_aliveCreatures_list = new List<GameObject>() 
        {
            GameReferences.e_Creature_01, 
            GameReferences.e_Creature_02, 
            GameReferences.e_Creature_03
        };
    }

    public void SelectNewAvaliableCreature()
    {   
        if(AllCreaturesSleeping())
        {
            return;
        }

        if(e_avaliableCreatures.Count == 1)     
        {
            ResetAvailableCreatureSelection();   
        
            int _index = Random.Range(0, e_avaliableCreatures.Count);
            int _result = e_avaliableCreatures[_index];
            
            SetCurrentSelectedCreature(_result);
            return;
        }

        int castingCreaturePosition = currentSelectedCreature.GetComponent<CreatureProperty>().creatureTeamPosition;
        
        bool isDead = currentSelectedCreature.GetComponent<CreatureProperty>().IsDead();
        if(!isDead)
        {
            e_notAvaliableCreatures.Add(castingCreaturePosition);       
            e_avaliableCreatures.Remove(castingCreaturePosition);
        }
        
        int index = Random.Range(0, e_avaliableCreatures.Count);
        int result = e_avaliableCreatures[index];
        
        SetCurrentSelectedCreature(result);
    }

    public void SetCurrentSelectedCreature(int creatureIndex)
    {
        currentSelectedCreature = e_creatureList[creatureIndex];
        UpdateAvailabilityDisplay();
        CreaturePositioningManager.SetCreatureOnAttackPosition(currentSelectedCreature);
        DisplaySelectedCreature();
    }

    void DisplaySelectedCreature()
    {
        foreach (var creature in e_creatureList)
        {
            creature.GetComponent<CreatureVFX>().CreatureSelectedVFX(false);
        }
        currentSelectedCreature.GetComponent<CreatureVFX>().CreatureSelectedVFX(true);
    }

    public void ResetAvailableCreatureSelection()
    {
        UpdateDeadCreatureList(); // TODO: Probably not necessary here, since it also gets called inside UpdateAvailabilityDisplay()
        e_avaliableCreatures = new List<int>(e_aliveCreatures);
        e_notAvaliableCreatures = new List<int>(e_deadCreatures);
        UpdateSleepCreatureList();
        UpdateAvailabilityDisplay();
    }

    public void UpdateAvailabilityDisplay()
    {
        UpdateDeadCreatureList(); // TODO: Probably should call this only on ResetAvailableCreatureSelection(), right before this method is called

        for(int i = 0; i < e_creatureList.Count; i++)
        {
            int creaturePosition = e_creatureList[i].GetComponent<CreatureProperty>().creatureTeamPosition;
            Image currentColor = e_creatureList[i].GetComponent<CreatureProperty>().creatureImage;

            for (int k = 0; k < e_avaliableCreatures.Count; k++)
            {
                if(creaturePosition == e_avaliableCreatures[k])
                {
                    currentColor.color = new Color(currentColor.color.r, currentColor.color.g, currentColor.color.b, 1f);
                }
            }
            
            for (int k = 0; k < e_notAvaliableCreatures.Count; k++)
            {
                if(creaturePosition == e_notAvaliableCreatures[k])
                {
                    currentColor.color = new Color(currentColor.color.r, currentColor.color.g, currentColor.color.b, 0.5f);
                }
            }
        }
    }

    public void UpdateDeadCreatureList()
    {
        for(int i = 0; i < e_creatureList.Count; i++)
        {
            int creaturePosition = e_creatureList[i].GetComponent<CreatureProperty>().creatureTeamPosition;
            bool isDead = e_creatureList[i].GetComponent<CreatureProperty>().IsDead();

            if (isDead && !e_deadCreatures.Contains(creaturePosition))
            {
                e_deadCreatures.Add(creaturePosition);
                e_aliveCreatures.Remove(creaturePosition);

                e_notAvaliableCreatures.Add(creaturePosition);
                e_avaliableCreatures.Remove(creaturePosition);

                BattleVictoryManager.SubtractEnemyAliveCount();
            }
        }
    }

    public void UpdateSleepCreatureList()
    {
        for(int i = 0; i < e_creatureList.Count; i++)
        {
            int creaturePosition = e_creatureList[i].GetComponent<CreatureProperty>().creatureTeamPosition;
            bool isSleep = e_creatureList[i].GetComponent<CreatureProperty>().IsSleep();

            if (isSleep && e_aliveCreatures.Contains(creaturePosition) && !e_notAvaliableCreatures.Contains(creaturePosition))
            {
                e_notAvaliableCreatures.Add(creaturePosition);
                e_avaliableCreatures.Remove(creaturePosition);
            }
        }

        var i_creature = currentSelectedCreature.GetComponent<CreatureProperty>().creatureTeamPosition;
        if (e_notAvaliableCreatures.Contains(i_creature) && e_avaliableCreatures.Count != 1)
            SelectNewAvaliableCreature();
        else if (e_notAvaliableCreatures.Contains(i_creature) && e_avaliableCreatures.Count == 1)
            SetCurrentSelectedCreature(e_avaliableCreatures[0]);
    }

    public bool AllCreaturesSleeping()
    {
        if(e_notAvaliableCreatures.Count >= 3)
            return true;
        else    
            return false;
    }
}
