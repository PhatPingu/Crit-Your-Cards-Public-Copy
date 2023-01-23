using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public int maxDeckSize;
    public List<Card_ScriptableObj> PlayerDeck_list = new List<Card_ScriptableObj>();
    
    void FixedUpdate()
    {
        if(PlayerDeck_list.Count > maxDeckSize)
        {
            Debug.LogError("ERROR: You went over the allowed maxDeckSize");
        }
    }
    
}
