using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] GameStates GameStates;
    [SerializeField] CreatureSelection_p CreatureSelection_p;

    public void EndTurn()
    {
        if(GameStates.currentGameState == GameStates.GameState.PlayerTurn)
        {
            GameStates.currentGameState = GameStates.GameState.PlayerEndTurn;
        }

        CreatureSelection_p.ResetAvailableCreatureSelection();
    }
}
