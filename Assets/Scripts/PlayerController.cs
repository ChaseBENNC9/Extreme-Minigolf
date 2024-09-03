/// <remarks>
/// Author: Chase Bennett-Hill
/// Date Created: 24 / 07 / 2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This Class Handles the Player's Input and Movement such as swiping and touching the screen.
/// </summary>


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public Vector3 startPos; //The starting position of the swipe
    [HideInInspector] public Vector3 endPos; //The ending position of the swipe
    [HideInInspector] public Vector3 RespawnPosition; //The direction of the swipe
    public float speedModifier;
    public static PlayerController i;
    public LineRenderer line;
    public int sectionMoves = 0;
    public int LivesRemaining;
    [SerializeField] private bool respawning = false;
    [SerializeField] private List<GameObject> lifeIcons = new List<GameObject>();



    void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        startPos = Vector3.zero;
        endPos = Vector3.zero;
        RespawnPosition = gameObject.transform.position;
        LivesRemaining = 3;

    }

    public void Launch(Vector3 delta)
    {
        Vector3 direction = new Vector3(-delta.x, 0, -delta.z);
        sectionMoves++;
        gameObject.GetComponent<Rigidbody>().AddForce(direction * speedModifier, ForceMode.Impulse);
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("OutOfBounds"))
        {
            gameObject.transform.position = RespawnPosition;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (LivesRemaining > 0)
            {
                if (!respawning)
                {
                    lifeIcons[LivesRemaining - 1].SetActive(false);
                    LivesRemaining--;
                    Debug.Log("Player has " + LivesRemaining + " lives remaining");
                    return;
                }
            }
            else
            {
                Debug.Log("Player has no lives remaining");
            }
            respawning = false;
        }
        
    }

}
