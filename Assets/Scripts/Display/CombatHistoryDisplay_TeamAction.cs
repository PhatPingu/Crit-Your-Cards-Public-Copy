using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHistoryDisplay_TeamAction : MonoBehaviour
{
    [SerializeField] SpriteRenderer actionColor;
    [SerializeField] Color playerColor;
    [SerializeField] Color enemyColor;
    [SerializeField] GameObject arrowIndication;
    
    [SerializeField] Sprite multiCastArrow_Right;
    [SerializeField] Sprite selfCastArrow_Down;
    [SerializeField] Sprite singleCastArrow_Right;

    
    public void SetTeamActionDisplay(GameReferences.TeamSide casterTeamSide, GameReferences.AllowedTarget targetType, GameReferences.TeamSide targetTeam )
    {
        var right   = new Quaternion(0,0,0,0);
        var left    = new Quaternion(0,180,0,0);
        var down    = new Quaternion(0,0,0,0);
        var up      = new Quaternion(180,0,0,0);

        // Player ----------------------------------------------------------------
        if     (casterTeamSide == GameReferences.TeamSide.Player && targetType == GameReferences.AllowedTarget.Allies)
        {
            actionColor.color = playerColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = multiCastArrow_Right;
            arrowIndication.transform.localRotation = left;
        }
        else if(casterTeamSide == GameReferences.TeamSide.Player && targetType == GameReferences.AllowedTarget.Ally)
        {
            actionColor.color = playerColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = singleCastArrow_Right;
            arrowIndication.transform.localRotation = left;
        }
        else if(casterTeamSide == GameReferences.TeamSide.Player && targetType == GameReferences.AllowedTarget.Opponents)
        {
            actionColor.color = playerColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = multiCastArrow_Right;
            arrowIndication.transform.localRotation = right;
        }
        else if(casterTeamSide == GameReferences.TeamSide.Player && targetType == GameReferences.AllowedTarget.Opponent)
        {
            actionColor.color = playerColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = singleCastArrow_Right;
            arrowIndication.transform.localRotation = right;
        }
        else if(casterTeamSide == GameReferences.TeamSide.Player && targetType == GameReferences.AllowedTarget.Self)
        {
            actionColor.color = playerColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = selfCastArrow_Down;
            arrowIndication.transform.localRotation = down;
        } 
        
        // Enemy ----------------------------------------------------------------
        else if(casterTeamSide == GameReferences.TeamSide.Enemy && targetType == GameReferences.AllowedTarget.Allies)
        {
            actionColor.color = enemyColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = multiCastArrow_Right;
            arrowIndication.transform.localRotation = right;
        }
        else if(casterTeamSide == GameReferences.TeamSide.Enemy && targetType == GameReferences.AllowedTarget.Ally)
        {
            actionColor.color = enemyColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = singleCastArrow_Right;
            arrowIndication.transform.localRotation = right;
        }
        else if(casterTeamSide == GameReferences.TeamSide.Enemy && targetType == GameReferences.AllowedTarget.Opponents)
        {
            actionColor.color = enemyColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = multiCastArrow_Right;
            arrowIndication.transform.localRotation = left;
        }
        else if(casterTeamSide == GameReferences.TeamSide.Enemy && targetType == GameReferences.AllowedTarget.Opponent)
        {
            actionColor.color = enemyColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = singleCastArrow_Right;
            arrowIndication.transform.localRotation = left;
        }
        else if(casterTeamSide == GameReferences.TeamSide.Enemy && targetType == GameReferences.AllowedTarget.Self)
        {
            actionColor.color = enemyColor;
            arrowIndication.GetComponent<SpriteRenderer>().sprite = selfCastArrow_Down;
            arrowIndication.transform.localRotation = up;
        }

        // Any ----------------------------------------------------------------
        else if (targetType == GameReferences.AllowedTarget.AnySingle)
        {
            if      (casterTeamSide == GameReferences.TeamSide.Player && targetTeam == GameReferences.TeamSide.Player)
            {
                actionColor.color = playerColor;
                arrowIndication.GetComponent<SpriteRenderer>().sprite = singleCastArrow_Right;
                arrowIndication.transform.localRotation = left;
            }
            else if (casterTeamSide == GameReferences.TeamSide.Player && targetTeam == GameReferences.TeamSide.Enemy)
            {
                actionColor.color = playerColor;
                arrowIndication.GetComponent<SpriteRenderer>().sprite = singleCastArrow_Right;
                arrowIndication.transform.localRotation = right;
            }
            else if (casterTeamSide == GameReferences.TeamSide.Enemy && targetTeam == GameReferences.TeamSide.Enemy)
            {
                actionColor.color = enemyColor;
                arrowIndication.GetComponent<SpriteRenderer>().sprite = singleCastArrow_Right;
                arrowIndication.transform.localRotation = right;
            }
            else if (casterTeamSide == GameReferences.TeamSide.Enemy && targetTeam == GameReferences.TeamSide.Player)
            {
                actionColor.color = enemyColor;
                arrowIndication.GetComponent<SpriteRenderer>().sprite = singleCastArrow_Right;
                arrowIndication.transform.localRotation = left;
            }
        }
        else if (targetType == GameReferences.AllowedTarget.AnyMany)
        {
            if      (casterTeamSide == GameReferences.TeamSide.Player && targetTeam == GameReferences.TeamSide.Player)
            {
                actionColor.color = playerColor;
                arrowIndication.GetComponent<SpriteRenderer>().sprite = multiCastArrow_Right;
                arrowIndication.transform.localRotation = left;
            }
            else if (casterTeamSide == GameReferences.TeamSide.Player && targetTeam == GameReferences.TeamSide.Enemy)
            {
                actionColor.color = playerColor;
                arrowIndication.GetComponent<SpriteRenderer>().sprite = multiCastArrow_Right;
                arrowIndication.transform.localRotation = right;
            }
            else if (casterTeamSide == GameReferences.TeamSide.Enemy && targetTeam == GameReferences.TeamSide.Enemy)
            {
                actionColor.color = enemyColor;
                arrowIndication.GetComponent<SpriteRenderer>().sprite = multiCastArrow_Right;
                arrowIndication.transform.localRotation = right;
            }
            else if (casterTeamSide == GameReferences.TeamSide.Enemy && targetTeam == GameReferences.TeamSide.Player)
            {
                actionColor.color = enemyColor;
                arrowIndication.GetComponent<SpriteRenderer>().sprite = multiCastArrow_Right;
                arrowIndication.transform.localRotation = left;
            }
        }
        else
        {
            Debug.LogError("DeveloperError: History Action Arrow can not be set!");
        }
    }

}
