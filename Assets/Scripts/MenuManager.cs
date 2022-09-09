using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string namePlayer;
    }

    public void SaveNamePlayer()
    {
        SaveData data = new SaveData();
        data.namePlayer = MenuUIHandler.nameEntered;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    public string LoadNamePlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            return data.namePlayer;
        }
        return "";
    }


    [System.Serializable]
    class SaveDataScore
    {
        public int bestScore;
        public string playerBestScore;
    }
    public (int, string) LoadOverallBestScore()
    {
        string path = Application.persistentDataPath + "/savefilescore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataScore dataScore = JsonUtility.FromJson<SaveDataScore>(json);

            return (dataScore.bestScore, dataScore.playerBestScore);
        }
        return (0, "");
    }
}
