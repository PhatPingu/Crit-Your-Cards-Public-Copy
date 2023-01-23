using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBuffDisplay : MonoBehaviour
{
    [SerializeField] GameObject[] buffArray;

    void Start()
    {
        foreach (var buff in buffArray)
        {
            buff.SetActive(false);
        }
    }

    public void ShowRetaliate(bool choice)
    {
        buffArray[0].SetActive(choice);
    }

    public void ShowBurn(bool choice)
    {
        buffArray[1].SetActive(choice);
    }

    public void ShowSleep(bool choice)
    {
        buffArray[2].SetActive(choice);
    }

    public void ShowHidden(bool choice)
    {
        buffArray[3].SetActive(choice);
    }

    public void ShowTaunt(bool choice)
    {
        buffArray[4].SetActive(choice);
    }

    public void ShowShielded(bool choice)
    {
        buffArray[5].SetActive(choice);
    }
}
