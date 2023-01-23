using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Raycast_Setup : MonoBehaviour
{
    Image thisButton;

    void Start()
    {
        thisButton = GetComponent<Image>();
        thisButton.alphaHitTestMinimumThreshold = 1f;
    }
}
