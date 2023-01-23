using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_VFX : MonoBehaviour
{
    [SerializeField] CardProperty CardProperty;
    [SerializeField] CardMovement CardMovement;
    [SerializeField] GameObject CardOutline_CanCastGreen;
    //[SerializeField] GameObject CardOutline_SelectedBlue;
    
    GameObject GameManager;
    GameStates GameStates;
    ManaManager ManaManager;

    void Awake()
    {
        GameManager = GameObject.Find("Game Manager");
        GameStates = GameManager.GetComponent<GameStates>();
        ManaManager = GameManager.GetComponent<ManaManager>();
    }

    void Start()
    {
        SetCardOutline(false, false);
    }

    void FixedUpdate()
    {
        if(GameStates.currentGameState != GameStates.GameState.PlayerTurn)
        {
            SetCardOutline(false, false);
        }

        if(GameStates.currentGameState == GameStates.GameState.PlayerTurn && CardProperty.cardData != null)
        {
            if(CardProperty.cardData.manaCost <= ManaManager.currentMana_player)
            {
                SetCardOutline(true, false);
            }
            else
            {
                SetCardOutline(false, false);
            }

            /*if(CardMovement.thisCardSelected)
            {
                SetCardOutline(false, true);
            }*/
        }

    }

    public void SetCardOutline(bool CanCastGreen = false, bool SelectedBlue = false)
    {
        CardOutline_CanCastGreen.SetActive(CanCastGreen);
        //CardOutline_SelectedBlue.SetActive(SelectedBlue);
    }

}
