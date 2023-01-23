using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyHand EnemyHand;
    [SerializeField] GameStates GameStates;
    [SerializeField] ManaManager ManaManager;
    [SerializeField] EnemyHandDisplay EnemyHandDisplay;
    [SerializeField] CreatureSelection_p CreatureSelection_p;
    [SerializeField] CreatureSelection_e CreatureSelection_e;
    [SerializeField] CreaturePositioningManager CreaturePositioningManager;

    [SerializeField, Tooltip("Time the card will be displayed when cast")] 
    float freezeCardTimer;
    [SerializeField, Tooltip("If e_creature has less life_% than this, return true")] 
    float defenseCriteria;
    [SerializeField, Tooltip("If p_creature has less life_% than this, return true")] 
    float attackCriteria;

    List<GameObject> castableCard_mana = new List<GameObject>();
    List<GameObject> attackCards_list = new List<GameObject>();
    List<GameObject> defenseCards_list = new List<GameObject>();

    [Header ("Debug Information")]
    [SerializeField] GameReferences.StrategyTypes attackOrDefense;
    [SerializeField] GameObject card;
    [SerializeField] GameObject castingCreature;
    public GameObject target;
    bool pauseForCastDisplaying = false;
    float endTurnTimer = 0.5f;
    float startTurnTimer = 0.5f;

    public void PerformMove_Enemy() //This gets called on a Update() method
    {
        startTurnTimer -= Time.deltaTime;
        if(pauseForCastDisplaying == false && startTurnTimer < 0)
        {
            if(CreatureSelection_e.AllCreaturesSleeping())
            {  
                return;
            }
            
            CheckCardsMana();
            if(castableCard_mana.Count > 0 && EnemyHand.EnemyHand_list.Count > 0)
            {
                CheckCardsMana(); //CheckMana - list castableCards
                ListCardStrategy(); //AnalizeCards - list attackCards and defenseCards

                attackOrDefense = DecideAttackOrDefense();
                castingCreature = ChooseCastingCreature();
                SetCastingCreature(castingCreature);
                target = ChooseTarget();
                card = ChooseCard();

                CastCard(); //Call CardCastManager.CastCard(); with delay for Animation() and Display()
                CreatureSelection_e.UpdateAvailabilityDisplay();
                return;
            }
            else
            {
                EndTurn(slow: true);
            }
        }
    }

    

    void CheckCardsMana()
    {
        castableCard_mana = new List<GameObject>();

        List<GameObject> enemyHand = EnemyHandDisplay.avalibaleEnemyCards_list;
        foreach (var card in enemyHand)
        {
            int manaCost = card.GetComponent<CardProperty>().cardData.manaCost;
            if(manaCost <= ManaManager.currentMana_enemy)
            {
                castableCard_mana.Add(card);
            }
        }
    }

    void ListCardStrategy()
    {
        attackCards_list = new List<GameObject>();
        defenseCards_list = new List<GameObject>();

        foreach (var card in castableCard_mana)
        {
            var cardStrategy = card.GetComponent<CardProperty>().cardData.cardStrategy;
            if(cardStrategy == GameReferences.StrategyTypes.Attack)
                attackCards_list.Add(card);
            else
                defenseCards_list.Add(card);
        }
    }

    GameReferences.StrategyTypes DecideAttackOrDefense()
    {
        if(DecideDefense(defenseCriteria) || attackCards_list.Count == 0)
        {
            return GameReferences.StrategyTypes.Defense;
        }
        else if(DecideAttack(attackCriteria) || defenseCards_list.Count == 0)
        {
            return GameReferences.StrategyTypes.Attack;
        }
        else if(!DecideDefense(defenseCriteria) && !DecideAttack(attackCriteria))
        {
            // Try attack; If that fails, ChooseCreatureToAttack() will end EndTurn();
            return GameReferences.StrategyTypes.Any;
            /*Discontinued:     
                EndTurn();
                return GameReferences.StrategyTypes.EndTurn;*/
        }    
        else return GameReferences.StrategyTypes.Any;
        
        bool DecideDefense(float criteria)
        {
            if(defenseCards_list.Count <= 0)
                return false;

            foreach(int i in CreatureSelection_e.e_avaliableCreatures)
            {
                var i_creature = CreatureSelection_e.e_creatureList[i];
                int creatureHeal = i_creature.GetComponent<CreatureProperty>().creatureData.creatureHeal;

                if(creatureHeal > 0)
                {
                    foreach(int k in CreatureSelection_e.e_aliveCreatures)
                    {
                        var CreatureProperty = CreatureSelection_e.e_creatureList[k].GetComponent<CreatureProperty>();
                            int creatureHealth =        CreatureProperty.currentCreatureHealth;
                            int creatureMaxHealth =     CreatureProperty.currentCreatureMaxHealth;
                        
                        if(((float)creatureHealth / (float)creatureMaxHealth) < criteria)
                        {
                            return true; // Once the decision is true, there is no need to continue checking
                        }
                        return false;  // Todo: Add criteria here other than deciding if should heal or not.
                    }
                }
            }
            return false;
        }

        bool DecideAttack(float criteria)
        {
            if(attackCards_list.Count <= 0)
                return false;

            foreach(int i in CreatureSelection_p.p_aliveCreatures)
            {
                var CreatureProperty = CreatureSelection_p.p_creatureList[i].GetComponent<CreatureProperty>();
                    int creatureHealth =        CreatureProperty.currentCreatureHealth;
                    int creatureMaxHealth =     CreatureProperty.currentCreatureMaxHealth;

                if(((float)creatureHealth / (float)creatureMaxHealth) < criteria)
                {
                    var CreatureBuffDebuffManager = CreatureSelection_p.p_creatureList[i].GetComponent<CreatureBuffDebuffManager>();
                    if (!CreatureBuffDebuffManager.hidden) 
                        return true;
                }
            }
            return false;
        }
    }
    
    GameObject ChooseCastingCreature()
    {
        GameObject caster = CreatureSelection_e.e_creatureList[CreatureSelection_e.e_avaliableCreatures[0]];
        var cardStrategy = ChooseCard().GetComponent<CardProperty>().cardData.cardStrategy;

        if(cardStrategy == GameReferences.StrategyTypes.Attack)
        {
            int highestAttack = 0;
            foreach(int i in CreatureSelection_e.e_avaliableCreatures)
            {
                GameObject i_creature = CreatureSelection_e.e_creatureList[i];
                    int creatureAttack = i_creature.GetComponent<CreatureProperty>().creatureData.creatureAttack;
                if(creatureAttack > highestAttack)
                {
                    caster = i_creature;
                }
                return caster;
            }
            return caster;
        }
        else if(cardStrategy == GameReferences.StrategyTypes.Defense)
        {
            int highestHeal = 0;
            foreach(int i in CreatureSelection_e.e_avaliableCreatures)
            {
                GameObject i_creature = CreatureSelection_e.e_creatureList[i];
                    int creatureHeal = i_creature.GetComponent<CreatureProperty>().creatureData.creatureHeal;
                if(creatureHeal > highestHeal)
                {
                    caster = i_creature;
                }
                return caster;
            }
            return caster;
        } 
        else
        {
            return caster;
        }
    }

    void SetCastingCreature(GameObject _castingCreature)
    {
        CreatureSelection_e.currentSelectedCreature = _castingCreature;
    }

    GameObject ChooseTarget()
    {
        GameObject _target = CreatureSelection_e.e_creatureList[CreatureSelection_e.e_aliveCreatures[0]];
        var cardStrategy = ChooseCard().GetComponent<CardProperty>().cardData.cardStrategy;

        if(cardStrategy == GameReferences.StrategyTypes.Attack)
        {
            return ChooseCreatureToAttack();
        }
        else if(cardStrategy == GameReferences.StrategyTypes.Defense)
        {
            return ChooseCreatureToDefend();
        } 
        else if(cardStrategy == GameReferences.StrategyTypes.Any)
        {
            if(attackCards_list.Count > 0)
            {
                return ChooseCreatureToAttack();
            }
            else ChooseCreatureToDefend();
        }
        else if(cardStrategy == GameReferences.StrategyTypes.Self)
        {
            _target = CreatureSelection_e.currentSelectedCreature;
        }
        else Debug.LogError("Cant decide target!");
        return _target;


        GameObject ChooseCreatureToAttack()  //RAFACTOR: i from CreatureSelection_p.p_aliveCreatures is an INT but can be a GameObject
        {
            int highestDefense = 1000;
            foreach(int i in TargetableCreatures())  
            {
                GameObject i_creature = CreatureSelection_p.p_creatureList[i];
                    int creatureDefense = i_creature.GetComponent<CreatureProperty>().creatureData.creatureDefense;
                if(creatureDefense < highestDefense)
                {
                    _target =  i_creature;
                    highestDefense = creatureDefense;
                }// Hidden creatures will not return results; 
            }
            // If all hidden then highestDefense == 1000.
            if (highestDefense == 1000) 
                EndTurn();
            Debug.Log(_target); 
            return _target;
        }

        GameObject ChooseCreatureToDefend()
        {
            int highestHealth = 1000;
            foreach(int i in CreatureSelection_e.e_aliveCreatures)
            {
                GameObject i_creature = CreatureSelection_e.e_creatureList[i];
                int creatureHealth = i_creature.GetComponent<CreatureProperty>().currentCreatureHealth;
                if(creatureHealth < highestHealth)
                {
                    target = i_creature;
                }
                return target;
            }
            return target;
        }
    }

        
    List<int> TargetableCreatures()
    {
        List<int> _targetableCreatures = new List<int>();

        foreach(int i in CreatureSelection_p.p_aliveCreatures) // Check if there are Taunt Creatures
        {
            var _creatureBuffs = CreatureSelection_p.p_creatureList[i].GetComponent<CreatureBuffDebuffManager>();
            if (_creatureBuffs.taunting && !_creatureBuffs.hidden)
                _targetableCreatures.Add(CreatureSelection_p.p_aliveCreatures[i]);
        }
        
        if (_targetableCreatures.Count == 0)  //This means there are no creatures with Taunt_Active...
        {
            foreach(int i in CreatureSelection_p.p_aliveCreatures) // Check if there are Taunt Creatures
            {
                var _creatureBuffs = CreatureSelection_p.p_creatureList[i].GetComponent<CreatureBuffDebuffManager>();
                if(!_creatureBuffs.hidden)
                    _targetableCreatures.Add(i);
            }
        }

        return _targetableCreatures;
    }

    GameObject ChooseCard() // TODO: Choose card with highest mana cost
    {
        if      (attackOrDefense == GameReferences.StrategyTypes.Attack && attackCards_list.Count > 0)
        {
            List<GameObject> _choice_list = new List<GameObject>();

            // !#!#! --> All the commented code bellow: Attemped to rank creatures and cards. Maybe wasnt working? Review before using again.
            // ^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v

            //List<GameObject> _target_list = new List<GameObject>();
            int bestRank = 0;

            // TODO NOW: this finds the creature with lowest life in the p_aliveCreatures. Chenge to TargetableCreatures()
            /*foreach(int i in TargetableCreatures()) 
            {
                GameObject i_creature = CreatureSelection_p.p_creatureList[i];
                target = i_creature;

                foreach(GameObject card in attackCards_list)
                {
                    if(_target_list.Count == 0)
                    {
                        _target_list.Add(i_creature);
                    }
                    else if(card.GetComponent<CardConditionalCheck>().CheckConditionals() == bestRank)
                    {
                        _target_list.Add(i_creature);
                    }
                    else if(card.GetComponent<CardConditionalCheck>().CheckConditionals() > bestRank)
                    {
                        _target_list.Clear();
                        _target_list.Add(i_creature);
                    }
                }
            }
            target = _target_list[Random.Range(0, _target_list.Count)];    */


            // TODO NOW: This chooses between the attack cards, the card with the best rank
            /*foreach(GameObject card in attackCards_list)
            {
                if(_choice_list.Count == 0)
                {
                    _choice_list.Add(card);
                }
                else if(card.GetComponent<CardConditionalCheck>().CheckConditionals() == bestRank)
                {
                    _choice_list.Add(card);
                }
                else if(card.GetComponent<CardConditionalCheck>().CheckConditionals() > bestRank)
                {
                    _choice_list.Clear();
                    _choice_list.Add(card);
                }
            }
            return _choice_list[Random.Range(0, _choice_list.Count)]; */

            return attackCards_list[Random.Range(0, attackCards_list.Count)];
        }
        if      (attackOrDefense == GameReferences.StrategyTypes.Defense && defenseCards_list.Count > 0)
        {
            if(defenseCards_list.Count > 0)
                return defenseCards_list[Random.Range(0,defenseCards_list.Count)];
            else
                attackOrDefense = GameReferences.StrategyTypes.Any;
        }
        if      (attackOrDefense == GameReferences.StrategyTypes.Any && attackCards_list.Count > 0)
        {
            return attackCards_list[Random.Range(0, attackCards_list.Count)];
        }
        else if (attackOrDefense == GameReferences.StrategyTypes.Any && defenseCards_list.Count > 0)
        {
            foreach(int i in CreatureSelection_e.e_aliveCreatures)
            {
                int creatureHealth = CreatureSelection_e.e_creatureList[i].GetComponent<CreatureProperty>().currentCreatureHealth;
                int creatureMaxHealth = CreatureSelection_e.e_creatureList[i].GetComponent<CreatureProperty>().currentCreatureMaxHealth;
                
                if((float)creatureHealth < (float)creatureMaxHealth)
                {
                    return defenseCards_list[Random.Range(0, defenseCards_list.Count)];    
                }
                else
                {
                    GameStates.currentGameState = GameStates.GameState.EnemyEndTurn;
                    return defenseCards_list[Random.Range(0, defenseCards_list.Count)];  
                } 
            }
            GameStates.currentGameState = GameStates.GameState.EnemyEndTurn;
            return defenseCards_list[Random.Range(0, defenseCards_list.Count)];  
        }
        else 
        {
            GameStates.currentGameState = GameStates.GameState.EnemyEndTurn;
            return castableCard_mana[0];
        }
    }

    void EndTurn(bool slow = false)
    {
        if(slow)
        {
            endTurnTimer -= Time.deltaTime;
            if (endTurnTimer < 0)
            {
                CreatureSelection_e.ResetAvailableCreatureSelection();
                GameStates.currentGameState = GameStates.GameState.EnemyEndTurn;
                startTurnTimer = 1.5f;
                endTurnTimer = 1f;
            }
        }
        else
        {
            CreatureSelection_e.ResetAvailableCreatureSelection();
            GameStates.currentGameState = GameStates.GameState.EnemyEndTurn;
        }
    }






    // REAFACTOR this to its own class
    Vector3 originalCardPosition;
    Vector3 originalCardScale;
    int originalSortingOrder;
    void CastCard()
    {
        CreaturePositioningManager.SetCreatureOnAttackPosition(castingCreature); 

        originalCardPosition = card.transform.position;
        originalCardScale = card.transform.localScale; 
        originalSortingOrder = card.GetComponent<Canvas>().sortingOrder;
        
        StartDisplayCardOnCast();
        Invoke("PerformCast", freezeCardTimer);
    }

    void StartDisplayCardOnCast()
    {
        pauseForCastDisplaying = true;
        card.transform.localPosition = new Vector3(25, 100, 0);
        card.transform.localScale = new Vector3(7f, 7f, 7f);

        card.GetComponent<LineRenderer>().sortingOrder = 0;
        card.GetComponent<LineRenderer>().SetPosition(0, new Vector3(1.8f ,0.62f ,0));
        card.GetComponent<LineRenderer>().SetPosition(1, target.transform.position);
        
        card.GetComponent<Canvas>().sortingOrder = 1;
    }

    void PerformCast()
    {
        card.transform.position = target.transform.position;
        card.transform.localScale = originalCardScale;
        
        card.GetComponent<LineRenderer>().sortingOrder = -10;
        card.GetComponent<LineRenderer>().SetPosition(0, originalCardPosition);
        card.GetComponent<LineRenderer>().SetPosition(1, originalCardPosition);
        
        card.GetComponent<Canvas>().sortingOrder = originalSortingOrder;
        card.GetComponent<CardCastManager>().CastCard();
        card.transform.position = originalCardPosition;
        pauseForCastDisplaying = false;
    }

}
