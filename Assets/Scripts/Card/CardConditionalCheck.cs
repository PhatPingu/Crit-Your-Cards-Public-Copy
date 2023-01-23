using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConditionalCheck : MonoBehaviour
{
    [SerializeField] EffectConditionalManager EffectConditionalManager;
    [SerializeField] CardCastManager CardCastManager;
    [SerializeField] CardTargetCheck CardTargetCheck;

    [SerializeField] CardEffect CardEffect1;
    [SerializeField] CardEffect CardEffect2;
    [SerializeField] CardEffect CardEffect3;
    

    public int CheckConditionals()
    {
        var i_target = CardCastManager.target;
        if  (  EffectConditionalManager.ConditionalTest(CardEffect1, i_target) && EffectConditionalManager.Condition(CardEffect1, i_target)
            && EffectConditionalManager.ConditionalTest(CardEffect2, i_target) && EffectConditionalManager.Condition(CardEffect2, i_target) 
            && EffectConditionalManager.ConditionalTest(CardEffect3, i_target) && EffectConditionalManager.Condition(CardEffect3, i_target)
            && CardTargetCheck.CheckTargetAllowed(CardCastManager.target, CardCastManager.castingTeam))
            {
                return 2;
            }
            else if (EffectConditionalManager.ConditionalTest(CardEffect1, i_target) && EffectConditionalManager.Condition(CardEffect1, i_target)
            &&       EffectConditionalManager.ConditionalTest(CardEffect2, i_target) && EffectConditionalManager.Condition(CardEffect2, i_target) 
            &&  CardTargetCheck.CheckTargetAllowed(CardCastManager.target, CardCastManager.castingTeam))
            {
                return 1;
            }
            else
            {
                return 0;
            }


    }
}
