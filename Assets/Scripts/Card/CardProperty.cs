using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardProperty : MonoBehaviour
{
    ManaManager ManaManager;
    
    public Card_ScriptableObj cardData;
    public Card_VFX Card_VFX;
       
    [SerializeField] GameObject Image;
    [SerializeField] TextMeshProUGUI cardName;
    
    public TextMeshProUGUI manaCost;
    public GameReferences.StrategyTypes cardStrategy;
    public GameReferences.FamilyColor cardColor;
    public GameReferences.TeamSide teamSide;
    public GameReferences.AllowedTarget allowedTarget;

    [SerializeField] Color canCast;
    [SerializeField] Color canNotCast;


    [SerializeField] Image manaImage;
    [SerializeField] Sprite canCastMana;
    [SerializeField] Sprite canNotCastMana;

    void OnEnable()
    {
        ManaManager = GameObject.Find("Game Manager").GetComponent<ManaManager>();

        if(cardData != null)
            UpdateCardProperty();
    }

    void FixedUpdate()
    {
        if(cardData != null)
        {
            UpdateCardProperty();
        }
    }

    public void UpdateCardProperty()
    {
        Image.GetComponent<Image>().sprite = cardData.cardImage;
        cardName.text = cardData.cardName;
        manaCost.text = cardData.manaCost.ToString();
        cardStrategy = cardData.cardStrategy;
        cardColor = cardData.cardColor;
        allowedTarget = cardData.cardEffect[0].allowedTarget;
        FontColorFeedback();
    }

    public void FontColorFeedback()
    {
        if(cardData != null)
        {
            if(ManaManager.CheckMana(cardData.manaCost, teamSide))
            {
                manaCost.color = canCast;
                manaImage.sprite = canCastMana;
            }
            else
            {
                manaCost.color = canNotCast;
                manaImage.sprite = canNotCastMana;
            }
        }
    }
}
