using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameText;
    public static string nameEntered;
    public TextMeshProUGUI BestScoreMenuText;

    private void Start()
    {
        BestScoreMenuText.SetText("Best Score " + MenuManager.Instance.LoadOverallBestScore().Item1 + " by " + MenuManager.Instance.LoadOverallBestScore().Item2);
        LoadNameEntered();
    }

    public void StartNew()
    {
        nameEntered = nameText.text;
        SaveNameEntered();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        // Application.Quit(); for exiting app in build mode
        // EditorApplication.ExitPlaymode(); for exiting app in editor testing mode
        EditorApplication.ExitPlaymode(); 

    }

    public void SaveNameEntered()
    {
        MenuManager.Instance.SaveNamePlayer();
    }

    public void LoadNameEntered()
    {
        nameText.text = MenuManager.Instance.LoadNamePlayer();

    }
}
