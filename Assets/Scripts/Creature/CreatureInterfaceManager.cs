using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInterfaceManager : MonoBehaviour
{
    [SerializeField] CreatureProperty CreatureProperty;
    
    [SerializeField] GameObject attackIcon;
    [SerializeField] GameObject attackText;
    [SerializeField] GameObject defenseIcon;
    [SerializeField] GameObject defenseText;


    public void SetPlayerAttackInterface(bool choice)
    {
        bool isPlayer = CreatureProperty.TeamSide == GameReferences.TeamSide.Player;

        if(isPlayer)
        {
            SetDisplayToAttack(choice);
            SetDisplayToDefense(!choice);
        }
        else
        {
            SetDisplayToAttack(!choice);
            SetDisplayToDefense(choice);
        }
    }

    void SetDisplayToAttack(bool choice)
    {
        attackIcon.SetActive(choice);
        attackText.SetActive(choice);
        defenseIcon.SetActive(!choice);
        defenseText.SetActive(!choice);
    }

    void SetDisplayToDefense(bool choice)
    {
        attackIcon.SetActive(!choice);
        attackText.SetActive(!choice);
        defenseIcon.SetActive(choice);
        defenseText.SetActive(choice);
    }

}
