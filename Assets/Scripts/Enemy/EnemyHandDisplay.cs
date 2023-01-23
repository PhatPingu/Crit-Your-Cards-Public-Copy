using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandDisplay : MonoBehaviour
{
    [SerializeField] EnemyHand EnemyHand;

    public List<GameObject> EnemyHand_list = new List<GameObject>();
    public List<GameObject> avalibaleEnemyCards_list = new List<GameObject>();

    void Update() //If this becomes FixedUpdate, it breaks the game;
    {
        UpdateEnemyHand_list();   
    }

    void UpdateEnemyHand_list()
    {
        avalibaleEnemyCards_list = new List<GameObject>();

        int count = EnemyHand.EnemyHand_list.Count;
        for (int i = 0; i < EnemyHand_list.Count; i++)
        {
            if(count > 0)
            {
                EnemyHand_list[i].GetComponent<CardProperty>().cardData = EnemyHand.EnemyHand_list[i];
                EnemyHand_list[i].SetActive(true);
                avalibaleEnemyCards_list.Add(EnemyHand_list[i]);
                count--;
            }
            else
            {
                EnemyHand_list[i].SetActive(false);
            }
        }
    }


}
