using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMap_Manager : MonoBehaviour
{
    public GameObject[] positions;

    public int currentPlayerPosition;
    public int selectedForAttack;

    void Start()
    {
        selectedForAttack = currentPlayerPosition;
        DisplayPlayerOnCurrentPosition();
    }

    public void ToggleAllPositionPaths_off()
    {
        foreach (var position in positions)
        {
            position.GetComponent<SubMap_PositionSetup>().TogglePathsOff();
        }
    }

    public void MovePlayerToPosition(int positionIndex) // NOTE: Order of methods matters!
    {
        SetplayerPosition(positionIndex);
        DisplayPlayerOnCurrentPosition();
    }

    void SetplayerPosition(int positionIndex)
    {
        currentPlayerPosition = positionIndex;
    }

    void DisplayPlayerOnCurrentPosition()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            var PositionSetup = positions[i].GetComponent<SubMap_PositionSetup>();
            
            PositionSetup.image.sprite = PositionSetup.currentPositionSprite[1];
            if(PositionSetup.positionIndex == currentPlayerPosition)
            {
                PositionSetup.image.sprite = PositionSetup.currentPositionSprite[0];
            }
            
            PositionSetup.SetPositionPossibilities();
        }
    }


}
