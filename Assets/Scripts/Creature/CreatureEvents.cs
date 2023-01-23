using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreatureEvents : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField] CreatureProperty CreatureProperty;
    [SerializeField] GameObject e_display;

    PlayerPanelDisplay CreaturePanelDisplay;
    CreatureSelection_p CreatureSelection_p;
    GameObject target;

    void Start()
    {
        CreatureSelection_p = GameObject.Find("Game Manager").GetComponent<CreatureSelection_p>();

        if(CreatureProperty.TeamSide == GameReferences.TeamSide.Player)
        {
            CreaturePanelDisplay = GameObject.Find("p_Creature Panel Display").GetComponent<PlayerPanelDisplay>();
        }
        else
        {
            CreaturePanelDisplay = GameObject.Find("e_Creature Panel Display").GetComponent<PlayerPanelDisplay>();
            e_display.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        if(CreatureProperty != null && CreatureProperty.TeamSide == GameReferences.TeamSide.Player)
        {
            CreaturePanelDisplay.UpdatePlayerCreaturePanel(CreatureProperty);
        }

        if(CreatureProperty != null && CreatureProperty.TeamSide == GameReferences.TeamSide.Enemy)
        {
            e_display.SetActive(true);
            CreaturePanelDisplay.UpdatePlayerCreaturePanel(CreatureProperty);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(CreatureSelection_p.currentSelectedCreature != null)
        {
            CreaturePanelDisplay.UpdatePlayerCreaturePanel(CreatureSelection_p.currentSelectedCreature.GetComponent<CreatureProperty>());
        }

        if(CreatureProperty.TeamSide == GameReferences.TeamSide.Enemy)
        {
            e_display.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if(CreatureProperty.TeamSide == GameReferences.TeamSide.Player
        && TestCreatureSelecteable())
        {
            int index = CreatureProperty.creatureTeamPosition;
            CreatureSelection_p.SetCurrentSelectedCreature(index);
        }
    }

    bool TestCreatureSelecteable()
    {
        var avaliableCreatures = CreatureSelection_p.p_avaliableCreatures;
        for(int i = 0 ; i < avaliableCreatures.Count; i++)
        {
            if(avaliableCreatures[i] == CreatureProperty.creatureTeamPosition)
            {
                return true;
            }
        }
        return false;
    }


}
