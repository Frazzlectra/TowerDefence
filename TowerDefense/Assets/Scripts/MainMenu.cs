﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button playBtn;
    public Button quitBtn;
    public Canvas HudCanvas;
    public GameManager gameManager;
    public static Text mainMenuText;

    void Awake()
    {        
        playBtn.onClick.AddListener(() => { ButtonClicked("play"); });
        quitBtn.onClick.AddListener(() => { ButtonClicked("quit"); });
        mainMenuText = GetComponentInChildren<Text>();
        mainMenuText.text = "MainMenu";
    }

    private void ButtonClicked(string btn)
    {
        switch(btn)
        {
            case "play":                
                HudCanvas.gameObject.SetActive(true);
                Debug.Log("Play");
                gameObject.SetActive(false);
                gameManager.gameObject.SetActive(true);
                break;
            case "quit":
                Application.Quit();
                break;
        }
    }
}
