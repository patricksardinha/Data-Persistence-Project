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
    public TextMeshProUGUI nameText;
       
    private void Start()
    {
        LoadNameEntered();
        nameText.text = MainManagerMenu.Instance.playerName.text;
    }
    public void StartNew()
    {
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
        MainManagerMenu.Instance.SaveNamePlayer();
    }

    public void LoadNameEntered()
    {
        MainManagerMenu.Instance.LoadNamePlayer();
    }
}
