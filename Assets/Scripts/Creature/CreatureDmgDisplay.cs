using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureDmgDisplay : MonoBehaviour
{
    [SerializeField] GameObject ui_bg_SpellDamage;
    [SerializeField] GameObject ui_bg_RetaliateDamage;
    [SerializeField] GameObject ui_bg_BurnDamage;
    [SerializeField] GameObject ui_bg_Attack;
    [SerializeField] GameObject ui_bg_Defense;
    [SerializeField] GameObject ui_bg_Damage;

    public TextMeshProUGUI spellDamage_text;
    public TextMeshProUGUI retaliateDamage_text;
    public TextMeshProUGUI burnDamage_text;
    public TextMeshProUGUI attack_text;
    public TextMeshProUGUI defense_text;
    public TextMeshProUGUI damage_text;

    public Sprite[] spellColor; // Blue, Red, White, Green, Black
    [SerializeField]Image creatureSpellColor;

    void Start()
    {
        SetAllDisplaysInactive();
    }

    public void DisplayAttackAndDefense(bool choice)
    {
        if(choice)
        {
            defense_text.text = GetComponent<CreatureProperty>().currentCreatureDefense.ToString();

            ui_bg_Attack.SetActive(true);
            Invoke("SetDefenseActive", 0.3f);
            Invoke("SetDamageActive", 0.7f);
            Invoke("SetAllDisplaysInactive", 2.5f);
        }
        else
        {
            CancelInvoke("SetDefenseActive");
            CancelInvoke("SetDamageActive");
            CancelInvoke("SetAllDisplaysInactive");
            SetAllDisplaysInactive();
        }
    }

    public void DisplaySpellDamage(GameReferences.FamilyColor spellColor)
    {
        SetSpellIcon(spellColor);
        ui_bg_SpellDamage.SetActive(true);
        Invoke("SetAllDisplaysInactive", 2.5f);
    }

    void SetSpellIcon(GameReferences.FamilyColor color) // Blue, Red, White, Green, Black
    {
        if(color == GameReferences.FamilyColor.Blue)
            creatureSpellColor.sprite = spellColor[0];
        else if(color == GameReferences.FamilyColor.Red)
            creatureSpellColor.sprite = spellColor[1];
        else if(color == GameReferences.FamilyColor.White)
            creatureSpellColor.sprite = spellColor[2];
        else if(color == GameReferences.FamilyColor.Green)
            creatureSpellColor.sprite = spellColor[3];
        else if(color == GameReferences.FamilyColor.Black)
            creatureSpellColor.sprite = spellColor[4];
        else
            creatureSpellColor.sprite = spellColor[5];
    }

    public void DisplayBurnDamage()
    {
        ui_bg_BurnDamage.SetActive(true);
        Invoke("SetAllDisplaysInactive", 2.5f);
    }

    public void DisplayRetaliateDamage()
    {
        ui_bg_RetaliateDamage.SetActive(true);
        Invoke("SetAllDisplaysInactive", 2.5f);
    }

    void SetDefenseActive()
    {
        ui_bg_Defense.SetActive(true);
    }

    void SetDamageActive()
    {
        ui_bg_Damage.SetActive(true);
    }

    void SetAllDisplaysInactive()
    {
        ui_bg_SpellDamage.SetActive(false);
        ui_bg_RetaliateDamage.SetActive(false);
        ui_bg_BurnDamage.SetActive(false);
        ui_bg_Attack.SetActive(false);
        ui_bg_Defense.SetActive(false);
        ui_bg_Damage.SetActive(false);
    }




}
