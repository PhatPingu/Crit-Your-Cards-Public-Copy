using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catalogue_CardDisplay : MonoBehaviour
{
    [SerializeField] PlayerCollection PlayerCollection;
    [SerializeField] GameObject[] cards_array;

    int currentPage;
    bool maxPageReached;

    void Start()
    {
        SetCatalogueCardsData();
    }

    public void SetCatalogueCardsData()
    {
        for(int i = 0; i < cards_array.Length; i++)
        {
            var k = (i + (10*currentPage));
            cards_array[i].SetActive(true);

            if(PlayerCollection.PlayerCollection_list.Count >= k+1)
            {
                cards_array[i].GetComponent<CardProperty>().cardData = PlayerCollection.PlayerCollection_list[k];
            }
            else
            {
                cards_array[i].SetActive(false);
            }

            if(PlayerCollection.PlayerCollection_list.Count == k+1)
            {
                maxPageReached = true;
            }
            else
            {
                maxPageReached = false;
            }
        }
    }

    public void GoNextPage()
    {
        if(maxPageReached) return;
        else currentPage += 1;

        SetCatalogueCardsData();
    }

    public void GoPreviousPage()
    {
        if(currentPage == 0) return;
        else currentPage -= 1;

        SetCatalogueCardsData();
    }
}
