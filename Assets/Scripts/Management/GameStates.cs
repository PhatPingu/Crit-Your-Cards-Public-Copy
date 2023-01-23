using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStates : MonoBehaviour
{
    [SerializeField] GameManager GameManager;
    [SerializeField] GameReferences GameReferences;
    [SerializeField] ManaManager ManaManager;
    [SerializeField] CardSelectionManager CardSelectionManager;
    [SerializeField] EnemyAI EnemyAI;
    [SerializeField] SFX_Manager SFX_Manager;
    [SerializeField] CreatureSelection_e CreatureSelection_e;
    [SerializeField] CreatureSelection_p CreatureSelection_p;
    [SerializeField] CreaturePositioningManager CreaturePositioningManager;
    

    [SerializeField] Button EndTurnButton;
    [SerializeField] TextMeshProUGUI EndTurnButton_text;
    [SerializeField] Image EndTurnButton_Image;
    [SerializeField] Sprite[] EndTurnButton_Sprite; // 0-Active, 1-Inactive

    [SerializeField] GameObject victoryDisplay;
    [SerializeField] GameObject defeatDisplay;


    public enum GameState
    {
        OffCombat,
        ResetGame,
        PlayerDraw,
        PlayerTurn,
        PlayerEndTurn,
        EnemyDraw,
        EnemyTurn,
        EnemyEndTurn,
        CastingCard,
        Defeated,
        Victory
    }

    [SerializeField] public GameState currentGameState;
    


    void Start()
    {
        currentGameState = GameState.OffCombat;
    }

    void Update()
    {
        if(currentGameState == GameState.OffCombat)
        {
            
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.ResetGame)
        {
            ResetAllTickers(); // REFACTR: This is not really used
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.PlayerDraw)
        {
            GameManager.DrawCard_Player(3);
            ManaManager.GrowMana_Player(1);
            ManaManager.DisplayPlayerManaOverEnemy(true);
            CreatureSelection_p.ResetAvailableCreatureSelection();

            currentGameState = GameState.PlayerTurn;
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.PlayerTurn)
        {
            UpdatePlayerAttackInterface(true);
            CreaturePositioningManager.SetCreatureOnAttackPosition(CreatureSelection_p.currentSelectedCreature);
            EndTurnButton.enabled = true;
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.PlayerEndTurn)
        {
            UpdatePlayerTicker();

            EndTurnButton.enabled = false;
            EndTurnButton_Image.sprite = EndTurnButton_Sprite[1];
            EndTurnButton_text.text = "Enemy Turn";

            SFX_Manager.audioPlayer.PlayOneShot(SFX_Manager.UI_endTurnFlip);
            currentGameState = GameState.EnemyDraw;
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.EnemyDraw)
        {
            GameManager.DrawCard_Enemy(3);
            ManaManager.GrowMana_Enemy(1);
            ManaManager.DisplayPlayerManaOverEnemy(false);
            CreatureSelection_e.ResetAvailableCreatureSelection();

            currentGameState = GameState.EnemyTurn;
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.EnemyTurn)
        {
            UpdatePlayerAttackInterface(false);
            PlayerCardMovementEnabled(false);
            CreaturePositioningManager.SetCreatureOnAttackPosition(CreatureSelection_e.currentSelectedCreature); 
            
            if(CreatureSelection_e.AllCreaturesSleeping()) 
                currentGameState = GameState.EnemyEndTurn;

            EnemyAI.PerformMove_Enemy();  
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.EnemyEndTurn)
        {
            UpdateEnemyTicker();
            EndTurnButton_Image.sprite = EndTurnButton_Sprite[0];
            EndTurnButton_text.text = "End Turn";

            SFX_Manager.audioPlayer.PlayOneShot(SFX_Manager.UI_endTurnFlip);
            currentGameState = GameState.PlayerDraw;
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.CastingCard)
        {
            PlayerCardMovementEnabled(false);
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.Defeated)
        {
            defeatDisplay.SetActive(true);
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
        else if(currentGameState == GameState.Victory)
        {
            victoryDisplay.SetActive(true);
        } //-------------------------------------------------------------------------------------------------------------------------------------------------------
    }







    void PlayerCardMovementEnabled(bool choice)
    {
        foreach(GameObject card in CardSelectionManager.playerCardHandArray)
            card.GetComponent<CardMovement>().CardSelected(choice);
    }

    void UpdatePlayerAttackInterface(bool choice)
    {
        foreach(GameObject creature in GameReferences.all_Creature_array)
            creature.GetComponent<CreatureInterfaceManager>().SetPlayerAttackInterface(choice);
    }

    void UpdatePlayerTicker() 
    {
        foreach (var creature in GameReferences.p_Creature_array)
            creature.GetComponent<CreatureBuffDebuffManager>().RemoveTickFromBuffDebuff();
    }

    void UpdateEnemyTicker() 
    {
        foreach (var creature in GameReferences.e_Creature_array)
            creature.GetComponent<CreatureBuffDebuffManager>().RemoveTickFromBuffDebuff();
    }

    void ResetAllTickers() 
    {
        foreach (var creature in GameReferences.all_Creature_array)
            creature.GetComponent<CreatureBuffDebuffManager>().RemoveTickFromBuffDebuff();
    }


}
