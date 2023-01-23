using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class CardDescription_Target : CardDescription
{
    public override void OverrideText()
    {
        description.text = CardProperty.cardData.cardEffect[0].allowedTarget.ToString();
    }

}
