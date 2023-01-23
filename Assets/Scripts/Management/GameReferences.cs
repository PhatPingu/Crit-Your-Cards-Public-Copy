using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReferences : MonoBehaviour
{
    public GameObject PlayerDeck;
    public GameObject PlayerGrave;
    
    public GameObject EnemyDeck;
    public GameObject EnemyGrave;
    public GameObject EnemyHand;

    public GameObject p_Creature_01;
    public GameObject p_Creature_02;
    public GameObject p_Creature_03;

    public GameObject e_Creature_01;
    public GameObject e_Creature_02;
    public GameObject e_Creature_03;
    
    public GameObject[] p_Creature_array;
    public GameObject[] e_Creature_array;
    public GameObject[] all_Creature_array;


    public GameObject Display_CardCast;

    public enum TeamSide
    {
        Player, Enemy, None
    }

    public enum AllowedTarget
    {
        Ally, Allies, Opponent, Opponents, AnySingle, AnyMany, Self
    }

    public enum FamilyColor 
    {
        Black, Blue, Green, Red, White,
        Neutral
    }

    public enum StrategyTypes // TODO: Add Heal
    {
        Attack, Defense, Any, Self, EndTurn
    }

    public enum CreatureRareness
    {
        Common, Rare, Epic, Legendary
    }

}
