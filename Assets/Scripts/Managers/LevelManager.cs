using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// LevelManager class is used to manage the level and generate the tracks
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject straightPrefab, finalPrefab;
    [SerializeField]
    private List<GameObject> hazardPrefabs = new ();

    [HideInInspector] public GameObject currentTrack;

[SerializeField]
/// <summary>
/// List of tracks in the map that have been generated
/// </summary>
    private List<GameObject> tracksInMap = new ();
    public static LevelManager instance;
    /// <summary>
    /// Flag to check if the final track has been generated , when true no more tracks will be generated
    /// </summary>
    public bool finalGenerated = false;

    public TextMeshProUGUI scoreText;
    public int score;
    /// <summary>
    /// Controls which level this instance of the class is managing, locked to 3 levels
    /// </summary>
    [Range(1, 3)]
    [SerializeField] private int level = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
    
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score = 0;
        scoreText.text = score.ToString();
        SortTracks();
        RangeOfHazard(false);
        GameManager.currentLevel = level;
    }

/// <summary>
/// Changes the color of the score text to indicate if the player is in a hazard, this determines if the player will get a score or not
/// </summary>
/// <param name="range"></param>
    public void RangeOfHazard(bool range)
    {
        if (range)
        {
           scoreText.color = new Color32(65, 120, 54, 255);
        }
        else
        {
           scoreText.color = new Color32(142, 147, 141, 146);
        }
    }
    /// <summary>
    /// Ends the game and displays the end screen
    /// </summary>
    /// <param name="win">Flag determines if the game ends as a win or loss</param>
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

    /// <summary>
    /// Updates the score text and resets the moves for the section
    /// </summary>
    public void UpdateScore()
    {
        int moves = PlayerController.i.sectionMoves;
        score += moves;
        scoreText.text = score.ToString();
        PlayerController.i.sectionMoves = 0;
    }

/// <summary>
///  Sorts the tracks in the map by their z position so the next track to be generated is always the one in front
/// </summary>
    void SortTracks()
    {
        tracksInMap.Sort((x, y) => x.transform.position.z.CompareTo(y.transform.position.z));
    }

/// <summary>
/// Adds a track to the list of tracks in the map
/// </summary>
/// <param name="track"></param>
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


/// <summary>
/// Removes the first track in the list of tracks in the map
/// </summary>
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
    
    /// <summary>
    /// Generates a new section of the map, a section consists of a normal track and a hazard track
    /// </summary>
    public void GenerateSection()
    {
        if(finalGenerated)
            return;
        if (hazardPrefabs.Count == 0) // No more hazards to generate
        {
            GenerateNormal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 15));
            GenerateFinal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 30));
            finalGenerated = true;
            return;
        }
        GenerateNormal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 15));
        GenerateHazard(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 30));
    }
    /// <summary>
    /// Generates a normal track
    /// </summary>
    /// <param name="position"></param>
    public void GenerateNormal(Vector3 position)
    {
        Instantiate(straightPrefab, position, Quaternion.identity);
    }

/// <summary>
/// Generates the final track
/// </summary>
/// <param name="position"></param>
    public void GenerateFinal(Vector3 position)
    {
        Instantiate(finalPrefab, position, Quaternion.identity);
    }

/// <summary>
///  Generates a hazard track
/// </summary>
/// <param name="position"></param>
    public void GenerateHazard(Vector3 position)
    {
        if (hazardPrefabs.Count == 0)
        {
            return;
        }
        int i = Random.Range(0, hazardPrefabs.Count); //Select a random hazard from the list



        Instantiate(hazardPrefabs[i], position, Quaternion.identity);
        hazardPrefabs.RemoveAt(i); // remove the selected hazard from the list

    }
}
