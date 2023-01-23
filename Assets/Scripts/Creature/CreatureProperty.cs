using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureProperty : MonoBehaviour
{
    public Creature_ScriptableObj creatureData;
    [SerializeField] CreatureBuffDebuffManager CreatureBuffDebuffManager;
    public int creatureTeamPosition;
    
    public Image creatureImage;
    public Image creatureImageTaunt;
    public Image creatureImageShielded;
    [SerializeField] Sprite transparentSprite;
    
    bool imageNotDesaturated = true;

    CreatureSelection_p CreatureSelection_p;
    CreatureSelection_e CreatureSelection_e;

    [SerializeField] RectTransform creatureDisplay, DMG_BG, health_fill;
    [SerializeField] RectTransform retaliateDamageText, burnDamageText, attackText, defenseText, damageText ;

    [SerializeField] TextMeshProUGUI creatureName;
    [SerializeField] TextMeshProUGUI creatureHealthDisplay, creatureHealthDisplaySeparator, creatureMaxHealthDisplay;
    [SerializeField] TextMeshProUGUI creatureDefenseDisplay;
    [SerializeField] TextMeshProUGUI creatureAttackDisplay;

    public int currentCreatureMaxHealth;
    public int currentCreatureHealth;
    public int currentCreatureAttack;
    public int currentCreatureHeal;
    public int currentCreatureDefense;

    int deadCounter = 1;
    
    public GameReferences.TeamSide TeamSide;
    public GameReferences.FamilyColor creatureColor;

    void Start()
    {
        GameObject _gameManager = GameObject.Find("Game Manager");
        CreatureSelection_p = _gameManager.GetComponent<CreatureSelection_p>();
        CreatureSelection_e = _gameManager.GetComponent<CreatureSelection_e>();

        UpdateSpriteOrientation();
    }
    
    void UpdateSpriteOrientation() // This needs UPDATE
    {
        if(TeamSide == GameReferences.TeamSide.Player)
        {
            creatureDisplay.rotation    = new Quaternion(0f, 180f, 0f, 0f);
            DMG_BG.rotation             = new Quaternion(0f, 180f, 0f, 0f);
            health_fill.rotation        = new Quaternion(0f, 180f, 0f, 0f);

            creatureName.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
            creatureHealthDisplay.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f); //this is alread flipped, since the healthbar is flipped
            creatureHealthDisplaySeparator.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f); //this is alread flipped, since the healthbar is flipped
            creatureMaxHealthDisplay.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f); //this is alread flipped, since the healthbar is flipped
            creatureDefenseDisplay.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
            creatureAttackDisplay.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);

            attackText.localRotation    = new Quaternion(0f, 180f, 0f, 0f);
            defenseText.localRotation   = new Quaternion(0f, 180f, 0f, 0f);
            
            var healthPos = creatureHealthDisplay.rectTransform.anchoredPosition; 
            creatureHealthDisplay.rectTransform.anchoredPosition = new Vector2(-healthPos.x, healthPos.y);
            
            var maxHealthPos = creatureMaxHealthDisplay.rectTransform.anchoredPosition;
            creatureMaxHealthDisplay.rectTransform.anchoredPosition = new Vector2(-maxHealthPos.x, maxHealthPos.y);
        }
    }

    public void UpdateCreatureProperty()
    {
        currentCreatureMaxHealth    = creatureData.creatureMaxHealth;
        currentCreatureHealth       = currentCreatureMaxHealth;
        currentCreatureAttack       = creatureData.creatureAttack;
        currentCreatureHeal         = creatureData.creatureHeal;
        currentCreatureDefense      = creatureData.creatureDefense;
        
        creatureColor               = creatureData.creatureColor;
    }

    void FixedUpdate()
    {
        UpdateCreaturePropertyDisplay();
        CheckIfDead();
        ResetHealthToBoundries();

        void ResetHealthToBoundries()
        {
            if(currentCreatureHealth > currentCreatureMaxHealth)
                currentCreatureHealth = currentCreatureMaxHealth;

            if(currentCreatureHealth < 0)
                currentCreatureHealth = 0;
        }
    }
    
    public void UpdateCreaturePropertyDisplay()
    {
        creatureImage.sprite            = CreatureImage();
        creatureImageTaunt.sprite       = CreatureImageTaunt();
        creatureImageShielded.sprite    = CreatureImageShielded();

        creatureName.text               = creatureData.creatureName;
        creatureHealthDisplay.text      = currentCreatureHealth.ToString();
        creatureMaxHealthDisplay.text   = currentCreatureMaxHealth.ToString();
        creatureDefenseDisplay.text     = currentCreatureDefense.ToString();
        creatureAttackDisplay.text      = currentCreatureAttack.ToString();    
    }

//------------------------------------------------------------------------------------------------
    Sprite CreatureImage()
    {
        if (CreatureBuffDebuffManager.hidden == true)
            return creatureData.creatureImage_hidden;
        if(imageNotDesaturated && !IsSleep())
            return creatureData.creatureImage;
        else if(!imageNotDesaturated && !IsSleep())
            return creatureData.creatureImage_dessaturated;
        else if (IsSleep())
            return creatureData.creatureImage_sleeping;    
        else
            return creatureData.creatureImage_dead;
    }
    public void SetDesaturateImage(bool choice) {imageNotDesaturated = !choice;}

    Sprite CreatureImageTaunt()
    {
        if (CreatureBuffDebuffManager.taunting && CreatureBuffDebuffManager.hidden)
            return creatureData.creatureImageMod_tauntingHidden;
        else if (CreatureBuffDebuffManager.taunting == true)
            return creatureData.creatureImageMod_taunting;
        else
            return transparentSprite;
    } 

    Sprite CreatureImageShielded()
    {
        if(CreatureBuffDebuffManager.shielded)
            return creatureData.creatureImage_shielded;
        else
            return transparentSprite;
    }
        

//------------------------------------------------------------------------------------------------
    public void CheckIfDead()
    {
        if(IsDead() && deadCounter > 0)
        {
            deadCounter--;

            if(TeamSide == GameReferences.TeamSide.Player)
                CreatureSelection_p.ResetAvailableCreatureSelection();
            else
                CreatureSelection_e.ResetAvailableCreatureSelection();
        }
    }
    
    public bool IsDead()
    {
        if(currentCreatureHealth <= 0)
        {
            creatureImage.sprite = creatureData.creatureImage_dead;
            return true;  
        } 
        else
            return false;
    }

    public bool IsSleep()
    {
        if(CreatureBuffDebuffManager.sleepTick > 0)
            return true;  
        else
        {
            transform.GetComponent<CreatureBuffDisplay>().ShowSleep(false);
            return false;
        }
    }

    public void SetHidden(bool choice)
    {
        CreatureBuffDebuffManager.hidden = choice;
        CreatureBuffDebuffManager.hiddenJustApplied = choice;
        transform.GetComponent<CreatureBuffDisplay>().ShowHidden(choice);
    }

    public void SetTaunt(bool choice)
    {
        CreatureBuffDebuffManager.taunting = choice;
        CreatureBuffDebuffManager.tauntJustApplied = choice;
        transform.GetComponent<CreatureBuffDisplay>().ShowTaunt(choice);
    }

    public void SetShielded(bool choice)
    {
        CreatureBuffDebuffManager.shielded = choice;
        transform.GetComponent<CreatureBuffDisplay>().ShowShielded(choice);
    }
}
