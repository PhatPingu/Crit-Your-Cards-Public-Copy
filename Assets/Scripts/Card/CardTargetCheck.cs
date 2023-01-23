using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTargetCheck : MonoBehaviour
{ 
    CardProperty CardProperty;
    CreatureSelection_p CreatureSelection_p;
    CreatureSelection_e CreatureSelection_e;
    CardCastManager CardCastManager;
    CreatureTargetingManagement CreatureTargetingManagement;

    GameReferences.TeamSide castingTeam;
    public GameReferences.TeamSide targetTeam;

    void Start()
    {
        CardProperty = GetComponent<CardProperty>();
        CardCastManager = GetComponent<CardCastManager>();
        GameObject _gameManager = GameObject.Find("Game Manager");
        CreatureSelection_p = _gameManager.GetComponent<CreatureSelection_p>();
        CreatureSelection_e = _gameManager.GetComponent<CreatureSelection_e>();
        CreatureTargetingManagement = _gameManager.GetComponent<CreatureTargetingManagement>();
        castingTeam = CardProperty.teamSide;
    }

    public void OnTriggerStay2D(Collider2D other)
    {  
        if(castingTeam == GameReferences.TeamSide.Player)
        {
            targetTeam = other.GetComponent<CreatureProperty>().TeamSide;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        targetTeam = GameReferences.TeamSide.None;
    }

    void IfEnemy_SetEnemyTarget(GameObject target)
    {
        if(castingTeam == GameReferences.TeamSide.Enemy)
        {
            if(CardProperty.allowedTarget == GameReferences.AllowedTarget.Ally
            || CardProperty.allowedTarget == GameReferences.AllowedTarget.Allies)
            targetTeam = GameReferences.TeamSide.Enemy;
            else targetTeam = GameReferences.TeamSide.Player;
        }
    }



// ------------------------------------------------------------------------------------------------
    public bool CheckTargetAllowed(GameObject target, GameReferences.TeamSide castingTeam)  
    {
        if(CardProperty != null) CardProperty.UpdateCardProperty();
        IfEnemy_SetEnemyTarget(target);

        if(target == null) return false;
        if(target.GetComponent<CreatureProperty>().IsDead()) {Debug.Log("Target: " + target + " Is Dead: "); return false;}
        
        if(CardProperty.allowedTarget == GameReferences.AllowedTarget.Ally && target.tag == "Creature")
        {
            if     ( castingTeam == GameReferences.TeamSide.Player
            &&       targetTeam  == GameReferences.TeamSide.Player
            
            ||       castingTeam == GameReferences.TeamSide.Enemy
            &&       targetTeam  == GameReferences.TeamSide.Enemy)   
            {
                return true;
            }
            else
            {
                Debug.Log("Casting team: " + castingTeam + ", Target team: " + targetTeam + ", Card: " + gameObject + ", Target: " + target + ", Weird return 01: "); 
                return false; 
            }
        }
        else if(CardProperty.allowedTarget == GameReferences.AllowedTarget.Allies && target.tag == "Creature")
        {
            if     ( castingTeam == GameReferences.TeamSide.Player
            &&       targetTeam  == GameReferences.TeamSide.Player
            
            ||       castingTeam == GameReferences.TeamSide.Enemy
            &&       targetTeam  == GameReferences.TeamSide.Enemy)   
            {
                return true;
            }
            else
            {
                Debug.Log("Casting team: " + castingTeam + ", Target team: " + targetTeam + ", Card: " + gameObject + ", Target: " + target + ", Weird return 01: "); 
                return false; 
            }
        }
        else if(CardProperty.allowedTarget == GameReferences.AllowedTarget.Opponent && target.tag == "Creature")
        {
            if     ( castingTeam == GameReferences.TeamSide.Player
            &&       targetTeam  == GameReferences.TeamSide.Enemy
            
            ||       castingTeam == GameReferences.TeamSide.Enemy
            &&       targetTeam  == GameReferences.TeamSide.Player)   
            {
                if(castingTeam == GameReferences.TeamSide.Player) 
                {
                    if(CreatureTargetingManagement.IfTauntPresent_isTargetable(target)) // If Taunt present, is target.tauntin == true;
                        return CheckHidden();
                    else return false;
                }
                else // if TeamSide.Enemy
                {   return CheckHidden();}

                bool CheckHidden()
                {
                    if (target.GetComponent<CreatureBuffDebuffManager>().hidden == true)
                        return false;
                    else
                        return true;
                }
            }
            else
            {
                Debug.Log("Casting team: " + castingTeam + ", Target team: " + targetTeam + ", Card: " + gameObject + ", Target: " + target + ", Weird return 02: "); 
                return false; 
            }
        }
        else if(CardProperty.allowedTarget == GameReferences.AllowedTarget.Opponents && target.tag == "Creature")
        {
            if     ( castingTeam == GameReferences.TeamSide.Player
            &&       targetTeam  == GameReferences.TeamSide.Enemy
            
            ||       castingTeam == GameReferences.TeamSide.Enemy
            &&       targetTeam  == GameReferences.TeamSide.Player)   
            {
                return true;
            }
            else
            {
                Debug.Log("Casting team: " + castingTeam + ", Target team: " + targetTeam + ", Card: " + gameObject + ", Target: " + target + ", Weird return 02: "); 
                return false; 
            }
        }
        else if(CardProperty.allowedTarget == GameReferences.AllowedTarget.AnySingle && target.tag == "Creature"
            ||  CardProperty.allowedTarget == GameReferences.AllowedTarget.AnyMany && target.tag == "Creature" )
        {
            if     ( castingTeam == GameReferences.TeamSide.Player
            &&       targetTeam  == GameReferences.TeamSide.Enemy
            
            ||       castingTeam == GameReferences.TeamSide.Enemy
            &&       targetTeam  == GameReferences.TeamSide.Player)   
            {
                return true; //output this is enemy
            }
            else
            {
                return true; //output this is ally
            }
        }
        else if(CardProperty.allowedTarget == GameReferences.AllowedTarget.Self && target.tag == "Creature")
        {
            if     ( castingTeam == GameReferences.TeamSide.Player
            &&      CreatureSelection_p.currentSelectedCreature == target
            
            ||      castingTeam == GameReferences.TeamSide.Enemy
            &&      CreatureSelection_e.currentSelectedCreature == target) 
            {
                return true;
            }
            else
            {
                return false; //Not Self
            }
        }
        else Debug.LogError("Casting team: " + castingTeam + ", Target team: " + targetTeam + ", Card: " + gameObject + ", Target: " + target + ", Weird return 03: "); ; return false;
    }

    
    
}
