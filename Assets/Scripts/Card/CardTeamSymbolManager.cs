using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTeamSymbolManager : MonoBehaviour
{
    [SerializeField] CardProperty CardProperty;
    [SerializeField] Image teamSymbol;
    [SerializeField] Sprite[] teamSymbol_array;

    void FixedUpdate()
    {
        if(CardProperty.cardColor == GameReferences.FamilyColor.Blue)
            teamSymbol.sprite = teamSymbol_array[0];
        else if(CardProperty.cardColor == GameReferences.FamilyColor.Red)
            teamSymbol.sprite = teamSymbol_array[1];
        else if(CardProperty.cardColor == GameReferences.FamilyColor.White)
            teamSymbol.sprite = teamSymbol_array[2];
        else if(CardProperty.cardColor == GameReferences.FamilyColor.Green)
            teamSymbol.sprite = teamSymbol_array[3];
        else if(CardProperty.cardColor == GameReferences.FamilyColor.Black)
            teamSymbol.sprite = teamSymbol_array[4];
        else
            teamSymbol.sprite = teamSymbol_array[5];
    }
}