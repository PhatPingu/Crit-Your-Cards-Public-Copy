using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureStatDisplay : MonoBehaviour
{
    [SerializeField] Image outlineImage;

    public void SetDisplayAs_Selected(bool choice)
    {
        if(choice)
        {
            outlineImage.enabled = true;            
        }
        else
        {
            outlineImage.enabled = false;
        }
    }
}
