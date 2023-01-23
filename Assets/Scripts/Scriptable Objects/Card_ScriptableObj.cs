using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "GameObjects/GenerateCard")]
public class Card_ScriptableObj : ScriptableObject
{
    public Sprite cardImage;    
    public string cardName;
    public int manaCost;
    public GameReferences.StrategyTypes cardStrategy;

    public CardEffect_ScriptableObj[] cardEffect;
    public bool[] hideTooltip = new bool[3];
    
    public GameReferences.FamilyColor cardColor;

    [TextArea(10,30)]
    public string overrideescription;
    
    public bool displayAttackStat;
    
    public bool onHover_HighlightAttack, onHover_HighlightDefense, onHover_HighlightHealing;

    // make onHove_HighlightAttack, onHove_HighlightDefense, onHove_HighlightHealing
    // on CardMovement.cs -- onHover.event -> check CardProperty the booleans above
    // if true -> swap attributeBG_faded by attributeBG_highlighted
}

