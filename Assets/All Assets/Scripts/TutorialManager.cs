using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public List<Button> gameHUDButtons;
    public UnityEvent startTutorialEvent;
    public bool isTutorialCompleted;
    private void Start()
    {
        Instance = this;
    }

    public void OnTutorialStart()
    {
        startTutorialEvent?.Invoke();
        foreach (var button in gameHUDButtons)
        {
            button.interactable = false;
        }
    }
    public void EndTutorial()
    {
        isTutorialCompleted = true;
        foreach (var button in gameHUDButtons)
        {
            button.interactable = true;
        }
    }
}
