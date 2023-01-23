using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class CardDescription_Attack : CardDescription
{
    [Header("CardDescription_Attack Fields")]
    [SerializeField] GameObject attackSymbolBG;

    public override void OverrideText()
    {
        if(CardProperty.cardData.displayAttackStat)
        {
            attackSymbolBG.SetActive(true);
            description.text = CurrentCreatureAttack.ToString();
        }
        else
        {
            attackSymbolBG.SetActive(false);
            description.text = "";
        }
    }

}
