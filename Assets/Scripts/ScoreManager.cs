using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public int scoreToEndGame = 15;
    public string menuSceneName = "1 Start Scene";

    // --- NUEVO ---
    public List<string> collectedOrder = new List<string>();
    public List<string> correctOrder = new List<string> { "A", "B", "C" }; // Cambia los nombres según tus objetos
    public string failScene = SceneManager.GetActiveScene().name;   // Escena si el orden es incorrecto

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score actual: " + score);

        if (score > scoreToEndGame)
        {
            score = 0;
            Debug.Log("Puntaje alcanzado, regresando al menú...");
            SceneManager.LoadScene(menuSceneName);
        }
    }

    // --- NUEVO ---
    public void RegisterCollection(string id)
    {
        collectedOrder.Add(id);

        if (collectedOrder.Count == correctOrder.Count)
        {
            bool isCorrect = true;
            for (int i = 0; i < correctOrder.Count; i++)
            {
                if (collectedOrder[i] != correctOrder[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect == false)
            {
                Debug.Log("Orden incorrecto. Reiniciando escena...");
                collectedOrder.Clear();
                menuSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(failScene);
            }
        }
    }
}
