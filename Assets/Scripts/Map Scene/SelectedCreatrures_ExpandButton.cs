using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedCreatrures_ExpandButton : MonoBehaviour
{
    [SerializeField] GameObject allPanels;
    [SerializeField] GameObject otherButtonState;
    
    [SerializeField] DeckPreviewPanelManager DeckPreviewPanelManager;

    [SerializeField] Vector3 untoggledPosition;
    [SerializeField] Vector3 toggledPosition;



    public void ExpandPanel(bool choice)
    {
        if(choice)
        {
            DeckPreviewPanelManager.UpdateDeckCards();
            
            allPanels.transform.localPosition = toggledPosition;
            otherButtonState.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            allPanels.transform.localPosition = untoggledPosition;
            otherButtonState.SetActive(true);
            this.gameObject.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}
