using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string currentPlayer;

    public BestScore bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }


    [System.Serializable]
    public class BestScore
    {
        public string playerName;
        public int score;
    }

    public void SaveBestScore(string playerName, int score)
    {
        if (score > bestScore.score) {
            BestScore newBestScore = new BestScore();
            newBestScore.playerName = playerName;
            newBestScore.score = score;

            string json = JsonUtility.ToJson(newBestScore);
  
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

            bestScore = newBestScore;
        }
    }
    
    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScore savedBestScore = JsonUtility.FromJson<BestScore>(json);
            bestScore = savedBestScore;
        } else {
            bestScore = new BestScore();
            bestScore.playerName = "";
            bestScore.score = 0;
        }
    }
}
