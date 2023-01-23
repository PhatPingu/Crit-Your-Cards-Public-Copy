using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMap_AttackButton : MonoBehaviour
{
    [SerializeField] SubMap_Manager SubMap_Manager;

    public void LaunchAttack()
    {
        //Start Attack 
        //Launch combat Scene
        //Load Data from index == SubMap_Manager.selectedForAttack

        if(SubMap_Manager.currentPlayerPosition == SubMap_Manager.selectedForAttack)
        {
            Debug.Log("You need to select a Village first!");
        }
        else
        {
            Debug.Log("Attack was launched!!!");
            SceneManager.LoadScene("Combat");
        }

    }
    
}
