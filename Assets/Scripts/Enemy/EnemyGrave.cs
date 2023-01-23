using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrave : MonoBehaviour
{
    public int maxGraveSize;
    public List<Card_ScriptableObj> EnemyGrave_list = new List<Card_ScriptableObj>();

    void FixedUpdate()
    {
        if(EnemyGrave_list.Count > maxGraveSize)
        {
            Debug.LogError("ERROR: You went over the allowed maxGraveSize");
        }
    }
}
