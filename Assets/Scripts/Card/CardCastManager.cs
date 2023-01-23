using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardCastManager : MonoBehaviour
{
    [SerializeField] CardProperty CardProperty;
    [SerializeField] EffectConditionalManager EffectConditionalManager;
    [SerializeField] CardTargetCheck CardTargetCheck; 
    
    [SerializeField] CardEffect CardEffect1;
    [SerializeField] CardEffect CardEffect2;
    [SerializeField] CardEffect CardEffect3;
    List<CardEffect> cardEffects;

    EnemyAI EnemyAI;
    GameManager GameManager;
    GameStates GameStates;
    ManaManager ManaManager;
    AudioSource SFX_Player;
    SFX_Manager SFX_Manager;
    CreatureSelection_p CreatureSelection_p;
    CreatureSelection_e CreatureSelection_e;
    
    public GameObject target;
    public GameReferences.TeamSide castingTeam;
    

    void Start()
    {
        GameObject _gameManager = GameObject.Find("Game Manager");

        EnemyAI         = _gameManager.GetComponent<EnemyAI>();
        GameManager     = _gameManager.GetComponent<GameManager>();
        GameStates      = _gameManager.GetComponent<GameStates>();
        ManaManager     = _gameManager.GetComponent<ManaManager>();
        SFX_Player      = _gameManager.GetComponent<AudioSource>();
        SFX_Manager     = _gameManager.GetComponent<SFX_Manager>();
        CreatureSelection_p = _gameManager.GetComponent<CreatureSelection_p>();
        CreatureSelection_e = _gameManager.GetComponent<CreatureSelection_e>();

        cardEffects     = new List<CardEffect>() {CardEffect1, CardEffect2, CardEffect3};
        castingTeam     = CardProperty.teamSide;
    }

    public void OnTriggerStay2D(Collider2D other)
    {  
        if(castingTeam == GameReferences.TeamSide.Player)
            target = other.gameObject;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        target = null;
    }

    void IfEnemy_SetEnemyTarget()
    {
        if(castingTeam == GameReferences.TeamSide.Enemy)
            target = EnemyAI.target;
    }


// ------------------------------------------------------------------------------------------------
    public void CastCard()
    {
        IfEnemy_SetEnemyTarget();
        
        if(CardTargetCheck.CheckTargetAllowed(target, castingTeam))
        {
            var thisCard      = GetComponent<CardProperty>().cardData;
            var chosenTarget  = target;

            if (CheckMana())
                StartCoroutine(CastEffects(thisCard, chosenTarget));
            else
                {Debug.Log("Card: was NOT cast at target: " + target);}
        }
        else if (!CardTargetCheck.CheckTargetAllowed(target, castingTeam))
        {
            if(target != null)
                SFX_Manager.audioPlayer.PlayOneShot(SFX_Manager.sfx_wrongTarget); 
        }
    }
    
    IEnumerator CastEffects(Card_ScriptableObj thisCard, GameObject chosenTarget)
    {
        var originalGameState = GameStates.currentGameState; // this needs to happen before the next line;
        GameStates.currentGameState = GameStates.GameState.CastingCard;
        //target.GetComponent<CreatureVFX>().CastEffect.Play(); --> This maybe obsolete?
        
        var _castingFinished = false;
        var i_target = target;
        
        for(int i = 0; i < cardEffects.Count; i++)
        {
            bool ConditionalTest    = EffectConditionalManager.ConditionalTest(cardEffects[i], i_target);
            bool Condition          = EffectConditionalManager.Condition(cardEffects[i], i_target);

            if(thisCard.cardEffect[i].name == "_NONE")
            {
                EndAttackAnimations();  Debug.Log("yield " + i + "NULL");
                yield return null;
            }
            else if(ConditionalTest && Condition)
            {
                cardEffects[i].CastEffect(chosenTarget);
                SFX_Player.PlayOneShot(cardEffects[i].sfx_effect);
                CastEffect_VFX(i_target, i);

                yield return new WaitForSeconds(1.0f);
                EndAttackAnimations();  Debug.Log("yield " + i);
            }
            else Debug.Log("yield " + i + " " + ConditionalTest + " " + Condition);
            EndAttackAnimations();
        }
        
        yield return _castingFinished = true;
        if(_castingFinished)
        {
            Creature_ScriptableObj caster;
            if(castingTeam == GameReferences.TeamSide.Player)
                caster = CreatureSelection_p.currentSelectedCreature.GetComponent<CreatureProperty>().creatureData;
            else
                caster = CreatureSelection_e.currentSelectedCreature.GetComponent<CreatureProperty>().creatureData;

            SetBuffDebuffs_False(i_target);    // Must come before SendCardToGrave(thisCard)
            
            var i_targetProperty = i_target.GetComponent<CreatureProperty>();
            SendCardToGrave(thisCard, caster, i_targetProperty.creatureData, castingTeam, i_targetProperty.TeamSide, CardProperty.allowedTarget); // Can Not come befoer other methods that depend on variables nested in the card
        }
        
        GameStates.currentGameState = originalGameState; 
    }

    void CastEffect_VFX(GameObject i_target, int i)
    {
        if (cardEffects[i].vfx_isAreaEffect == false)
            i_target.GetComponent<CreatureVFX>().CastEffect_VFX(cardEffects[i].vfx_effect);
        else // if(cardEffects[i].vfx_isAreaEffect == true)
        {
            var e_creatureList = CreatureSelection_e.e_creatureList;
            var p_creatureList = CreatureSelection_p.p_creatureList;

            if (i_target.GetComponent<CreatureProperty>().TeamSide == GameReferences.TeamSide.Player)
            {
                foreach (var j in CreatureSelection_p.p_aliveCreatures)
                    p_creatureList[j].GetComponent<CreatureVFX>().CastEffect_VFX(cardEffects[i].vfx_effect);
            }
            else // i_target TeamSide.Enemy
            {
                foreach (var j in CreatureSelection_e.e_aliveCreatures)
                    e_creatureList[j].GetComponent<CreatureVFX>().CastEffect_VFX(cardEffects[i].vfx_effect);
            }
        }
    }


    // ------------------------------------------------------------------------------------------------
    void SetBuffDebuffs_False(GameObject i_target)
    {
        TurnHiddenOff();
        TurnTauntOff();

        void TurnHiddenOff()
        {
            var Caster = CreatureSelection_p.currentSelectedCreature;
            if(CardProperty.teamSide == GameReferences.TeamSide.Enemy)
                Caster = CreatureSelection_e.currentSelectedCreature;
            var _hiddenJustApplied = Caster.GetComponent<CreatureBuffDebuffManager>().hiddenJustApplied;

            if (!_hiddenJustApplied) 
            {
                if(CardProperty.teamSide == GameReferences.TeamSide.Player)
                    CreatureSelection_p.currentSelectedCreature.GetComponent<CreatureProperty>().SetHidden(false);
                else
                    CreatureSelection_e.currentSelectedCreature.GetComponent<CreatureProperty>().SetHidden(false);
            }
            
            foreach (var creature in CreatureSelection_p.p_creatureList)
                creature.GetComponent<CreatureBuffDebuffManager>().hiddenJustApplied = false; 
            foreach (var creature in CreatureSelection_e.e_creatureList)
                creature.GetComponent<CreatureBuffDebuffManager>().hiddenJustApplied = false; 
        }

        void TurnTauntOff()
        {
            //var Caster = CreatureSelection_p.currentSelectedCreature;
            //if(CardProperty.teamSide == GameReferences.TeamSide.Enemy)
            //    Caster = CreatureSelection_e.currentSelectedCreature;
            var _tauntJustApplied = i_target.GetComponent<CreatureBuffDebuffManager>().tauntJustApplied;  //Shoudl be target

            if(!_tauntJustApplied)
            {
                if (castingTeam != i_target.GetComponent<CreatureProperty>().TeamSide)
                    i_target.GetComponent<CreatureProperty>().SetTaunt(false); 
            }

            foreach (var creature in CreatureSelection_p.p_creatureList)
                creature.GetComponent<CreatureBuffDebuffManager>().tauntJustApplied = false; 
            foreach (var creature in CreatureSelection_e.e_creatureList)
                creature.GetComponent<CreatureBuffDebuffManager>().tauntJustApplied = false; 

        }
    }

    public bool CheckMana()
    {
        if  (castingTeam == GameReferences.TeamSide.Player &&  ManaManager.UseMana_Player(CardProperty.cardData.manaCost)
        ||  castingTeam == GameReferences.TeamSide.Enemy &&  ManaManager.UseMana_Enemy(CardProperty.cardData.manaCost))
            return true;
        else
            return false;
    }

    void SendCardToGrave(Card_ScriptableObj thisCard, Creature_ScriptableObj thisCaster, Creature_ScriptableObj thisTarget,
     GameReferences.TeamSide castingTeam, GameReferences.TeamSide targetTeam, GameReferences.AllowedTarget allowedTarget)
    {
        if  (castingTeam == GameReferences.TeamSide.Player)
            CreatureSelection_p.SelectNewAvaliableCreature(); 
        else
            CreatureSelection_e.SelectNewAvaliableCreature(); 
        GameManager.SendCardGrave(thisCard, thisCaster, thisTarget, castingTeam, targetTeam, allowedTarget);
    }

    void EndAttackAnimations()
    {
        CreatureSelection_p.currentSelectedCreature.GetComponent<CreatureAnimation>().EndAnimations();
        CreatureSelection_e.currentSelectedCreature.GetComponent<CreatureAnimation>().EndAnimations();
    }

    
}
