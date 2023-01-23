using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectedCreaturePanelManager : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler

{
    [SerializeField] PlayerGameData PlayerGameData;
    [SerializeField] Creature_ScriptableObj creatureData;
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

    public void UpdateCreatureData()
    {
        creatureData = PlayerGameData.creatureCollection_activeTeam[transform.GetSiblingIndex()];
    }

    void UpdateCreatureCardArray()
    {
        int count = creatureData.creatureCards_available.Count;
        
        for (int i = 0; i < creatureBigCard_array.Length; i++)
        {
            if(count > 0)
            {
                creatureBigCard_array[i].SetActive(true);
                creatureBigCard_array[i].GetComponent<CardProperty>().cardData = creatureData.creatureCards_available[i];
                count --;           
            }
            else
            {
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
