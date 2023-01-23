using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class CardDescription : MonoBehaviour
{
    public TextMeshProUGUI description;
    public CardProperty CardProperty;
    [SerializeField] int descriptionIndex;
    [SerializeField] GameObject[] descriptions;


    CardEffect_ScriptableObj CardEffect;
    CreatureSelection_p CreatureSelection_p;
    CreatureSelection_e CreatureSelection_e;
    CreatureProperty selectedCreatureProperty;
    
    public bool setSpecial;

    public bool descriptionOverwritten;

    public int CurrentCreatureAttack;
    int CurrentCreatureHeal;
    int CurrentCreatureDefense;
    int magicColorIconID;

    public string descriptionFromData;

    void Start()
    {
        CreatureSelection_p = GameObject.Find("Game Manager").GetComponent<CreatureSelection_p>();
        CreatureSelection_e = GameObject.Find("Game Manager").GetComponent<CreatureSelection_e>();
    }

    void Update()
    {
        if(CardProperty.teamSide == GameReferences.TeamSide.Player && CreatureSelection_p.currentSelectedCreature != null)
        {
            selectedCreatureProperty = CreatureSelection_p.currentSelectedCreature.GetComponent<CreatureProperty>();
            CurrentCreatureAttack    = selectedCreatureProperty.currentCreatureAttack;
            CurrentCreatureHeal      = selectedCreatureProperty.currentCreatureHeal;
            CurrentCreatureDefense   = selectedCreatureProperty.currentCreatureDefense;
        }
        else if(CardProperty.teamSide == GameReferences.TeamSide.Enemy && CreatureSelection_e.currentSelectedCreature != null)
        {
            selectedCreatureProperty = CreatureSelection_e.currentSelectedCreature.GetComponent<CreatureProperty>();
            CurrentCreatureAttack    = selectedCreatureProperty.currentCreatureAttack;
            CurrentCreatureHeal      = selectedCreatureProperty.currentCreatureHeal;
            CurrentCreatureDefense   = selectedCreatureProperty.currentCreatureDefense;
        }

        if(CardProperty.cardData != null)
        {
            OverrideText();            
        }
    }

    public virtual void OverrideText()
    {
        if(CardProperty.cardData.overrideescription == "")
        {
            SetDescriptionsActive();
            descriptionOverwritten = false;
            CardEffect = CardProperty.cardData.cardEffect[descriptionIndex];
            descriptionFromData = CardEffect.description;
            description.text = SetTextDescription();
        }
        else
        {
            if(descriptionIndex == 0)
            {
                descriptionOverwritten = true;
                descriptionFromData = CardProperty.cardData.overrideescription;
                description.text = SetTextDescription();
            }
            else
            {
                descriptionOverwritten = true;
                gameObject.SetActive(false);
            }
        }

        void SetDescriptionsActive()
        {
            foreach (var description in descriptions)
            {
                description.SetActive(true);
            }
        }
    }

    public string SetTextDescription()
    {
        var sb = new StringBuilder(descriptionFromData);

        sb.Replace("{{creatureAttack}}"     , (CurrentCreatureAttack.ToString()));
        sb.Replace("{{creatureHeal}}"       , (CurrentCreatureHeal.ToString()));
        sb.Replace("{{creatureDefense}}"    , (CurrentCreatureDefense.ToString()));

        if(descriptionOverwritten) 
        {
            var _CardEffect = CardProperty.cardData.cardEffect[0];
            sb.Replace("{{modifierAmount}}" , (_CardEffect.modifierAmount.ToString()));
        }
        else
        {
            sb.Replace("{{modifierAmount}}" , (CardEffect.modifierAmount.ToString()));
        }

        //sb.Replace("{{totalDamageAmount}}"       , (CurrentCreatureAttack + CardEffect.modifierAmount).ToString());
        //sb.Replace("{{totalHealAmount}}"         , (CurrentCreatureHeal + CardEffect.modifierAmount).ToString());

        
        sb.Replace("{{magicColor}}", "<sprite=\"MagicColors\" index="+ IconID() + ">");

        SetSpecialTextColor(setSpecial);

        return sb.ToString();
        
        void SetSpecialTextColor(bool choice)
        {
            if(choice)
            {
                sb.Replace("{{color}}"  , ("<color=green>"));
                sb.Replace("{{/color}}" , ("</color>"));
            }
            else
            {
                sb.Replace("{{color}}"  , (""));
                sb.Replace("{{/color}}" , (""));
            }
        }

        int IconID()
        {
            /*if      (CardProperty.cardColor == GameReferences.FamilyColor.Blue)
                return 0;
            else if (CardProperty.cardColor == GameReferences.FamilyColor.Red)
                return 1;
            else if (CardProperty.cardColor == GameReferences.FamilyColor.White)
                return 2;
            else if (CardProperty.cardColor == GameReferences.FamilyColor.Green)
                return 3;
            else if (CardProperty.cardColor == GameReferences.FamilyColor.Black)
                return 4;
            else
                return 5;*/

            return 8;
        }
    }

}
