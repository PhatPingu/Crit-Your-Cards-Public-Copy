using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDataInitialization : MonoBehaviour
{
    [SerializeField] GameReferences GameReferences;   
    [SerializeField] BattleVictoryManager BattleVictoryManager;
    [SerializeField] PlayerDeck PlayerDeck;
    [SerializeField] PlayerHand PlayerHand;
    [SerializeField] PlayerGrave PlayerGrave;
    [SerializeField] GameObject[] deckDisplay_array;

    [SerializeField] EnemyDeck EnemyDeck;
    [SerializeField] EnemyHand EnemyHand;
    [SerializeField] EnemyGrave EnemyGrave;

    public List<Creature_ScriptableObj> p_creatureList;
    public List<Creature_ScriptableObj> e_creatureList;
    public List<Card_ScriptableObj> EnemyCollection_list = new List<Card_ScriptableObj>();
    public List<Card_ScriptableObj> PlayerCollection_list = new List<Card_ScriptableObj>();

    void Start()
    {
        //SetPlayerCreatureData();
        //SetEnemyCreatureData();
        SetPlayerCreaturesForBattle();        
    }

    public void InitializeBattleData()
    {
        BattleVictoryManager.ResetAliveCreatures();
    }

    public void ReInitializeBattleData()
    {
        ResetAllCards();
        BattleVictoryManager.ResetAliveCreatures();
    }

    void SetPlayerCreatureData()
    {
        var activeTeam = PlayerGameData.s_data.creatureCollection_activeTeam;
        p_creatureList.Clear();
        p_creatureList = activeTeam;
    }

    void SetEnemyCreatureData()
    {
        var CreaturePool = Data_GameLibrary.s_data.creatureLibrary_array;
        var availablePool = Data_GameLibrary.s_data.i_creatureLibrary_AvailableForUsed;
        e_creatureList.Clear();
        
        for (int i = 0; i < 3; i++)
        {
            int key = Random.Range(0, availablePool.Count);
            e_creatureList.Add(CreaturePool[availablePool[key]]);
        }
    }

    void SetPlayerCreaturesForBattle()
    {

        foreach (var creature in GameReferences.all_Creature_array)
        {
            creature.SetActive(false);
        }

        for (int i = 0; i < p_creatureList.Count; i++)
        {
            var creature = GameReferences.p_Creature_array[i];
            var creatureProperty = creature.GetComponent<CreatureProperty>();

            creature.SetActive(true);
            creatureProperty.creatureData = p_creatureList[i];
            creatureProperty.UpdateCreatureProperty();
        }

        for (int i = 0; i < e_creatureList.Count; i++)
        {
            var creature = GameReferences.e_Creature_array[i];
            var creatureProperty = creature.GetComponent<CreatureProperty>();

            creature.SetActive(true);
            creatureProperty.creatureData = e_creatureList[i];
            creatureProperty.UpdateCreatureProperty();
        }
    }

   

    public void ResetAllCards()
    {
        PlayerDeck.PlayerDeck_list      = new List<Card_ScriptableObj>();
        PlayerHand.PlayerHand_list      = new List<Card_ScriptableObj>();
        PlayerGrave.PlayerGrave_list    = new List<Card_ScriptableObj>();
        EnemyDeck.EnemyDeck_list        = new List<Card_ScriptableObj>();
        EnemyHand.EnemyHand_list        = new List<Card_ScriptableObj>();
        EnemyGrave.EnemyGrave_list      = new List<Card_ScriptableObj>();

        foreach (var deck in deckDisplay_array)
        {
            deck.GetComponent<DisplayCardCount>().UpdateDeckLists();
        }
    }

}
