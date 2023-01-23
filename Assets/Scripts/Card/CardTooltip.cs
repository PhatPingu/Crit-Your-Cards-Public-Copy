using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardTooltip : MonoBehaviour
{
    [SerializeField] GameObject[] toolTips;
    [SerializeField] TextMeshProUGUI[] toolTips_TMP;
    [SerializeField] CardProperty CardProperty;
    [SerializeField] CardMovement CardMovement;
    
    CardEffect_ScriptableObj CardEffect;

    void Start()
    {
        SetTooltipsActive(false);
    }

    public void SetTooltipsActive(bool choice)
    {
        if(!choice)
        {
            for (int i = 0; i < toolTips.Length; i++)
                toolTips[i].SetActive(false);
            return;
        }

        for (int i = 0; i < toolTips.Length; i++)
        {
            CardEffect = CardProperty.cardData.cardEffect[i];    

            if (CardEffect.Tooltip() == "" || CardProperty.cardData.hideTooltip[i])
                toolTips[i].SetActive(false);
            else
            {
                toolTips[i].SetActive(true);
                toolTips_TMP[i].text = CardEffect.Tooltip();
            }   
        }
    }
}
