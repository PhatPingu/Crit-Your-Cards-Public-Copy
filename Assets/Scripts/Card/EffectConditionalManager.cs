using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectConditionalManager : MonoBehaviour
{
    [SerializeField] CardCastManager CardCastManager;
    [SerializeField] CardTargetCheck CardTargetCheck;
    [SerializeField] CardEffect CardEffect1;
    [SerializeField] CardEffect CardEffect2;
    [SerializeField] CardEffect CardEffect3;
    
    CreatureSelection_p CreatureSelection_p;
    CreatureSelection_e CreatureSelection_e;

    GameObject target;

    GameReferences.TeamSide CastingTeam;
    GameReferences.TeamSide TargetTeam;

    GameReferences.FamilyColor targetTested_Color;
    
    bool testCaster;

    void Start()
    {
        GameObject _gameManager = GameObject.Find("Game Manager");
        CreatureSelection_p = _gameManager.GetComponent<CreatureSelection_p>();
        CreatureSelection_e = _gameManager.GetComponent<CreatureSelection_e>();
    }

    public bool ConditionalTest(CardEffect cardEffect, GameObject target)
    {
        var TargetConditionl = cardEffect.GetComponent<CardEffect>().TargetConditional;

        var None          = CardEffect_ScriptableObj.conditional.None;
        var IfTargetCard  = CardEffect_ScriptableObj.conditional.IfTargetCard;
        var IfTargetAlly  = CardEffect_ScriptableObj.conditional.IfTargetAlly;
        var IfTargetEnemy = CardEffect_ScriptableObj.conditional.IfTargetEnemy;
        var IfTarget      = CardEffect_ScriptableObj.conditional.IfTarget;
        var IfAnyAlly     = CardEffect_ScriptableObj.conditional.IfAnyAlly;
        var IfAnyone      = CardEffect_ScriptableObj.conditional.IfAnyone;
        var IfAnyEnemy    = CardEffect_ScriptableObj.conditional.IfAnyEnemy;
        var IfCaster      = CardEffect_ScriptableObj.conditional.IfCaster;
        
        CastingTeam = CardCastManager.castingTeam;
        TargetTeam = CardTargetCheck.targetTeam;
                
        if(TargetConditionl == None)
        {
            cardEffect.GetComponent<CardEffect>().EffectCondition = CardEffect_ScriptableObj.condition.None;
            return true;
        }
        
        if(TargetConditionl == IfTargetCard) //targetTested = card
        {
            return false;
        }

        if(TargetConditionl == IfTargetAlly)
        {
            if(CastingTeam == TargetTeam)
            return true;
        }

        if(TargetConditionl == IfTargetEnemy)
        {
            if(CastingTeam != TargetTeam)
            return true;
        }

        if(TargetConditionl == IfTarget)
        {
            if(CastingTeam == TargetTeam || CastingTeam != TargetTeam)
            return true;
        }

        if(TargetConditionl == IfAnyAlly) //Listener
        {
            return false;
        }

        if(TargetConditionl == IfAnyone) //Listener
        {
            return false;
        }

        if(TargetConditionl == IfAnyEnemy) //Listener
        {
            return false;
        }
        
        if(TargetConditionl == IfCaster) 
        {
            testCaster = true;
            return true;
        }

        return false;
    }

    public bool Condition(CardEffect cardEffect, GameObject target)
    {
        var EffectCondition = cardEffect.GetComponent<CardEffect>().EffectCondition;

        var None        = CardEffect_ScriptableObj.condition.None;
        var IsBlue      = CardEffect_ScriptableObj.condition.IsBlue;
        var IsRed       = CardEffect_ScriptableObj.condition.IsRed;
        var IsWhite     = CardEffect_ScriptableObj.condition.IsWhite;
        var IsGreen     = CardEffect_ScriptableObj.condition.IsGreen;
        var IsBlack     = CardEffect_ScriptableObj.condition.IsBlack;
        var IsNotBlue   = CardEffect_ScriptableObj.condition.IsNotBlue;
        var IsNotRed    = CardEffect_ScriptableObj.condition.IsNotRed;
        var IsNotWhite  = CardEffect_ScriptableObj.condition.IsNotWhite;
        var IsNotGreen  = CardEffect_ScriptableObj.condition.IsNotGreen;
        var IsNotBlack  = CardEffect_ScriptableObj.condition.IsNotBlack;
        var isHidden    = CardEffect_ScriptableObj.condition.IsHidden;
        var isTaunted   = CardEffect_ScriptableObj.condition.isTaunted;
        var isShieled   = CardEffect_ScriptableObj.condition.isShieled;

        if(target != null)
        {
            targetTested_Color = target.GetComponent<CreatureProperty>().creatureData.creatureColor;
        }
            
        if      (testCaster && CastingTeam == GameReferences.TeamSide.Player)
        {
            target = CreatureSelection_p.currentSelectedCreature;
            targetTested_Color = target.GetComponent<CreatureProperty>().creatureData.creatureColor;
        }
        else if (testCaster && CastingTeam == GameReferences.TeamSide.Enemy)
        {
            target = CreatureSelection_e.currentSelectedCreature;
            targetTested_Color = target.GetComponent<CreatureProperty>().creatureData.creatureColor;
        }
        testCaster = false;

        if(EffectCondition == None)
        {
            return true;
        }

        if(EffectCondition == IsBlue)
        {
            if(targetTested_Color == GameReferences.FamilyColor.Blue)
            return true;
        }

        if(EffectCondition == IsRed)
        {
            if(targetTested_Color == GameReferences.FamilyColor.Red)
            return true;
        }

        if(EffectCondition == IsWhite)
        {
            if(targetTested_Color == GameReferences.FamilyColor.White)
            return true;
        }

        if(EffectCondition == IsGreen)
        {
            if(targetTested_Color == GameReferences.FamilyColor.Green)
            return true;
        }

        if(EffectCondition == IsBlack)
        {
            if(targetTested_Color == GameReferences.FamilyColor.Black)
            return true;
        }

        if(EffectCondition == IsNotBlue)
        {
            if(targetTested_Color != GameReferences.FamilyColor.Blue)
            return true;
        }

        if(EffectCondition == IsNotRed)
        {
            if(targetTested_Color != GameReferences.FamilyColor.Red)
            return true;
        }

        if(EffectCondition == IsNotWhite)
        {
            if(targetTested_Color != GameReferences.FamilyColor.White)
            return true;
        }

        if(EffectCondition == IsNotGreen)
        {
            if(targetTested_Color != GameReferences.FamilyColor.Green)
            return true;
        }

        if(EffectCondition == IsNotBlack)
        {
            if(targetTested_Color != GameReferences.FamilyColor.Black)
            return true;
        }

        if(EffectCondition == isHidden)
        {
            if(target.GetComponent<CreatureBuffDebuffManager>().hidden)
            return true;
        }

        if(EffectCondition == isTaunted)
        {
            if(target.GetComponent<CreatureBuffDebuffManager>().taunting)
            return true;
        }
        
        if(EffectCondition == isShieled)
        {
            if(target.GetComponent<CreatureBuffDebuffManager>().shielded)
            return true;
        }
        
        return false;
    }
}
