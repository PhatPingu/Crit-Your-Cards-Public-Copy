using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameStates GameStates;

    [SerializeField] BattleDataInitialization BattleDataInitialization;
    
    [SerializeField] PlayerDeck PlayerDeck;
    [SerializeField] PlayerHand PlayerHand;
    [SerializeField] PlayerGrave PlayerGrave;

    [SerializeField] EnemyDeck EnemyDeck;
    [SerializeField] EnemyHand EnemyHand;
    [SerializeField] EnemyGrave EnemyGrave;

    [SerializeField] CombatHistoryDisplay_Manager CombatHistoryDisplay_Manager;

    [SerializeField] PlayerHandDisplay PlayerHandDisplay;
    [SerializeField] CreatureSelection_p CreatureSelection_p;
    [SerializeField] CreatureSelection_e CreatureSelection_e;

    List<Card_ScriptableObj> playerCollection_list;
    List<Card_ScriptableObj> enemyCollection_list;


    void Start()
    {
        playerCollection_list = BattleDataInitialization.PlayerCollection_list;

        enemyCollection_list = BattleDataInitialization.EnemyCollection_list;
    }

    public void StartTheGame()
    {
        ShuffleDecks();
        AddCardsToDeck_Player();
        AddCardsToDeck_Enemy();
        GameStates.currentGameState = GameStates.GameState.PlayerDraw;
        CreatureSelection_p.SetCurrentSelectedCreature(Random.Range(0,2));
        CreatureSelection_e.SetCurrentSelectedCreature(Random.Range(0,2));
    }

    void ShuffleDecks()
    {
        for (int i = 0; i < playerCollection_list.Count; i++) 
        {
         var temp = playerCollection_list[i];
         int randomIndex = Random.Range(i, playerCollection_list.Count);
         playerCollection_list[i] = playerCollection_list[randomIndex];
         playerCollection_list[randomIndex] = temp;
        }

        for (int i = 0; i < enemyCollection_list.Count; i++) 
        {
         var temp = enemyCollection_list[i];
         int randomIndex = Random.Range(i, enemyCollection_list.Count);
         enemyCollection_list[i] = enemyCollection_list[randomIndex];
         enemyCollection_list[randomIndex] = temp;
        }
    }

    void AddCardsToDeck_Player()
    {
        for (int i = 0; i < PlayerDeck.maxDeckSize; i++) 
        {
            PlayerDeck.PlayerDeck_list.Add(playerCollection_list[i]);
        }
    }

    void AddCardsToDeck_Enemy()
    {
        for (int i = 0; i < EnemyDeck.maxDeckSize; i++) 
        {
            EnemyDeck.EnemyDeck_list.Add(enemyCollection_list[i]);
        }
    }

    public void DrawCard_Player(int amount)
    {
        for (int j = 0; j < amount; j++)
        {
            if(PlayerDeck.PlayerDeck_list.Count == 0)
            {
                for (int i = 0; i < PlayerGrave.PlayerGrave_list.Count; i++)
                {
                    PlayerDeck.PlayerDeck_list.Add(PlayerGrave.PlayerGrave_list[i]);
                }
                PlayerGrave.PlayerGrave_list.Clear();
            }
            
            if(PlayerHand.PlayerHand_list.Count == PlayerHand.maxHandSize)
            {
                PlayerGrave.PlayerGrave_list.Add(PlayerDeck.PlayerDeck_list[0]);
                PlayerDeck.PlayerDeck_list.RemoveAt(0);
                Debug.Log("Player Hand Full"); 
            }
            else
            {
                PlayerHand.PlayerHand_list.Add(PlayerDeck.PlayerDeck_list[0]);
                PlayerDeck.PlayerDeck_list.RemoveAt(0);
            }
        }
    } 

    public void DrawCard_Enemy(int amount)
    {
        for (int j = 0; j < amount; j++)
        {
            if(EnemyDeck.EnemyDeck_list.Count == 0)
            {
                for (int i = 0; i < EnemyGrave.EnemyGrave_list.Count; i++)
                {
                    EnemyDeck.EnemyDeck_list.Add(EnemyGrave.EnemyGrave_list[i]);
                }
                EnemyGrave.EnemyGrave_list.Clear();
            }
            
            if(EnemyHand.EnemyHand_list.Count == EnemyHand.maxHandSize)
            {
                EnemyGrave.EnemyGrave_list.Add(EnemyDeck.EnemyDeck_list[0]);
                EnemyDeck.EnemyDeck_list.RemoveAt(0);
                Debug.Log("Enemy Hand Full"); 
            }
            else
            {
                EnemyHand.EnemyHand_list.Add(EnemyDeck.EnemyDeck_list[0]);
                EnemyDeck.EnemyDeck_list.RemoveAt(0);
            }
        }
    } 

    public void SendCardGrave(Card_ScriptableObj Card, Creature_ScriptableObj Caster, Creature_ScriptableObj Target, 
    GameReferences.TeamSide CasterTeam, GameReferences.TeamSide TargetTeam, GameReferences.AllowedTarget allowedTarget)
    {
        var cardsHistory_array = CombatHistoryDisplay_Manager.cards_array;
        if(CombatHistoryDisplay_Manager.cardUsedHistory_list.Count >= cardsHistory_array.Length)
                CombatHistoryDisplay_Manager.RemoveOldestHistoryEntry();

        CombatHistoryDisplay_Manager.cardUsedHistory_list.Add(Card);
        CombatHistoryDisplay_Manager.casterUsedHistory_list.Add(Caster);
        CombatHistoryDisplay_Manager.targetUsedHistory_list.Add(Target);
        CombatHistoryDisplay_Manager.castingTeam_list.Add(CasterTeam);
        CombatHistoryDisplay_Manager.targetTeam_list.Add(TargetTeam);
        CombatHistoryDisplay_Manager.allowedTarget_list.Add(allowedTarget);
        
                
        var last = CombatHistoryDisplay_Manager.cardUsedHistory_list.Count - 1;
        cardsHistory_array[last].GetComponent<CardProperty>().teamSide = CasterTeam;
       
        if(CasterTeam == GameReferences.TeamSide.Player)
        {
            PlayerGrave.PlayerGrave_list.Add(Card);
            PlayerHand.PlayerHand_list.Remove(Card);
        }
        else
        {
            EnemyGrave.EnemyGrave_list.Add(Card);
            EnemyHand.EnemyHand_list.Remove(Card);
        }
        
        CombatHistoryDisplay_Manager.UpdateCards_array();
    }
}
