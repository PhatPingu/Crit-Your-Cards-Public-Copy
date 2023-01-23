using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBuffDebuffManager : MonoBehaviour
{
    [SerializeField] CreatureBuffDisplay CreatureBuffDisplay;
    
    CreatureProperty CreatureProperty;
    CreatureSelection_p CreatureSelection_p;
    CreatureSelection_e CreatureSelection_e;

    public bool burnJustApplied, hiddenJustApplied, tauntJustApplied;
    public bool hidden, taunting, shielded;
    public int burnTick, retaliateTick, sleepTick;

    int healthOnShield;
    

    void Start()
    {
        GameObject _gameManager = GameObject.Find("Game Manager");
        CreatureSelection_p = _gameManager.GetComponent<CreatureSelection_p>();
        CreatureSelection_e = _gameManager.GetComponent<CreatureSelection_e>();
        
        CreatureProperty = GetComponent<CreatureProperty>();
        healthOnShield = CreatureProperty.currentCreatureHealth;
    }

    void FixedUpdate()
    {
        AdjustIfShielded();
    }

    void AdjustIfShielded()
    {
        if(shielded && healthOnShield > CreatureProperty.currentCreatureHealth)
        {
            CreatureProperty.currentCreatureHealth = healthOnShield;
            CreatureProperty.SetShielded(false);
            GetComponent<CreatureDmgDisplay>().DisplayAttackAndDefense(false);
        }
        else
        {
            healthOnShield = CreatureProperty.currentCreatureHealth;
        }
    }
    
    public void SetBuffDebuffTick(int burn = 0, int retaliate = 0, int sleep = 0)
    {
        burnTick = burn;
        retaliateTick = retaliate;
        sleepTick = sleep;
    }

    public void AddTickToBuffDebuff(int burn = 0, int retaliate = 0, int sleep = 0)
    {
        burnTick += burn;
        retaliateTick += retaliate;
        sleepTick += sleep;
    }

    public void RemoveTickFromBuffDebuff()
    {
        if(sleepTick > 0 ) sleepTick -= 1;        
    }

    public void BuffDebuffResponse_BeforeEffect()
    {
        sleepTick = 0; // DESIGN ISSUE: This should only happen if the player suffers DMG or if enemy targets it?
    }
    
    public void BuffDebuffResponse_AfterEffect(GameObject caster)
    {
        var casterProperty = caster.GetComponent<CreatureProperty>();

        Retaliate();
        Burn();

        void Retaliate()
        {
            if (retaliateTick > 0 && casterProperty.TeamSide != CreatureProperty.TeamSide)
            {
                var damage = CreatureProperty.currentCreatureAttack - casterProperty.currentCreatureDefense;
                if(damage < 1) damage = 1;
                casterProperty.currentCreatureHealth -= damage;

                retaliateTick = 0;
                CreatureBuffDisplay.ShowRetaliate(false);

                caster.GetComponent<CreatureDmgDisplay>().retaliateDamage_text.text = damage.ToString();
                caster.GetComponent<CreatureDmgDisplay>().DisplayRetaliateDamage();
            }
        }

        void Burn()
        {
            if (burnTick > 0 && casterProperty.TeamSide != CreatureProperty.TeamSide && !burnJustApplied
            ||  burnTick > 1 && casterProperty.TeamSide != CreatureProperty.TeamSide)
            {
                var damage = casterProperty.currentCreatureAttack;                
                if(damage < 0) damage = 0;
                CreatureProperty.currentCreatureHealth -= damage;

                burnTick = 0;
                CreatureBuffDisplay.ShowBurn(false);

                GetComponent<CreatureDmgDisplay>().burnDamage_text.text = damage.ToString();
                GetComponent<CreatureDmgDisplay>().DisplayBurnDamage();
            }
            
            burnJustApplied = false;
        }
    }

    

}
