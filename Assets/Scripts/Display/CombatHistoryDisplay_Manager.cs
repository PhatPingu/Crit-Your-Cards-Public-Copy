using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHistoryDisplay_Manager : MonoBehaviour
{
    public GameObject[] cards_array;

    public List<Card_ScriptableObj>           cardUsedHistory_list;
    public List<Creature_ScriptableObj>       casterUsedHistory_list;
    public List<Creature_ScriptableObj>       targetUsedHistory_list;
    public List<GameReferences.TeamSide>      castingTeam_list;
    public List<GameReferences.AllowedTarget> allowedTarget_list;
    public List<GameReferences.TeamSide>      targetTeam_list;

    void Start()
    {
        cardUsedHistory_list    = new List<Card_ScriptableObj>();
        casterUsedHistory_list  = new List<Creature_ScriptableObj>();
        targetUsedHistory_list  = new List<Creature_ScriptableObj>();
        castingTeam_list        = new List<GameReferences.TeamSide>();
        allowedTarget_list      = new List<GameReferences.AllowedTarget>();
        targetTeam_list         = new List<GameReferences.TeamSide>();
        
        UpdateCards_array();
    }

    public void UpdateCards_array()
    {
        int listSize = cardUsedHistory_list.Count;
        int index = 0;

        foreach(var card in cards_array)
        {
            if(index > listSize - 1)
            {
                card.SetActive(false);
            }
            else
            {
                card.SetActive(true);

                var cardAction = card.GetComponent<CombatHistory_OnHoverManager>().CombatHistoryDisplay_TeamAction;
                var cardProperty = card.GetComponent<CardProperty>();
                cardProperty.cardData = cardUsedHistory_list[index];
                cardProperty.UpdateCardProperty(); 

                cardAction.SetTeamActionDisplay(castingTeam_list[index], allowedTarget_list[index], targetTeam_list[index]);
                index ++;
            }
        }
    }

    public void RemoveOldestHistoryEntry()
    {
        cardUsedHistory_list.RemoveAt(0);
        casterUsedHistory_list.RemoveAt(0);
        targetUsedHistory_list.RemoveAt(0);
        castingTeam_list.RemoveAt(0);
        allowedTarget_list.RemoveAt(0);
        targetTeam_list.RemoveAt(0);
    }
}
