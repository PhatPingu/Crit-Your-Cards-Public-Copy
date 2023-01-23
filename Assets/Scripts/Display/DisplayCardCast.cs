using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCardCast : MonoBehaviour
{
    [SerializeField] CardProperty CardProperty;
    [SerializeField] Canvas canvas;

    int originalSort = -5;


    public void DisplayCardBeingCast(bool choice, GameObject cardCasted)
    {
        if(choice)
        {
            CardProperty.cardData = cardCasted.GetComponent<CardProperty>().cardData;
            canvas.sortingOrder = 100;
        }
        else
        {
            canvas.sortingOrder = originalSort;
        }
    }

    public void SendToGrave_Animation()
    {
        transform.GetComponent<Canvas>().sortingOrder = 1;

        Invoke("SendToGrave_AnimationEnd", 2f);
    }

    void SendToGrave_AnimationEnd()
    {
        canvas.sortingOrder = originalSort;
    }
}
