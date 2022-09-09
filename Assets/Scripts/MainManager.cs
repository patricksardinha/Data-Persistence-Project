using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public static MainManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        DisplayBestScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void UpdateBestScore()
    {
        int currentBestScore = LoadBestScore().Item1;

        if (m_Points > currentBestScore)
        {
            SaveBestscore(m_Points);
        }
    }

    void DisplayBestScore()
    {
        BestScoreText.text = $"Best Score : {LoadBestScore().Item2} : {LoadBestScore().Item1}";
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        UpdateBestScore();
        m_GameOver = true;
        GameOverText.SetActive(true);

    }


    [System.Serializable]
    class SaveDataScore
    {
        public int bestScore = 0;
        public string playerBestScore;
    }

    public void SaveBestscore(int newBestScore)
    {
        SaveDataScore dataScore = new SaveDataScore();
        dataScore.bestScore = newBestScore;
        dataScore.playerBestScore = MenuManager.Instance.LoadNamePlayer();

        string json = JsonUtility.ToJson(dataScore);

        File.WriteAllText(Application.persistentDataPath + "/savefilescore.json", json);
    }

    public (int,string) LoadBestScore()
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
