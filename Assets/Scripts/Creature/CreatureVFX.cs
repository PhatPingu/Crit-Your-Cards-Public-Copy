using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureVFX : MonoBehaviour
{
    [SerializeField]
    public GameObject CreatureSelected;
    //public ParticleSystem CastEffect;
    [SerializeField] GameObject creatureVFX;
        

    void Start()
    {
        CreatureSelectedVFX(false);
    }

    public void CreatureSelectedVFX(bool choice)
    {
        CreatureSelected.SetActive(choice);
    }

    public void CastEffect_VFX(GameObject effect_vfx = null)
    {
        if(effect_vfx != null)
        {
            var effect = Instantiate(effect_vfx, transform, false);
            effect_vfx.GetComponent<ParticleSystem>().Play();
        }
    }
}
