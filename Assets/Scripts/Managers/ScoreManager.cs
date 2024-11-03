using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// ScoreManager class is used to display the scores of the levels
/// </summary>
public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreText;
    void Start()
    {
        string[] score = new string[3];
        score[0] = "Level 1: " + GameManager.levels[0].score;
        score[1] = "Level 2: " + GameManager.levels[1].score;
        score[2] = "Level 3: " + GameManager.levels[2].score;
        scoreText.GetComponent<TextMeshProUGUI>().text = score[0] + "\n" + score[1] + "\n" + score[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
