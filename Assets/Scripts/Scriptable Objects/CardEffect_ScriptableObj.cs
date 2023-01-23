using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

//[CreateAssetMenu(fileName = "CardEffect", menuName = "GameObjects/GenerateCardEffect")]
public class CardEffect_ScriptableObj : ScriptableObject
{
    public GameReferences.AllowedTarget allowedTarget;

    public enum conditional{None, IfTargetCard, IfTargetAlly, IfTargetEnemy, IfTarget, IfAnyAlly, IfAnyone, IfAnyEnemy, IfCaster}
    public conditional targetConditional;

    public enum condition{None, IsBlue, IsRed, IsWhite, IsGreen, IsBlack, IsNotBlue, IsNotRed, IsNotWhite, IsNotGreen, IsNotBlack, IsHidden, isTaunted, isShieled}
    public condition effectCondition;

    public int modifierAmount;
    public int ticker;

    [Header ("Override if card's target is different from effect's target")]
    public bool overrideTarget;
    
    [TextArea(10,30)] // Set these on CardDescription
    public string description = "{{color}}<b>Deal {{totalDamageAmount}} DAMAGE</b> to Target{{/color}}({{modifierAmount}}{{magicColor}} + {{creatureAttack}}<sprite=\"MagicColors\" index=6>";
    public virtual string Tooltip() { return "";}

    public AudioClip sfx_effect;
    public GameObject vfx_effect;
    public bool vfx_isAreaEffect;

    public virtual void CastEffect
    (CardProperty CardProperty, GameObject target, int creatureAttack, int creatureHeal, int creatureDefense,
    CreatureSelection_p CreatureSelection_p, CreatureSelection_e CreatureSelection_e,
    GameObject[] playerTarget, GameObject[] enemyTarget)
    {

    }

}
