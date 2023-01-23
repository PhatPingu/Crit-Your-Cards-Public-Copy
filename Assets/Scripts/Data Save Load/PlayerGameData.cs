using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameData : MonoBehaviour
{
    public static PlayerGameData s_data;

    [SerializeField] Data_GameLibrary Data_GameLibrary;

    public List<Creature_ScriptableObj> creatureCollection_notConverted;
    public List<Creature_ScriptableObj> creatureCollection_convertedNotActive;
    public List<Creature_ScriptableObj> creatureCollection_activeTeam;

    public List<Card_ScriptableObj> activeDeck_list;

    [SerializeField] GameObject[] RosterCritPanel_array;

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

    void Start()
    {
        UpdateActiveDeckList();
    }

    public void CreatureCollection_NewGame()
    {
        creatureCollection_notConverted.Clear(); 
        creatureCollection_convertedNotActive.Clear();
        creatureCollection_activeTeam.Clear();

        for (int i = 0; i < 3; i++)
        {
            creatureCollection_activeTeam.Add(Data_GameLibrary.creatureLibrary_array[i]);
            Data_GameLibrary.i_creatureLibrary_AvailableForUsed.Remove(i);
        }
    }

    public void SaveCreatureCollections()
    {
        SaveSystem.SetInt("creatureCollection_notConverted_Count", creatureCollection_notConverted.Count);
        SaveSystem.SetInt("creatureCollection_convertedNotActive_Count", creatureCollection_convertedNotActive.Count);
        SaveSystem.SetInt("creatureCollection_activeTeam_Count", creatureCollection_activeTeam.Count);

        for (int i = 0; i < creatureCollection_notConverted.Count; i++)
        {
            SaveSystem.SetInt("creatureCollection_notConverted" + i, System.Array.IndexOf(Data_GameLibrary.creatureLibrary_array, creatureCollection_notConverted[i]));
        }

        for (int i = 0; i < creatureCollection_convertedNotActive.Count; i++)
        {
            SaveSystem.SetInt("creatureCollection_convertedNotActive" + i, System.Array.IndexOf(Data_GameLibrary.creatureLibrary_array, creatureCollection_convertedNotActive[i]));
        }

        for (int i = 0; i < creatureCollection_activeTeam.Count; i++)
        {
            SaveSystem.SetInt("creatureCollection_activeTeam" + i, System.Array.IndexOf(Data_GameLibrary.creatureLibrary_array, creatureCollection_activeTeam[i]));
            Debug.Log("creatureCollection_activeTeam" + i + " == " +  System.Array.IndexOf(Data_GameLibrary.creatureLibrary_array, creatureCollection_activeTeam[i]));
        }
    }

    public void LoadCreatureCollections()
    {
        var notConverted_Count = SaveSystem.GetInt("creatureCollection_notConverted_Count");
        var convertedNotActive_Count = SaveSystem.GetInt("creatureCollection_convertedNotActive_Count");
        var activeTeam_Count = SaveSystem.GetInt("creatureCollection_activeTeam_Count");

        creatureCollection_notConverted.Clear(); 
        for (int i = 0; i < notConverted_Count; i++)
        {
            string creatureCollection_notConverted_i = "creatureCollection_notConverted" + i;
            if (SaveSystem.HasKey(creatureCollection_notConverted_i))
            {
                creatureCollection_notConverted.Add(Data_GameLibrary.creatureLibrary_array[SaveSystem.GetInt(creatureCollection_notConverted_i)]);
            }
        }

        creatureCollection_convertedNotActive.Clear();
        for (int i = 0; i < convertedNotActive_Count; i++)
        {
            string creatureCollection_convertedNotActive_i = "creatureCollection_convertedNotActive" + i;
            if (SaveSystem.HasKey(creatureCollection_convertedNotActive_i))
            {
                creatureCollection_convertedNotActive.Add(Data_GameLibrary.creatureLibrary_array[SaveSystem.GetInt(creatureCollection_convertedNotActive_i)]);
            }
        }

        creatureCollection_activeTeam.Clear();
        for (int i = 0; i < activeTeam_Count; i++)
        {
            string creatureCollection_activeTeam_i = "creatureCollection_activeTeam" + i;
            Debug.Log(creatureCollection_activeTeam_i + " == " + SaveSystem.GetInt(creatureCollection_activeTeam_i));
            if (SaveSystem.HasKey(creatureCollection_activeTeam_i))
            {
                creatureCollection_activeTeam.Add(Data_GameLibrary.creatureLibrary_array[SaveSystem.GetInt(creatureCollection_activeTeam_i)]);
            }
        }

    }

    void UpdateRosterArray()
    {
        for (int i = 0; i < RosterCritPanel_array.Length; i++)
        {
            RosterCritPanel_array[i].SetActive(true);
            RosterCritPanel_array[i].GetComponent<Town_CreaturePanelManager>().UpdateCreatureData();
        }
    }

    public void UpdateActiveDeckList()
    {
        activeDeck_list = new List<Card_ScriptableObj>();

        foreach (var creature in creatureCollection_activeTeam)
        {
            for (int i = 0; i < creature.creatureCards_available.Count; i++)
            {
                activeDeck_list.Add(creature.creatureCards_available[i]);
            }
        }
    }





    public void TestAddCrit()
    {
        creatureCollection_convertedNotActive.Add(creatureCollection_convertedNotActive[0]);
        UpdateRosterArray();
    }
}
