using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

public class MenuManager : MonoBehaviour
{
    private TMP_Text bestScoreText;

    private TMP_InputField inputField;

    void Start()
    {
        bestScoreText = GameObject.Find("BestScore").GetComponent<TMP_Text>();

        inputField = GameObject.Find("NameInput").GetComponent<TMP_InputField>();

        LoadBestScore();
    }

    public void StartGame()
    {
        // Get the player's name from the Input Field
        if (inputField != null) {
            string playerName = inputField.text;
            Debug.Log("Player Name: " + playerName);

            // Check that the player entered a name
            if (!string.IsNullOrEmpty(playerName)) {
                // Pass the name to the shared between scenes instance
                ScoreManager.Instance.currentPlayer = playerName;
            } else {
                Debug.LogWarning("No name was entered");
            }

        } else {
            Debug.LogError("InputField not found in the scene!");
        }

        // Load game's scene
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    private void LoadBestScore() {
        bestScoreText.text = $"Best Score : {ScoreManager.Instance.bestScore.playerName} : {ScoreManager.Instance.bestScore.score}";    
    }
}
