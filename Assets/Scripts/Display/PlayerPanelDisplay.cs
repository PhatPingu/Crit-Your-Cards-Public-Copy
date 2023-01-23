using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPanelDisplay : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI creatureHealth_text, creatureMaxHealth_text;
    [SerializeField] SpriteRenderer creatureImage;
    [SerializeField] SpriteRenderer teamColor_Stripe;
    [SerializeField] SpriteRenderer teamColor_Window;
    [SerializeField] TextMeshProUGUI creatureName;
    [SerializeField] TextMeshProUGUI creatureHeal;
    [SerializeField] TextMeshProUGUI creatureDefense;
    [SerializeField] TextMeshProUGUI creatureAttack;
    [SerializeField] Sprite[] teamColorTexture; //Black, Blue, Green, Red, White

    [SerializeField] SpriteRenderer heal_BG, defense_BG, attack_BG;
    [SerializeField] Sprite stat_BG_Faded, stat_BG_Highlighted;



    public void UpdatePlayerCreaturePanel(CreatureProperty CreatureProperty)
    {
        healthSlider.maxValue = CreatureProperty.currentCreatureMaxHealth;
        healthSlider.value = CreatureProperty.currentCreatureHealth;

        creatureHealth_text.text = CreatureProperty.currentCreatureHealth.ToString();
        creatureMaxHealth_text.text = CreatureProperty.currentCreatureMaxHealth.ToString();
        creatureImage.sprite = CreatureProperty.creatureData.creatureImage;
        SetTeamColor();
        creatureName.text = CreatureProperty.creatureData.creatureName.ToString();
        creatureHeal.text = CreatureProperty.currentCreatureHeal.ToString();
        creatureDefense.text = CreatureProperty.currentCreatureDefense.ToString();
        creatureAttack.text = CreatureProperty.currentCreatureAttack.ToString();

        void SetTeamColor()
        {
            if      (CreatureProperty.creatureData.creatureColor == GameReferences.FamilyColor.Black)
            {
                teamColor_Stripe.sprite = teamColorTexture[0];
                teamColor_Window.sprite = teamColorTexture[0];
            }
            else if (CreatureProperty.creatureData.creatureColor == GameReferences.FamilyColor.Blue)
            {
                teamColor_Stripe.sprite = teamColorTexture[1];
                teamColor_Window.sprite = teamColorTexture[1];
            }
            else if (CreatureProperty.creatureData.creatureColor == GameReferences.FamilyColor.Green)
            {
                teamColor_Stripe.sprite = teamColorTexture[2];
                teamColor_Window.sprite = teamColorTexture[2];
            }
            else if (CreatureProperty.creatureData.creatureColor == GameReferences.FamilyColor.Red)
            {
                teamColor_Stripe.sprite = teamColorTexture[3];
                teamColor_Window.sprite = teamColorTexture[3];
            }
            else
            {
                teamColor_Stripe.sprite = teamColorTexture[4];
                teamColor_Window.sprite = teamColorTexture[4];
            }
        }

    }
    
    public void Highlight_AttributeBG(bool Heal_BG = false, bool Defense_BG = false, bool Attack_BG = false)
    {
        if (Heal_BG)
            heal_BG.sprite = stat_BG_Highlighted;
        else
            heal_BG.sprite = stat_BG_Faded;

        if (Defense_BG)
            defense_BG.sprite = stat_BG_Highlighted;
        else
            defense_BG.sprite = stat_BG_Faded;
        
        if (Attack_BG)
            attack_BG.sprite = stat_BG_Highlighted;
        else
            attack_BG.sprite = stat_BG_Faded;
    }

}
