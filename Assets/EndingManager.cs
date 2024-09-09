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
        if (GameManger.gameState == GameState.WIN)
        {
            Winner();
        }
        else if (GameManger.gameState == GameState.LOSE)
        {
            Loser();
        }
        else
        {
            Debug.LogError("Game State not set");
        }
    }

    // Update is called once per frame
    private void Winner()
    {
        winnerParent.SetActive(true);
        loserParent.SetActive(false);
        scoreText.text = LevelManager.instance.score.ToString();
    }
    private void Loser()
    {
        winnerParent.SetActive(false);
        loserParent.SetActive(true);
    }
}
