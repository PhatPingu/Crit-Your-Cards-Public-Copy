using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catalogue_TabManager : MonoBehaviour
{
    [SerializeField] GameObject creatureTab;
    [SerializeField] GameObject cardCollectionTab;

    public void GoCreatureTab()
    {
        creatureTab.SetActive(true);
        cardCollectionTab.SetActive(false);
    }

    public void GoCardCollectionTab()
    {
        creatureTab.SetActive(false);
        cardCollectionTab.SetActive(true);    
    }
}
