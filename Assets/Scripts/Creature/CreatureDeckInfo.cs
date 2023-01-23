using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDeckInfo : MonoBehaviour
{
    public List<Card_ScriptableObj> playerDeck_list;
    public List<Card_ScriptableObj> playerHand_list;
    public List<Card_ScriptableObj> playerGrave_list;

    public List<Card_ScriptableObj> enemyDeck_list;
    public List<Card_ScriptableObj> enemyHand_list;
    public List<Card_ScriptableObj> enemyGrave_list;

    public GameObject p_DiscardDisplay;
    public GameObject e_DiscardDisplay;

    void Start()
    {
        playerDeck_list = GameObject.Find("Player Deck").GetComponent<PlayerDeck>().PlayerDeck_list;
        playerHand_list = GameObject.Find("Player Hand").GetComponent<PlayerHand>().PlayerHand_list;
        playerGrave_list = GameObject.Find("Player Grave").GetComponent<PlayerGrave>().PlayerGrave_list;
        enemyDeck_list = GameObject.Find("Enemy Deck").GetComponent<EnemyDeck>().EnemyDeck_list;
        enemyHand_list = GameObject.Find("Enemy Hand").GetComponent<EnemyHand>().EnemyHand_list;
        enemyGrave_list = GameObject.Find("Enemy Grave").GetComponent<EnemyGrave>().EnemyGrave_list;
        p_DiscardDisplay = GameObject.Find("P_Discard Display");
        e_DiscardDisplay = GameObject.Find("E_Discard Display");
    }
}
