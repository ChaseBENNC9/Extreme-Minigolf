using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Update is called once per frame
    private void Winner()
    {
        Debug.Log("Winner" + GameManager.lastScore);
        winnerParent.SetActive(true);
        GameObject.Find("best").SetActive(false);
        loserParent.SetActive(false);
        scoreText.text = GameManager.lastScore.ToString();
        if (SaveGame.GetScoreForLevel(GameManager.currentLevel) >= 0 && SaveGame.GetScoreForLevel(GameManager.currentLevel) < GameManager.lastScore)
        {
            GameObject.Find("best").SetActive(true);
            SaveGame.UpdateLevelScore(GameManager.currentLevel, GameManager.lastScore);
        }

    }
    private void Loser()
    {
        winnerParent.SetActive(false);
        loserParent.SetActive(true);
    }
}
