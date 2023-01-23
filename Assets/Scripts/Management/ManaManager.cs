using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [SerializeField] SFX_Manager SFX_Manager;
    [SerializeField] GameObject p_ManaArea;
    [SerializeField] GameObject e_ManaArea;    

    [SerializeField] GameObject[] p_ManaIcons;
    [SerializeField] GameObject[] e_ManaIcons;
    [SerializeField] int startMana;
    [SerializeField] int maxMana;
    [SerializeField] int currentMaxMana_player;
    [SerializeField] int currentMaxMana_enemy;
    
    [SerializeField] Sprite UI_ManaPurple, UI_ManaSpentPreview;

    public int currentMana_player;
    public int currentMana_enemy;

    void Start()
    {
        currentMana_player = startMana;
        currentMana_enemy = startMana;
        currentMaxMana_player = startMana;
        currentMaxMana_enemy = startMana;
        UpdateManaDisplay();
    }

    public void PreviewManaUse(bool choice, int cardManaCost = 0) 
    {
        if (choice && cardManaCost <= currentMana_player)
        {
            int p_manaRemaining = currentMana_player - cardManaCost;
            int p_currenteMana = currentMana_player;
            for (int i = 0; i < currentMaxMana_player; i++)
            {
                if(p_manaRemaining > 0)
                {
                    p_ManaIcons[i].GetComponent<Image>().sprite = UI_ManaPurple;
                    p_manaRemaining--;
                    p_currenteMana --;
                }
                else if(p_currenteMana > 0)
                {
                    p_ManaIcons[i].GetComponent<Image>().sprite = UI_ManaSpentPreview;
                    p_currenteMana --;
                }
                else
                {
                    p_ManaIcons[i].GetComponent<Image>().sprite = UI_ManaPurple;
                    p_ManaIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                }
            }
        }
        else
        {
            UpdateManaDisplay();
        }
    }

    void UpdateManaDisplay()
    {
        int p_count    = currentMana_player;
        int p_countMax = currentMaxMana_player;
        for (int i = 0; i < p_ManaIcons.Length; i++)
        {
            p_ManaIcons[i].GetComponent<Image>().sprite = UI_ManaPurple;

            if(p_count > 0)
            {
                p_ManaIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                p_count--;
                p_countMax --;
            }
            else if(p_countMax > 0)
            {
                p_ManaIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                p_countMax --;
            }
            else
            {
                p_ManaIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
            }
        }

        int e_count    = currentMana_enemy;
        int e_countMax = currentMaxMana_enemy;
        for (int i = 0; i < e_ManaIcons.Length; i++)
        {
            if(e_count > 0)
            {
                e_ManaIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                e_count--;
                e_countMax--;
            }
            else if(e_countMax > 0)
            {
                e_ManaIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                e_countMax--;
            }
            else
            {
                e_ManaIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
            }
        }
    }

    public bool UseMana_Player(int amount)
    {
        if(amount > currentMana_player) 
        {
            Debug.Log("Not enough mana");
            SFX_Manager.audioPlayer.PlayOneShot(SFX_Manager.sfx_notEnoughMana);
            return false;
        }
        else 
        {
            currentMana_player -= amount;
            UpdateManaDisplay();

            Debug.Log("PLAYER Mana Used. Can cast card");
            return true;
        }
    }

    public bool UseMana_Enemy(int amount)
    {
        if(amount > currentMana_enemy) 
        {
            return false;
        }
        else 
        {
            currentMana_enemy -= amount;
            UpdateManaDisplay();

            Debug.Log("ENEMY Mana Used. Cast card");
            return true;
        }
    }

    public void GrowMana_Player(int amount)
    {
        currentMana_player = currentMaxMana_player + amount;
        currentMaxMana_player += amount;
        if(currentMana_player > maxMana)
        {
            currentMana_player = maxMana;
        }
        UpdateManaDisplay();
    }

    public void GrowMana_Enemy(int amount)
    {
        currentMana_enemy = currentMaxMana_enemy + amount;
        currentMaxMana_enemy += amount;
        if(currentMana_enemy > maxMana)
        {
            currentMana_enemy = maxMana;
        }
        UpdateManaDisplay();
    }

    public void GainMana_Player(int amount)
    {
        currentMana_player += amount;
        if( currentMana_player > currentMaxMana_player)
        {
            currentMana_player = currentMaxMana_player;
        }

        UpdateManaDisplay();
    }

    public void GainMana_Enemy(int amount)
    {
        currentMana_enemy += amount;
        if( currentMana_enemy > currentMaxMana_enemy)
        {
            currentMana_enemy = currentMaxMana_enemy;
        }

        UpdateManaDisplay();
    }

    public void DisplayPlayerManaOverEnemy(bool choice)
    {
        p_ManaArea.SetActive(choice);
        e_ManaArea.SetActive(!choice);
    }

    public bool CheckMana(int amount, GameReferences.TeamSide teamSide)
    {
        if(amount <= currentMana_player && teamSide == GameReferences.TeamSide.Player
        || amount <= currentMana_enemy  && teamSide == GameReferences.TeamSide.Enemy) 
            return true;
        else
            return false;
    }
 
}
