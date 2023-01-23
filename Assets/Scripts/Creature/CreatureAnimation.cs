using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimation : MonoBehaviour
{
    [SerializeField] CreatureProperty CreatureProperty;
    [SerializeField] GameObject creatureImage;
    [SerializeField] Canvas creatureDisplayCanvas;
    [SerializeField] RectTransform RectTransform;
    
    [SerializeField] float offset;

    GameObject GameManager;
    GameObject CMVirtualCamera;
    BG_Manager BG_Manager;

    int originalCanvasSortOrder;
    Vector3 originalPosition;
    Vector3 originalLocalPosition;
    float originalRotation;


    void Start()
    {
        GameManager     = GameObject.Find("Game Manager");
        CMVirtualCamera = GameObject.Find("CM Virtual Camera");
        BG_Manager      = GameObject.Find("BG_Area").GetComponent<BG_Manager>();

        originalPosition        = creatureImage.transform.position;
        originalLocalPosition   = creatureImage.transform.localPosition;
        originalCanvasSortOrder = creatureDisplayCanvas.sortingOrder;
        originalRotation        = RectTransform.localRotation.y; 
    }
    public void EndAnimations()
    {
        Attack_Animation(false);
        Spell_Animation(false);
        Heal_Animation(false);
    }

    public void Attack_Animation(bool playAnimation, GameObject target = null)
    {
        if(playAnimation)
        {
            var targetPos = target.transform.position;
            creatureImage.transform.position = new Vector2 (targetPos.x - OffSet(), targetPos.y - 1f);
            creatureDisplayCanvas.sortingOrder = 19;
            DoScreenShake(0.35f, 0.25f);
        }
        else
        {
            creatureImage.transform.localPosition = originalLocalPosition;
            creatureDisplayCanvas.sortingOrder = originalCanvasSortOrder;
        }

        float OffSet()
        {
            if(CreatureProperty.TeamSide == GameReferences.TeamSide.Enemy)
                return -offset;
            else 
                return offset;
        } 

        SetDesaturateFocus(playAnimation, target);
    }

    public void Spell_Animation(bool playAnimation, GameObject target = null)
    {
        if(playAnimation)
        {
            DoScreenShake(0.55f, 0.25f);
        }

        SetDesaturateFocus(playAnimation, target);
    }

    public void Heal_Animation(bool playAnimation, GameObject target = null)
    {
        
        if(playAnimation)
        {
            RectTransform.localRotation = new Quaternion(0f, originalRotation - 180f, 0f, 0f);
            DoScreenShake(0.15f, 0.25f);
        }
        
        if(!playAnimation)
        {
            RectTransform.localRotation = new Quaternion(0f, originalRotation, 0f, 0f);
        }

        SetDesaturateFocus(playAnimation, target);        
    }



    void SetDesaturateFocus(bool playAnimation, GameObject target = null)
    {
        if(playAnimation)
        {
            foreach(var creature in GameManager.GetComponent<GameReferences>().all_Creature_array)
            {
                creature.GetComponent<CreatureProperty>().SetDesaturateImage(true);
            }
            target.GetComponent<CreatureProperty>().SetDesaturateImage(false);
            CreatureProperty.SetDesaturateImage(false);
        }
        else
        {
            foreach(var creature in GameManager.GetComponent<GameReferences>().all_Creature_array)
            {
                creature.GetComponent<CreatureProperty>().SetDesaturateImage(false);
            }
        }

        BG_Manager.SetDessaturateBG(playAnimation);
    }

    public void DoScreenShake(float duration, float intensity)
    {
        CMVirtualCamera.GetComponent<CameraShake>().DoScreenShake(duration, intensity);
    }

}