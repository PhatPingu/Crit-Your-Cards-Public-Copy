using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "GameObjects/GenerateCreature")]
public class Creature_ScriptableObj : ScriptableObject
{
    public Sprite creatureImage;
    public Sprite creatureImage_dessaturated;
    public Sprite creatureImage_dead;
    public Sprite creatureImage_sleeping;
    public Sprite creatureImage_hidden;
    public Sprite creatureImageMod_taunting;
    public Sprite creatureImageMod_tauntingHidden;
    public Sprite creatureImage_shielded;
    
    public string creatureName;
    public int creatureMaxHealth;
    public int creatureAttack;
    public int creatureHeal;
    public int creatureDefense;
    public int creatureLevel;
    public GameReferences.CreatureRareness creatureRareness;

    public GameReferences.FamilyColor creatureColor;

    //List<Card_ScriptableObj> creatureCards_collection;
    public List<Card_ScriptableObj> creatureCards_starter;
    public List<Card_ScriptableObj> creatureCards_obtainable;
    public List<Card_ScriptableObj> creatureCards_available;
    public List<Card_ScriptableObj> creatureCards_selectedForUse;
}
