using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad_Manager : MonoBehaviour
{
    [SerializeField] Data_GameLibrary Data_GameLibrary;
    [SerializeField] PlayerGameData PlayerGameData;
    [SerializeField] Town_WindowManager Town_WindowManager;

    public void NewGame()
    {
        Data_GameLibrary.CreateNewLibrarynInstance();
        PlayerGameData.CreatureCollection_NewGame();
        Town_WindowManager.OpenTownScene(true);
    }
    
    public void SaveGame()
    {
        File.Delete(SaveSystem.GetPath());
        SaveSystem.data = new DataState();

        Data_GameLibrary.SaveCreatureLibrary();
        PlayerGameData.SaveCreatureCollections();
    }

    public void LoadGame() // Order of loading matters
    {
        NewGame();
        Data_GameLibrary.LoadCreatureLibrary();
        PlayerGameData.LoadCreatureCollections();
        //Load last Scene?
    }
}
