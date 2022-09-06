using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class MainManagerMenu : MonoBehaviour
{
    public static MainManagerMenu Instance;
    public TextMeshProUGUI playerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadNamePlayer();
    }

    [System.Serializable]
    class SaveData
    {
        public string namePlayer;
    }

    public void SaveNamePlayer()
    {
        SaveData data = new SaveData();
        data.namePlayer = playerName.text;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNamePlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName.text = data.namePlayer;
        }
    }
}
