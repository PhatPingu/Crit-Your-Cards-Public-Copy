using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public int maxHandSize;
    public List<Card_ScriptableObj> PlayerHand_list = new List<Card_ScriptableObj>();

    void FixedUpdate()
    {
        if(PlayerHand_list.Count > maxHandSize)
        {
            Debug.LogError("ERROR: You went over the allowed maxHandSize");
        }
    }
}
