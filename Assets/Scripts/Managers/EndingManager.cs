using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// EndingManager class is used to manage the ending of the game, whether the player wins or loses
/// </summary>
public class EndingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject loserParent, winnerParent;


    // Start is called before the first frame update
    void Start()
    {
        winnerParent.SetActive(false);
        loserParent.SetActive(false);
        if (GameManager.gameState == GameState.WIN)
        {
            Winner();
        }
        else if (GameManager.gameState == GameState.LOSE)
        {
            Loser();
        }
        else
        {
            Debug.LogError("Game State not set");
            Debug.LogError(GameManager.gameState);

        }
    }

/// <summary>
/// If the player wins, the winner screen is displayed, checks if the score is a new best score
/// </summary>
    private void Winner()
    {
        Debug.Log("Winner" + GameManager.lastScore);
        winnerParent.SetActive(true);
        GameObject.Find("best").GetComponent<TextMeshProUGUI>().text = "";
        loserParent.SetActive(false);
        scoreText.text = GameManager.lastScore.ToString();
        if ((SaveGame.GetScoreForLevel(GameManager.currentLevel)) == 0 || (SaveGame.GetScoreForLevel(GameManager.currentLevel) > 0 && SaveGame.GetScoreForLevel(GameManager.currentLevel) > GameManager.lastScore))
        {
            SaveGame.UpdateLevelScore(GameManager.currentLevel, GameManager.lastScore);
            Debug.Log("New Best Score");
        GameObject.Find("best").GetComponent<TextMeshProUGUI>().text = "New Best Score!";
        }
        else
        {
            Debug.Log("Not a best score");
            Debug.Log("Current Best Score: " + SaveGame.GetScoreForLevel(GameManager.currentLevel) + " Last Score: " + GameManager.lastScore);
        }

    }
    /// <summary>
    /// If the player loses, the loser screen is displayed
    /// </summary>
    private void Loser()
    {
        winnerParent.SetActive(false);
        loserParent.SetActive(true);
    }
}
