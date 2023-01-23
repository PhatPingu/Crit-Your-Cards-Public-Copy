using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Data_GameLibrary : MonoBehaviour
{
    public static Data_GameLibrary s_data;

    [SerializeField] PlayerGameData PlayerGameData;
    [SerializeField] SaveSystemSetup SaveSystemSetup;

    [TextArea] public string Notes = "The first 3 creatures in the list are the default active creatures in a New Game.";
    [SerializeField] 
    protected Creature_ScriptableObj[] original_creatureLibrary_array;
    public    Creature_ScriptableObj[] creatureLibrary_array;
    public List<int> i_creatureLibrary_AvailableForUsed = new List<int>();


    public Card_ScriptableObj[] original_cardLibrary_array;



    void Awake()
    {
        Singleton_PlayerGameData();
    }

    void Singleton_PlayerGameData()
    {
        if (s_data == null)
        {
            DontDestroyOnLoad(gameObject);
            s_data = this;
        }
        else
        {
            if(s_data !=this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void CreateNewLibrarynInstance()
    {
        creatureLibrary_array = original_creatureLibrary_array;
        for (int i = 0; i < creatureLibrary_array.Length; i++)
        {
            var newInstance = Instantiate(original_creatureLibrary_array[i]);
            creatureLibrary_array[i] = newInstance;    
            i_creatureLibrary_AvailableForUsed.Add(i);
        }
    }

    public void SaveCreatureLibrary()
    {
        for (int i = 0; i < creatureLibrary_array.Length; i++)
        {
            var creature = creatureLibrary_array[i];

            SaveSystem.SetInt("creatureMaxHealth" + i, creature.creatureMaxHealth);
            SaveSystem.SetInt("creatureAttack" + i, creature.creatureAttack);
            SaveSystem.SetInt("creatureHeal" + i, creature.creatureHeal);
            SaveSystem.SetInt("creatureDefense" + i, creature.creatureDefense);
            SaveSystem.SetInt("creatureLevel" + i, creature.creatureLevel);

            SaveCreatureList(creature.creatureCards_starter, "creatureCard_starter_", i);
            SaveCreatureList(creature.creatureCards_obtainable, "creatureCards_obtainable_", i);
            SaveCreatureList(creature.creatureCards_available, "creatureCards_available_", i);
            SaveCreatureList(creature.creatureCards_selectedForUse, "creatureCards_selectedForUse_", i);
        }
       
        SaveSystem.SaveToDisk();

        void SaveCreatureList(List<Card_ScriptableObj> list, string prefixName, int i)
        {
            for (int j = 0; j < list.Count; j++)
            {
                SaveSystem.SetInt(prefixName + i + j, System.Array.IndexOf(original_cardLibrary_array, list[j]));
            }
        }
    }

    public void LoadCreatureLibrary()
    {
        for (int i = 0; i < creatureLibrary_array.Length; i++)
        {
            var creature = creatureLibrary_array[i];

            creature.creatureMaxHealth  = SaveSystem.GetInt("creatureMaxHealth" + i);
            creature.creatureAttack     = SaveSystem.GetInt("creatureAttack" + i);
            creature.creatureHeal       = SaveSystem.GetInt("creatureHeal" + i);
            creature.creatureDefense    = SaveSystem.GetInt("creatureDefense" + i);
            creature.creatureLevel      = SaveSystem.GetInt("creatureLevel" + i);

            LoadCreatureList(creature.creatureCards_starter, "creatureCard_starter_", i);
            LoadCreatureList(creature.creatureCards_obtainable, "creatureCards_obtainable_", i);
            LoadCreatureList(creature.creatureCards_available, "creatureCards_available_", i);
            LoadCreatureList(creature.creatureCards_selectedForUse, "creatureCards_selectedForUse_", i);
        }

        void LoadCreatureList(List<Card_ScriptableObj> list, string prefixName, int i)
        {
            list.Clear();
            for (int j = 0; j < 6 ; j++)
            {
                string creatureCard_list_ij = prefixName + i + j;

                if(SaveSystem.HasKey(creatureCard_list_ij))
                {
                    list.Add(original_cardLibrary_array[SaveSystem.GetInt(creatureCard_list_ij)]);
                }
            }

        }
    }



    
    //---- Test Methods ----------------------------------------------------
    public void TestIncreaseCreatureAttributes()
    {
        foreach (var creature in creatureLibrary_array)
        {
            creature.creatureMaxHealth += 10;
            creature.creatureAttack    += 10;
            creature.creatureHeal      += 10;
            creature.creatureDefense   += 10;
            creature.creatureLevel     += 10;

            creature.creatureCards_starter.Add(original_cardLibrary_array[3]);
        }

    }

}
