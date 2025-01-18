using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    private Button startButton;
    private Button settingButton;
    private Button progressButton;
    private Button exitButton;

    private void Awake()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        settingButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        progressButton = GameObject.Find("ProgressButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
    }

    void Start()
    {
        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene("02-LevelSelect");
        });
    }

    
    void Update()
    {
        
    }
}
