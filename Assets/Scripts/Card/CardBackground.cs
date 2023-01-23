using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBackground : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Sprite[] backgroundImage_list;
    [SerializeField] CardProperty CardProperty;

    void Update()
    {
        SetCardBackground();
    }
    
    void SetCardBackground()
    {
        if      (CardProperty.cardColor == GameReferences.FamilyColor.Black)
        {
            backgroundImage.sprite = backgroundImage_list[0];
        }
        else if (CardProperty.cardColor == GameReferences.FamilyColor.Blue)
        {
            backgroundImage.sprite = backgroundImage_list[1];
        }
        else if (CardProperty.cardColor == GameReferences.FamilyColor.Green)
        {
            backgroundImage.sprite = backgroundImage_list[2];
        }
        else if (CardProperty.cardColor == GameReferences.FamilyColor.Red)
        {
            backgroundImage.sprite = backgroundImage_list[3];
        }
        else if (CardProperty.cardColor == GameReferences.FamilyColor.White)
        {
            backgroundImage.sprite = backgroundImage_list[4];
        }
        else
        {
            backgroundImage.sprite = backgroundImage_list[5];
        }

    }
}
