using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour
    , IPointerClickHandler
    , IPointerDownHandler
    , IPointerUpHandler
    , IBeginDragHandler
    , IDragHandler
    , IEndDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField] CardCastManager CardCastManager;
    [SerializeField] CardProperty CardProperty;
    [SerializeField] CardTargetCheck CardTargetCheck;
    [SerializeField] RectTransform cardDisplayRectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] LayoutElement LayoutElement;
    [SerializeField] LineRenderer LineRenderer;
    [SerializeField] EffectConditionalManager EffectConditionalManager;
    [SerializeField] CardTooltip CardTooltip;

    [SerializeField] CardEffect CardEffect1;
    [SerializeField] CardEffect CardEffect2;
    [SerializeField] CardEffect CardEffect3;

    GameObject GameManager;
    GameReferences GameReferences;
    CreatureSelection_p CreatureSelection_p;
    CardSelectionManager CardSelectionManager;
    ManaManager ManaManager;
    PlayerPanelDisplay PlayerPanelDisplay;

    int orginalSortOrder;

    Vector2 originalScale;
    [SerializeField] Vector2 onHoveScale;

    Vector3 originalParentPosition;
    Vector3 originalPosition;
    [SerializeField] Vector3 onHoverPosition;
    
    Quaternion originalRotation;
    Quaternion onHoverRotation;

    Vector3 mouseOffset;
    Vector3 mousePosition;

    public bool thisCardSelected;
    bool followMouseActive = false;


    void Start()
    {
        originalParentPosition = transform.position;

        originalScale       = cardDisplayRectTransform.localScale;
        originalPosition    = cardDisplayRectTransform.localPosition;
        originalRotation    = cardDisplayRectTransform.localRotation;
        
        onHoveScale         = new Vector2 (2f, 2f);
        onHoverPosition     = new Vector3(originalPosition.x, originalPosition.y + 10f, originalPosition.z); 
        onHoverRotation     = new Quaternion (0,0,0,0);

        GameManager          = GameObject.Find("Game Manager");
        GameReferences       = GameManager.GetComponent<GameReferences>();
        CreatureSelection_p  = GameManager.GetComponent<CreatureSelection_p>();
        CardSelectionManager = GameManager.GetComponent<CardSelectionManager>();
        ManaManager          = GameManager.GetComponent<ManaManager>();
        PlayerPanelDisplay   = GameObject.Find("p_Creature Panel Display").GetComponent<PlayerPanelDisplay>();

        orginalSortOrder     = canvas.sortingOrder;
        LineRenderer.enabled = false;
        
        CardCastFeedback(false);
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(1)) 
        {
            CardSelected(false);        
        }
        
        if(Input.GetMouseButtonDown(0) && followMouseActive == true) 
        {
            CastCard(); 
            CardSelected(false);          
        }
            
        FollowMouse();
        CardCastFeedback(followMouseActive);
    }

    void FollowMouse()
    {
        if(followMouseActive)
        {
            var screenPoint = mousePosition + mouseOffset;
            transform.position = new Vector2(screenPoint.x, screenPoint.y);
        }
    }

    void CastCard()
    {
        transform.position = originalParentPosition;
        CardCastManager.CastCard();
    }

// ---------- Mouse Events ----------
    public void OnPointerClick(PointerEventData eventData)
    {
        CardSelected(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalParentPosition = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CastCard();
    }
 
    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffset = transform.position - mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        CardSelected(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CardSelected(false);
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!CardSelectionManager.cardCurrentlySelected)
        {
            DisplayCardBig(true);
            HighlightAttributes(true);
            CardTooltip.SetTooltipsActive(true);
            ManaManager.PreviewManaUse(true, cardManaCost: CardProperty.cardData.manaCost);
        } 
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayCardBig(false);
        HighlightAttributes(false);
        CardTooltip.SetTooltipsActive(false);
        ManaManager.PreviewManaUse(false);
    }

    public void CardSelected(bool choice)
    {
        thisCardSelected = choice;
        followMouseActive = choice;        
        CardSelectionManager.cardCurrentlySelected = choice;
        GameReferences.Display_CardCast.GetComponent<DisplayCardCast>().DisplayCardBeingCast(choice, transform.gameObject);
        DisplayCardBig(false);
        CardTooltip.SetTooltipsActive(false);
    }

    void DisplayCardBig(bool choice)
    {
        if(choice)
        {
            LayoutElement.enabled = true;
            cardDisplayRectTransform.localScale    = onHoveScale;
            cardDisplayRectTransform.localPosition = onHoverPosition;
            cardDisplayRectTransform.localRotation = onHoverRotation;
            canvas.sortingOrder = 11;
        }
        else
        {
            cardDisplayRectTransform.localScale    = originalScale;
            cardDisplayRectTransform.localPosition = originalPosition;
            cardDisplayRectTransform.localRotation = originalRotation;
            canvas.sortingOrder = orginalSortOrder; 
            LayoutElement.enabled = false;
            LayoutElement.enabled = true; // Keep this: its a workaround for Unity bug?
            LayoutElement.enabled = false;// Keep this: its a workaround for Unity bug!
        }
    }

    void HighlightAttributes(bool choice)
    {
        bool _heal_BG    = false;
        bool _defense_BG = false;
        bool _attack_BG  = false;
        
        if(CardProperty.cardData.onHover_HighlightHealing)
            _heal_BG = choice;
        
        if(CardProperty.cardData.onHover_HighlightDefense)
            _defense_BG = choice;

        if(CardProperty.cardData.onHover_HighlightAttack)
            _attack_BG = choice;

        PlayerPanelDisplay.Highlight_AttributeBG(Heal_BG: _heal_BG, Defense_BG: _defense_BG, Attack_BG: _attack_BG);
    }

    void CardCastFeedback(bool choice)  //REFACTOR: Make this a class
    {
        LineRenderer.enabled = choice;
        if(choice) transform.position = (Vector2)mousePosition;
        cardDisplayRectTransform.gameObject.SetActive(!choice);
        

        var originalColliderSize = GetComponent<BoxCollider2D>().size;
        if (choice)
        {
            GetComponent<BoxCollider2D>().size = new Vector2( 3f, 3f);

            var castignCreature = CreatureSelection_p.currentSelectedCreature.transform.position;
            LineRenderer.SetPosition(0, new Vector3(castignCreature.x + 1.5f, castignCreature.y, 0f));
            LineRenderer.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, 0f));
        }
        else GetComponent<BoxCollider2D>().size = originalColliderSize;


        
        
        
        //REFACTOR: This changes the color of the Line Rend depending on what effects are eligeble for casting
        /*
        if(EffectConditionalManager.ConditionalTest(CardEffect1) && EffectConditionalManager.Condition(CardEffect1)
        && EffectConditionalManager.ConditionalTest(CardEffect2) && EffectConditionalManager.Condition(CardEffect2) 
        && CardTargetCheck.CheckTargetAllowed(CardCastManager.target, CardCastManager.castingTeam)

        || EffectConditionalManager.ConditionalTest(CardEffect1) && EffectConditionalManager.Condition(CardEffect1)
        && EffectConditionalManager.ConditionalTest(CardEffect2) && EffectConditionalManager.Condition(CardEffect2)
        && EffectConditionalManager.ConditionalTest(CardEffect3) && EffectConditionalManager.Condition(CardEffect3)
        && CardTargetCheck.CheckTargetAllowed(CardCastManager.target, CardCastManager.castingTeam))
        {
            LineRenderer.startColor = new Color(0, 1, 0.5450981f);
            LineRenderer.endColor = new Color(0.9384972f, 1, 0);
        }
        else if (EffectConditionalManager.ConditionalTest(CardEffect1) && EffectConditionalManager.Condition(CardEffect1)
        && CardTargetCheck.CheckTargetAllowed(CardCastManager.target, CardCastManager.castingTeam))
        {
            LineRenderer.startColor = new Color(0.2980392f, 0.6156863f, 0.972549f);
            LineRenderer.endColor = new Color(1, 1, 1);
        }
        else
        {
            LineRenderer.startColor = Color.red;
            LineRenderer.endColor = Color.white;
        }*/

    }
}
