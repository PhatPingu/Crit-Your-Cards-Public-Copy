using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMap_PositionSetup : MonoBehaviour
{
    [SerializeField] SubMap_Manager SubMap_Manager;
    public Image image;
    
    [TextArea] public string Notes = "Index 0 of currentPositionSprite is always sprite = playerOnPosition";
    public Sprite[] currentPositionSprite;
    
    public GameObject[] nextPossiblePositions;
    public GameObject[] previousPossiblePositions;
    public GameObject[] pathFromPositions;
    
    public bool isPossibleAttack;

    public int positionIndex;



    void Start()
    {
        image = GetComponent<Image>();
        positionIndex = transform.GetSiblingIndex();
        SetPositionPossibilities();
        TogglePathsOff();
    }

    public void SelectForAttack() //Button Call
    {
        if(isPossibleAttack)
        {
            SubMap_Manager.selectedForAttack = positionIndex;
            
            SubMap_Manager.ToggleAllPositionPaths_off();
            for (int i = 0; i < previousPossiblePositions.Length; i++)
            {
                int i_positionIndex = previousPossiblePositions[i].GetComponent<SubMap_PositionSetup>().positionIndex;
                if(SubMap_Manager.currentPlayerPosition != i_positionIndex)
                {
                    pathFromPositions[i].GetComponent<Image>().enabled = false;
                }
                else
                {
                    pathFromPositions[i].GetComponent<Image>().enabled = true;
                    
                }
            }
        }
    }

    public void SetPositionPossibilities() // Called on SubMap_Manager
    {
        if(nextPossiblePositions.Length > 0)
        {
            if(SubMap_Manager.currentPlayerPosition != positionIndex) // NOTE: Order of check matters!
            {
                foreach(var position in nextPossiblePositions)
                {
                    position.GetComponent<SubMap_PositionSetup>().isPossibleAttack = false;
                }
            }
            else
            {
                foreach(var position in nextPossiblePositions)
                {
                    position.GetComponent<SubMap_PositionSetup>().isPossibleAttack = true;
                }
            }
        }
    }

    public void TogglePathsOff()
    {
        foreach(var path in pathFromPositions)
        {
            path.GetComponent<Image>().enabled = false;
        }
    }
}
