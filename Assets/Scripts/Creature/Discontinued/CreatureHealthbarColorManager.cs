using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureHealthbarColorManager : MonoBehaviour
{
    [SerializeField] CreatureProperty CreatureProperty;
    [SerializeField] Image background;
    [SerializeField] Image rightBackground;
    [SerializeField] Image fill;
    [SerializeField] Image fillTip;
    [SerializeField] Image heartCircle;

    [SerializeField] Sprite[] background_array;
    [SerializeField] Sprite[] rightBackground_array;
    [SerializeField] Sprite[] fill_array;
    [SerializeField] Sprite[] fillTip_array;
    [SerializeField] Sprite[] heartCircle_array;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0);

        background.sprite       = background_array[TeamColor_ID()];
        rightBackground.sprite  = rightBackground_array[TeamColor_ID()];
        fill.sprite             = fill_array[TeamColor_ID()];
        fillTip.sprite          = fillTip_array[TeamColor_ID()];
        heartCircle.sprite      = heartCircle_array[TeamColor_ID()];
    }

    int TeamColor_ID()
    {
        if(CreatureProperty.creatureColor  == GameReferences.FamilyColor.Blue)
            return 0;
        else if(CreatureProperty.creatureColor  == GameReferences.FamilyColor.Red)
            return 1;
        else if(CreatureProperty.creatureColor  == GameReferences.FamilyColor.White)
            return 2;
        else if(CreatureProperty.creatureColor  == GameReferences.FamilyColor.Green)
            return 3;
        else
            return 4;
        
    }
}
