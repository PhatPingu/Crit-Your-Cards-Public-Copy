using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCardCount : MonoBehaviour
{
    [SerializeField] GameObject GameManager;

    GameObject PlayerDeck;
    GameObject PlayerGrave;
    GameObject EnemyDeck;
    GameObject EnemyGrave;
    GameObject EnemyHand;

    List<Card_ScriptableObj> PlayerDeck_list;
    List<Card_ScriptableObj> PlayerGrave_list;
    List<Card_ScriptableObj> PlayerHand_list;

    List<Card_ScriptableObj> EnemyDeck_list;
    List<Card_ScriptableObj> EnemyGrave_list;
    List<Card_ScriptableObj> EnemyHand_list;

    [SerializeField] TextMeshProUGUI cardCountDisplay; 

    public enum DisplayCount { PlayerDeck, PlayerGrave, EnemyDeck, EnemyGrave, EnemyHand };
    [SerializeField] DisplayCount displayCount;

    void Start ()
    {
        GameReferences gameReferences = GameManager.GetComponent<GameReferences>();
        
        PlayerDeck = gameReferences.PlayerDeck;
        PlayerGrave = gameReferences.PlayerGrave;
        EnemyDeck = gameReferences.EnemyDeck;
        EnemyGrave = gameReferences.EnemyGrave;
        EnemyHand = gameReferences.EnemyHand;

        UpdateDeckLists();
        UpdateCardCountDisplay();
    }

    void Update()
    {
        UpdateCardCountDisplay();
    }

    public void UpdateCardCountDisplay()
    {
        if(displayCount == DisplayCount.PlayerDeck) cardCountDisplay.text = PlayerDeck_list.Count.ToString();
        if(displayCount == DisplayCount.PlayerGrave) cardCountDisplay.text = PlayerGrave_list.Count.ToString();
        if(displayCount == DisplayCount.EnemyDeck) cardCountDisplay.text = EnemyDeck_list.Count.ToString();
        if(displayCount == DisplayCount.EnemyGrave) cardCountDisplay.text = EnemyGrave_list.Count.ToString();
        if(displayCount == DisplayCount.EnemyHand) cardCountDisplay.text = EnemyHand_list.Count.ToString();
    }

    public void UpdateDeckLists()
    {
        PlayerDeck_list = PlayerDeck.GetComponent<PlayerDeck>().PlayerDeck_list;
        PlayerGrave_list = PlayerGrave.GetComponent<PlayerGrave>().PlayerGrave_list;

        EnemyDeck_list = EnemyDeck.GetComponent<EnemyDeck>().EnemyDeck_list;
        EnemyGrave_list = EnemyGrave.GetComponent<EnemyGrave>().EnemyGrave_list;
        EnemyHand_list = EnemyHand.GetComponent<EnemyHand>().EnemyHand_list;
    }

}
