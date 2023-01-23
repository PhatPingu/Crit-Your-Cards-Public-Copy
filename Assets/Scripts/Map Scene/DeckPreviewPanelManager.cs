using UnityEngine;

public class DeckPreviewPanelManager : MonoBehaviour
{
    [SerializeField] PlayerGameData PlayerGameData;
    [SerializeField] GameObject[] cards;

    public void UpdateDeckCards()
    {
        PlayerGameData.UpdateActiveDeckList();
        var count = PlayerGameData.activeDeck_list.Count;

        for (int i = 0; i < cards.Length; i++)
        {
            if(count > 0)
            {
                cards[i].SetActive(true);
                cards[i].GetComponent<CardProperty>().cardData = PlayerGameData.activeDeck_list[i];
                count--;
            }
            else
            {
                cards[i].SetActive(false);
            }

        }
    }
}
