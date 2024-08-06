using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
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
    private void RemoveFirstTrack()
    {
        Destroy(tracksInMap[0]);
        tracksInMap.RemoveAt(0);
        SortTracks();
    }
    
    public void GenerateSection()
    {
        if (tracksInMap.Count == 0 || tracksInMap[0] == currentTrack || tracksInMap[0] == null)
        {
            return;
        }
        RemoveFirstTrack();
        GenerateNormal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 15));
        GenerateNormal(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 30));
        GenerateHazard(tracksInMap[tracksInMap.Count - 1].transform.position + new Vector3(0, 0, 45));
    }
    public void GenerateNormal(Vector3 position)
    {
        Instantiate(straightPrefab, position, Quaternion.identity);
    }

    public void GenerateHazard(Vector3 position)
    {
        int i = Random.Range(0, hazardPrefabs.Count);
        Instantiate(hazardPrefabs[i], position, Quaternion.identity);
    }
}
