using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_WindowManager : MonoBehaviour
{
    [SerializeField] GameObject roster;
    [SerializeField] GameObject gameMenu;
    [SerializeField] GameObject collectionScene;
    [SerializeField] GameObject townScene;
    [SerializeField] GameObject mapScene;
    [SerializeField] GameObject subMapPanel;

    GameObject[] localScenes;

    void Start() // !Important: Add all scenes here so the Open*Scene methods work
    {
        localScenes = new GameObject[]{gameMenu, collectionScene, townScene, mapScene, subMapPanel};
    }

    public void OpenGameMenu(bool choice) // Order of methods matter
    {
        TurnOffAllScenes(choice);
        gameMenu.SetActive(choice);
        roster.SetActive(!choice);
    }

    public void OpenCollectionScene(bool choice) // Order of methods matter
    {
        TurnOffAllScenes(choice);
        collectionScene.SetActive(choice);
        roster.SetActive(!choice);
    }
    public void OpenTownScene(bool choice) // Order of methods matter
    {
        TurnOffAllScenes(choice);
        townScene.SetActive(choice);
        roster.SetActive(choice);
    }

    public void OpenMapScene(bool choice) // Order of methods matter
    {
        TurnOffAllScenes(choice);
        mapScene.SetActive(choice);
        roster.SetActive(choice);
    }

    public void OpenSubMapPanel(bool choice) // Order of methods matter
    {
        TurnOffAllScenes(choice);
        mapScene.SetActive(choice); 
        subMapPanel.SetActive(choice); //Needs mapScene.ative
        roster.SetActive(choice);
    }

    void TurnOffAllScenes(bool choice)
    {
        foreach(var scene in localScenes)
        {
            scene.SetActive(!choice);
        }
    }


}
