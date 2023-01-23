using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatHistory_OnHoverManager : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public CombatHistoryDisplay_TeamAction CombatHistoryDisplay_TeamAction;

    [SerializeField] CombatHistoryDisplay_Manager     CombatHistoryDisplay_Manager;
        [SerializeField] List<Card_ScriptableObj>     Card_list;
        [SerializeField] List<Creature_ScriptableObj> Caster_list;
        [SerializeField] List<Creature_ScriptableObj> Target_list;
    
    [SerializeField] GameObject p_PopUp;
    [SerializeField] GameObject e_PopUp;
    [SerializeField] int thisIndexPos;

    [SerializeField] GameObject p_Card;
    [SerializeField] GameObject p_caster;
    [SerializeField] GameObject p_target;

    [SerializeField] GameObject e_Card;
    [SerializeField] GameObject e_caster;
    [SerializeField] GameObject e_target;

    void Start()
    {
        thisIndexPos = transform.GetSiblingIndex();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Card_list    = CombatHistoryDisplay_Manager.cardUsedHistory_list;
        Caster_list  = CombatHistoryDisplay_Manager.casterUsedHistory_list;
        Target_list  = CombatHistoryDisplay_Manager.targetUsedHistory_list;

        if(GetComponent<CardProperty>().teamSide == GameReferences.TeamSide.Player)
        {
            p_PopUp.SetActive(true);
            p_Card.GetComponent<CardProperty>().cardData = Card_list[thisIndexPos];
            p_caster.GetComponent<CreatureProperty>().creatureData = Caster_list[thisIndexPos];
            p_target.GetComponent<CreatureProperty>().creatureData = Target_list[thisIndexPos];
        }
        else
        {
            e_PopUp.SetActive(true);
            e_Card.GetComponent<CardProperty>().cardData = Card_list[thisIndexPos];
            e_caster.GetComponent<CreatureProperty>().creatureData = Caster_list[thisIndexPos];
            e_target.GetComponent<CreatureProperty>().creatureData = Target_list[thisIndexPos];
        }
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        if(GetComponent<CardProperty>().teamSide == GameReferences.TeamSide.Player)
        {
            p_PopUp.SetActive(false);
        }
        else
        {
            e_PopUp.SetActive(false);
        }
    }
}
