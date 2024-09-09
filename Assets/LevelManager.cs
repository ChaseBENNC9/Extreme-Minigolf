using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject straightPrefab, finalPrefab;
    [SerializeField]
    private List<GameObject> hazardPrefabs = new ();

    [HideInInspector] public GameObject currentTrack;

[SerializeField]
    private List<GameObject> tracksInMap = new ();
    public static LevelManager instance;
    public bool finalGenerated = false;

    public TextMeshProUGUI scoreText;
    public int score;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void GameOver(bool win = false)
    {
        if (win)
        {
            GameManager.gameState = GameState.WIN;
        }
        else
        {
            GameManager.gameState = GameState.LOSE;
        }
        GameManager.lastScore = score;
        Debug.Log("Score is " + score);
        Debug.Log("Last Score is " + GameManager.lastScore);
        MenuManager.i.EndGame();
    }
    public void UpdateScore()
    {
        int moves = PlayerController.i.sectionMoves;
        score += moves;
        scoreText.text = score.ToString();
        PlayerController.i.sectionMoves = 0;
    }
    void Start()
    {
    
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score = 0;
        scoreText.text = score.ToString();
        SortTracks();
    }

    void SortTracks()
    {
        tracksInMap.Sort((x, y) => x.transform.position.z.CompareTo(y.transform.position.z));
    }

    public void AddTrack(GameObject track)
    {
        foreach (GameObject t in tracksInMap)
        {
            if (t.transform.position.z == track.transform.position.z)
            {
                Debug.LogWarning("Track already exists");
               return;
            }
        }
        tracksInMap.Add(track);
        SortTracks();
    }

    public void RemoveFirstTrack()
    {
        if (tracksInMap.Count == 0 || tracksInMap[0] == currentTrack)
        {
            return;
        }
        Destroy(tracksInMap[0]);
        tracksInMap.RemoveAt(0);
        SortTracks();
    }
    
    public void GenerateSection()
    {
        if(finalGenerated)
            return;
        if (hazardPrefabs.Count == 0)
        {
            GenerateNormal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 15));
            GenerateFinal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 30));
            finalGenerated = true;
            return;
        }
        GenerateNormal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 15));
        GenerateHazard(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 30));
    }
    public void GenerateNormal(Vector3 position)
    {
        Instantiate(straightPrefab, position, Quaternion.identity);
    }

    public void GenerateFinal(Vector3 position)
    {
        Instantiate(finalPrefab, position, Quaternion.identity);
    }

    public void GenerateHazard(Vector3 position)
    {
        if (hazardPrefabs.Count == 0)
        {
            return;
        }
        int i = Random.Range(0, hazardPrefabs.Count);



        Instantiate(hazardPrefabs[i], position, Quaternion.identity);
        hazardPrefabs.RemoveAt(i); // Only 1 hazard of each type

    }
}
