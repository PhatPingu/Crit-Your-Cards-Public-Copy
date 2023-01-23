using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureSelection_p : MonoBehaviour
{
    [SerializeField] GameReferences GameReferences;
    [SerializeField] BattleVictoryManager BattleVictoryManager;
    [SerializeField] CreaturePositioningManager CreaturePositioningManager;
    [SerializeField] PlayerPanelDisplay PlayerPanelDisplay;
    
    public GameObject currentSelectedCreature;

    public List<GameObject> p_creatureList;
    // Thhe lists bellow store the index of the creature in p_creatureList as values.
    public List<int> p_aliveCreatures;
    public List<int> p_deadCreatures;
    public List<int> p_avaliableCreatures;
    public List<int> p_notAvaliableCreatures;

    void Start()
    {
        p_creatureList = new List<GameObject>();
        int position = 0;
        foreach (GameObject creature in GameReferences.p_Creature_array)
        {
            if(creature.activeInHierarchy)
            {
                p_creatureList.Add(creature);
                creature.GetComponent<CreatureProperty>().creatureTeamPosition = position;
                position ++;
            }
        }
        
        p_aliveCreatures = new List<int>();
        foreach (GameObject creature in p_creatureList)
        {
            p_aliveCreatures.Add(creature.GetComponent<CreatureProperty>().creatureTeamPosition);
        }

        p_deadCreatures = new List<int>();
        p_avaliableCreatures = new List<int>(p_aliveCreatures);
        p_notAvaliableCreatures = new List<int>();

    }

    public void SelectNewAvaliableCreature()
    {   
        if(AllCreaturesSleeping())
        {
            return;
        }

        if(p_avaliableCreatures.Count == 1)     
        {
            ResetAvailableCreatureSelection();   
        
            int _index = Random.Range(0, p_avaliableCreatures.Count);
            int _result = p_avaliableCreatures[_index];
            
            SetCurrentSelectedCreature(_result);
            return;
        }

        int castingCreaturePosition = currentSelectedCreature.GetComponent<CreatureProperty>().creatureTeamPosition;
        
        bool isDead = currentSelectedCreature.GetComponent<CreatureProperty>().IsDead();
        if(!isDead)
        {
            p_notAvaliableCreatures.Add(castingCreaturePosition);       
            p_avaliableCreatures.Remove(castingCreaturePosition);
        }
        
        int index = Random.Range(0, p_avaliableCreatures.Count);
        int result = p_avaliableCreatures[index];
        
        SetCurrentSelectedCreature(result);
    }

    public void SetCurrentSelectedCreature(int creatureIndex)
    {
        currentSelectedCreature = p_creatureList[creatureIndex];
        UpdateAvailabilityDisplay();
        CreaturePositioningManager.SetCreatureOnAttackPosition(currentSelectedCreature);
        DisplaySelectedCreature();
    }

    void DisplaySelectedCreature()
    {
        foreach (var creature in p_creatureList)
        {
            creature.GetComponent<CreatureVFX>().CreatureSelectedVFX(false);
        }
        currentSelectedCreature.GetComponent<CreatureVFX>().CreatureSelectedVFX(true);
        PlayerPanelDisplay.UpdatePlayerCreaturePanel(currentSelectedCreature.GetComponent<CreatureProperty>());
    }

    public void ResetAvailableCreatureSelection()
    {
        UpdateDeadCreatureList(); // TODO: Probably not necessary here, since it also gets called inside UpdateAvailabilityDisplay()
        p_avaliableCreatures = new List<int>(p_aliveCreatures);
        p_notAvaliableCreatures = new List<int>(p_deadCreatures);
        UpdateSleepCreatureList();
        UpdateAvailabilityDisplay();
    }

    public void UpdateAvailabilityDisplay()
    {
        UpdateDeadCreatureList(); // TODO: Probably should call this only on ResetAvailableCreatureSelection(), right before this method is called

        for(int i = 0; i < p_creatureList.Count; i++)
        {
            int creaturePosition = p_creatureList[i].GetComponent<CreatureProperty>().creatureTeamPosition;
            Image currentColor = p_creatureList[i].GetComponent<CreatureProperty>().creatureImage;

            for (int k = 0; k < p_avaliableCreatures.Count; k++)
            {
                if(creaturePosition == p_avaliableCreatures[k])
                {
                    currentColor.color = new Color(currentColor.color.r, currentColor.color.g, currentColor.color.b, 1f);
                }
            }
            
            for (int k = 0; k < p_notAvaliableCreatures.Count; k++)
            {
                if(creaturePosition == p_notAvaliableCreatures[k])
                {
                    currentColor.color = new Color(currentColor.color.r, currentColor.color.g, currentColor.color.b, 0.5f);
                }
            }
        }
    }

    public void UpdateDeadCreatureList()
    {
        for(int i = 0; i < p_creatureList.Count; i++)
        {
            int creaturePosition = p_creatureList[i].GetComponent<CreatureProperty>().creatureTeamPosition;
            bool isDead = p_creatureList[i].GetComponent<CreatureProperty>().IsDead();

            if (isDead && !p_deadCreatures.Contains(creaturePosition))
            {
                p_deadCreatures.Add(creaturePosition);
                p_aliveCreatures.Remove(creaturePosition);

                p_notAvaliableCreatures.Add(creaturePosition);
                p_avaliableCreatures.Remove(creaturePosition);

                BattleVictoryManager.SubtractPlayeAliveCount();
            }
        }
    }

    public void UpdateSleepCreatureList()
    {
        for(int i = 0; i < p_creatureList.Count; i++)
        {
            int creaturePosition = p_creatureList[i].GetComponent<CreatureProperty>().creatureTeamPosition;
            bool isSleep = p_creatureList[i].GetComponent<CreatureProperty>().IsSleep();

            if (isSleep && !p_deadCreatures.Contains(creaturePosition) && !p_notAvaliableCreatures.Contains(creaturePosition))
            {
                p_notAvaliableCreatures.Add(creaturePosition);
                p_avaliableCreatures.Remove(creaturePosition);
            }
        }
    }

    public bool AllCreaturesSleeping()
    {
        if(p_notAvaliableCreatures.Count >= 3)
            return true;
        else    
            return false;
    }
}
