using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreaturePositioningManager : MonoBehaviour
{
    [SerializeField] GameStates GameStates;
    [SerializeField] CreatureSelection_p CreatureSelection_p;
    [SerializeField] CreatureSelection_e CreatureSelection_e;

    public void SetCreatureOnAttackPosition(GameObject SelectedCreature)
    {
        foreach(var creature in CreatureSelection_p.p_creatureList)
        {
            creature.GetComponent<LayoutElement>().minWidth = 0;    
        }

        foreach(var creature in CreatureSelection_e.e_creatureList)
        {
            creature.GetComponent<LayoutElement>().minWidth = 0;    
        }

        if(GameStates.currentGameState == GameStates.GameState.PlayerTurn
        && SelectedCreature.GetComponent<CreatureProperty>().TeamSide == GameReferences.TeamSide.Player)
        {
            SelectedCreature.transform.SetAsLastSibling();
            SelectedCreature.GetComponent<LayoutElement>().minWidth = 100;
        }

        if(GameStates.currentGameState == GameStates.GameState.EnemyTurn
        && SelectedCreature.GetComponent<CreatureProperty>().TeamSide == GameReferences.TeamSide.Enemy)
        {
            SelectedCreature.transform.SetAsLastSibling();
            SelectedCreature.GetComponent<LayoutElement>().minWidth = 100;
        }
    }
    
}
