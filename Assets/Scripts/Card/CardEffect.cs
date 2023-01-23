using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    [SerializeField] CardProperty CardProperty;
    [SerializeField] CardCastManager CardCastManager;

    public enum EffectIndex {First, Second, Third}
    [SerializeField] EffectIndex effectCastOrder;

    GameReferences GameReferences;
    CreatureSelection_p CreatureSelection_p;
    CreatureSelection_e CreatureSelection_e;

    GameObject[] playerTarget;
    GameObject[] enemyTarget;

    public CardEffect_ScriptableObj.conditional TargetConditional;
    public CardEffect_ScriptableObj.condition EffectCondition;

    public AudioClip sfx_effect;
    public GameObject vfx_effect;
    public bool vfx_isAreaEffect;

    int creatureAttack, creatureHeal, creatureDefense;
    int indexOrder;


    void Start()
    {
        GameReferences = GameObject.Find("Game Manager").GetComponent<GameReferences>();
        CreatureSelection_p = GameObject.Find("Game Manager").GetComponent<CreatureSelection_p>();
        CreatureSelection_e = GameObject.Find("Game Manager").GetComponent<CreatureSelection_e>();

        if      (effectCastOrder == EffectIndex.First) indexOrder = 0;
        else if (effectCastOrder == EffectIndex.Second) indexOrder = 1;
        else    indexOrder = 2;

        playerTarget = new GameObject[] {GameReferences.p_Creature_01, GameReferences.p_Creature_02, GameReferences.p_Creature_03};      
        enemyTarget  = new GameObject[] {GameReferences.e_Creature_01, GameReferences.e_Creature_02, GameReferences.e_Creature_03};
    }

    void Update()
    {
        if(CardProperty.cardData != null)
        {
            UpdateCardEffects();
        }
        SelectedCastingCreature();
    }


    void UpdateCardEffects()
    {
        if (CardProperty.cardData.cardEffect[indexOrder] != null)
        {
            TargetConditional = CardProperty.cardData.cardEffect[indexOrder].targetConditional;
            EffectCondition   = CardProperty.cardData.cardEffect[indexOrder].effectCondition;  
            sfx_effect =        CardProperty.cardData.cardEffect[indexOrder].sfx_effect;
            vfx_effect =        CardProperty.cardData.cardEffect[indexOrder].vfx_effect;
            vfx_isAreaEffect =  CardProperty.cardData.cardEffect[indexOrder].vfx_isAreaEffect;
        }            
    }
    
    public void CastEffect(GameObject target)
    {
        //Cast_Animation(true, target); //**
        UpdateCardEffects();

        target.GetComponent<CreatureBuffDebuffManager>().BuffDebuffResponse_BeforeEffect();   

        CardProperty.cardData.cardEffect[indexOrder].CastEffect
        (CardProperty, target, creatureAttack, creatureHeal, creatureDefense, 
        CreatureSelection_p, CreatureSelection_e, playerTarget, enemyTarget);
        
        target.GetComponent<CreatureBuffDebuffManager>().BuffDebuffResponse_AfterEffect(SelectedCastingCreature());    
    }



    GameObject SelectedCastingCreature() 
    {
        if(CardProperty.teamSide == GameReferences.TeamSide.Player)
        {
            if(CreatureSelection_p.currentSelectedCreature == null) return null;
            var selectedCratureProperty_p = CreatureSelection_p.currentSelectedCreature.GetComponent<CreatureProperty>();
            creatureAttack  = selectedCratureProperty_p.currentCreatureAttack;
            creatureHeal    = selectedCratureProperty_p.currentCreatureHeal;
            creatureDefense = selectedCratureProperty_p.currentCreatureDefense;
            
            return CreatureSelection_p.currentSelectedCreature;
        }
        else
        {
            if(CreatureSelection_e.currentSelectedCreature == null) return null;
            var selectedCratureProperty_e = CreatureSelection_e.currentSelectedCreature.GetComponent<CreatureProperty>();
            creatureAttack  = selectedCratureProperty_e.currentCreatureAttack;
            creatureHeal    = selectedCratureProperty_e.currentCreatureHeal;
            creatureDefense = selectedCratureProperty_e.currentCreatureDefense;
            
            return CreatureSelection_e.currentSelectedCreature;
        }
    }





    /*void Cast_Animation(bool playAnimation, GameObject target) //**
    {
        if(CardProperty.teamSide == GameReferences.TeamSide.Player)
        {
            var thisCreatureAnimation = CreatureSelection_p.currentSelectedCreature.GetComponent<CreatureAnimation>();
            thisCreatureAnimation.Atack_Animation(true, target);
        }
        else
        {
            var thisCreatureAnimation = CreatureSelection_e.currentSelectedCreature.GetComponent<CreatureAnimation>();
            thisCreatureAnimation.Atack_Animation(true, target);
        }
    }*/


    
}
