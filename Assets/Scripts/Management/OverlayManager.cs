using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject feedbackPrompt;
    [SerializeField] GameObject menuWindow;
    bool menuOpened = false;

    void Start()
    {
        if(startButton != null)
        {
            startButton.SetActive(true);
        }
    }

    void Update()
    {
        DetectInput_ESC();
    }

    void DetectInput_ESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !feedbackPrompt.activeInHierarchy)
        {
            OpenMenu(menuOpened);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && feedbackPrompt.activeInHierarchy)
        {
            feedbackPrompt.SetActive(false);
        }
    }

    public void OpenMenu(bool choice)
    {
        if(choice)
        {
            menuWindow.SetActive(false);
            menuOpened = false;
        }
        else if (!choice)
        {
            menuWindow.SetActive(true);
            menuOpened = true;
        }
    }
    
}
