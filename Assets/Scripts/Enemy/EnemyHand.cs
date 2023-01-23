using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    public int maxHandSize;
    public List<Card_ScriptableObj> EnemyHand_list = new List<Card_ScriptableObj>();

    void FixedUpdate()
    {
        if(EnemyHand_list.Count > maxHandSize)
        {
            Debug.LogError("ERROR: You went over the allowed maxHandSize");
        }
    }
}
