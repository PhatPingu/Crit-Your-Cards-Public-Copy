using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeck : MonoBehaviour
{
    public int maxDeckSize;
    public List<Card_ScriptableObj> EnemyDeck_list = new List<Card_ScriptableObj>();
    
    void FixedUpdate()
    {
        if(EnemyDeck_list.Count > maxDeckSize)
        {
            Debug.LogError("ERROR: You went over the allowed maxHandSize");
        }
    }
}
