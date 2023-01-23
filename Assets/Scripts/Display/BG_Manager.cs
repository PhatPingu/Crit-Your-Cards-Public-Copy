using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Manager : MonoBehaviour
{
    [SerializeField] SpriteRenderer topStripe;
    [SerializeField] SpriteRenderer bg_BackLayer;
    [SerializeField] SpriteRenderer bg_MidLayer;
    [SerializeField] SpriteRenderer bg_FrontLayer;
    
    [SerializeField] Sprite[] bg_BackLayer_array;
    [SerializeField] Sprite[] bg_MidLayer_array;
    [SerializeField] Sprite[] bg_FrontLayer_array;

    public void SetDessaturateBG(bool choice)
    {
        if(choice)
        {
            bg_BackLayer.sprite = bg_BackLayer_array[1];
            bg_MidLayer.sprite = bg_MidLayer_array[1];
            bg_FrontLayer.sprite = bg_FrontLayer_array[1];
        }
        else
        {
            bg_BackLayer.sprite = bg_BackLayer_array[0];
            bg_MidLayer.sprite = bg_MidLayer_array[0];
            bg_FrontLayer.sprite = bg_FrontLayer_array[0];
        }
    }
}
