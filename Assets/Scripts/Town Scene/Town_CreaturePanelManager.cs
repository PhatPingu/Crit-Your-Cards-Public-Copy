using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Town_CreaturePanelManager : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler

{
    [SerializeField] PlayerGameData PlayerGameData;
    [SerializeField] Creature_ScriptableObj creatureData;
    [SerializeField] GameObject[] creatureCard_array;
    [SerializeField] GameObject[] creatureBigCard_array;
    [SerializeField] Image portraitImage;
    [SerializeField] Image portraitBigImage;
    [SerializeField] GameObject popUp_PanelDisplay;

    [SerializeField] TextMeshProUGUI creatureName_text;
    [SerializeField] TextMeshProUGUI attack_text;
    [SerializeField] TextMeshProUGUI defense_text;
    [SerializeField] TextMeshProUGUI heal_text;

    void Start()
    {
        popUp_PanelDisplay.SetActive(false);
        UpdateCreaturePanelData();
    }

    void FixedUpdate()
    {
        UpdateCreaturePanelData();
    }

    void UpdateCreaturePanelData()
    {
        UpdateCreatureData(); // This needs to happen before all comands below:
        portraitImage.sprite = creatureData.creatureImage;
        portraitBigImage.sprite = creatureData.creatureImage;
        creatureName_text.text = creatureData.creatureName.ToString();
        attack_text.text = creatureData.creatureAttack.ToString();
        defense_text.text = creatureData.creatureDefense.ToString();
        heal_text.text = creatureData.creatureHeal.ToString();
        UpdateCreatureCardArray();
    }

    public void UpdateCreatureData() // TODO: This needs to accound for when the PlayerGameData is empty
    {
        if(transform.GetSiblingIndex() > 2)
        {
            if(PlayerGameData.creatureCollection_convertedNotActive.Count - 1 < transform.GetSiblingIndex() - 3)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
                creatureData = PlayerGameData.creatureCollection_convertedNotActive[transform.GetSiblingIndex() - 3];
            }
        }
        else
        {
            this.gameObject.SetActive(true);
            creatureData = PlayerGameData.creatureCollection_activeTeam[transform.GetSiblingIndex()];
        }
    }

    void UpdateCreatureCardArray()
    {
        int count = creatureData.creatureCards_starter.Count; //TEMP: Changed creatureCards_available to creatureCards_starter for testing
        
        for (int i = 0; i < creatureCard_array.Length; i++)
        {
            if(count > 0)
            {
                creatureCard_array[i].SetActive(true);
                creatureCard_array[i].GetComponent<CardProperty>().cardData = creatureData.creatureCards_starter[i]; //TEMP: Changed creatureCards_available to creatureCards_starter for testing
                creatureBigCard_array[i].SetActive(true);
                creatureBigCard_array[i].GetComponent<CardProperty>().cardData = creatureData.creatureCards_starter[i]; //TEMP: Changed creatureCards_available to creatureCards_starter for testing
                count --;           
            }
            else
            {
                creatureCard_array[i].SetActive(false);
                creatureBigCard_array[i].SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        popUp_PanelDisplay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popUp_PanelDisplay.SetActive(false);
    }
}
