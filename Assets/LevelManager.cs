using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject straightPrefab;
    [SerializeField]
    private List<GameObject> hazardPrefabs = new ();

    public GameObject currentTrack;

[SerializeField]
    private List<GameObject> tracksInMap = new ();
    public static LevelManager instance;

    public TextMeshProUGUI scoreText;
    public int score;
    void Awake()
    {
        Debug.Log("LevelManager Awake");
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateScore()
    {
        int moves = PlayerController.i.sectionMoves;
        if (moves <= 1)
        {
            score += 3;
        }
        else if (moves <= 2)
        {
            score += 2;
        }
        else
        {
            score += 1;
        }
        scoreText.text = score.ToString();
        PlayerController.i.sectionMoves = 0;
    }
    void Start()
    {
        Debug.Log("LevelManager Start");
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
        currentTrack = tracksInMap[0];
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
        GenerateNormal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 15));
        GenerateHazard(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 30));
    }
    public void GenerateNormal(Vector3 position)
    {
        Instantiate(straightPrefab, position, Quaternion.identity);
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
