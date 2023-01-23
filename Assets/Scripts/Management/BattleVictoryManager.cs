using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleVictoryManager : MonoBehaviour
{
    [SerializeField] GameReferences GameReferences;
    [SerializeField] GameStates GameStates;

    int p_AliveCreatureCount;
    int e_AliveCreatureCount;

    void Start()
    {
        ResetAliveCreatures();
    }

    public void ResetAliveCreatures()
    {
        p_AliveCreatureCount = 0;
        e_AliveCreatureCount = 0;
        
        foreach (GameObject creature in GameReferences.p_Creature_array)
        {
            if(creature.activeInHierarchy)
            {
                p_AliveCreatureCount ++;
            }
        }

        foreach (GameObject creature in GameReferences.e_Creature_array)
        {
            if(creature.activeInHierarchy)
            {
                e_AliveCreatureCount ++;
            }
        }
    }
        
    public void SubtractPlayeAliveCount()
    {
        p_AliveCreatureCount --;
        CheckPlayerLoss();
    }
    
    public void SubtractEnemyAliveCount()
    {
        e_AliveCreatureCount --;
        CheckPlayerVictory();
    }

    void CheckPlayerLoss()
    {
        if(p_AliveCreatureCount <= 0)
        {
            GameStates.currentGameState = GameStates.GameState.Defeated;
        }
    }

    void CheckPlayerVictory()
    {
        if(e_AliveCreatureCount <= 0)
        {
            GameStates.currentGameState = GameStates.GameState.Victory;
        }
    }
}
