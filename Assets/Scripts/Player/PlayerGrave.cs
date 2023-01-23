using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrave : MonoBehaviour
{
    public int maxGraveSize;
    public List<Card_ScriptableObj> PlayerGrave_list = new List<Card_ScriptableObj>();

    void FixedUpdate()
    {
        if(PlayerGrave_list.Count > maxGraveSize)
        {
            Debug.LogError("ERROR: You went over the allowed maxGraveSize");
        }
    }
}
