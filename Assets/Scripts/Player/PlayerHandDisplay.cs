using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandDisplay : MonoBehaviour
{
    [SerializeField] PlayerHand PlayerHand;

    public List<GameObject> PlayerHand_list = new List<GameObject>();

    void FixedUpdate()
    {
        UpdatePlayerHand_list();   
    }

    void UpdatePlayerHand_list()
    {
        int count = PlayerHand.PlayerHand_list.Count;
        for (int i = 0; i < PlayerHand_list.Count; i++)
        {
            if(count > 0)
            {
                PlayerHand_list[i].GetComponent<CardProperty>().cardData = PlayerHand.PlayerHand_list[i];
                PlayerHand_list[i].SetActive(true);
                count--;
            }
            else
            {
                PlayerHand_list[i].SetActive(false);
            }
        }
    }
}
