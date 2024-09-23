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
using RDG;

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
        SetRespawn(gameObject.transform.position);
        LivesRemaining = 3;


    }

    public void Launch(Vector3 delta)
    {
        Vector3 direction = new Vector3(-delta.x, 0, -delta.z);
        sectionMoves++;
        gameObject.GetComponent<Rigidbody>().AddForce(direction * speedModifier, ForceMode.Impulse);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {

            GetComponent<AudioSource>().Play();
            #if UNITY_ANDROID && !UNITY_EDITOR
            Vibration.Vibrate(10);
            #endif
        }
    }
    

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("OutOfBounds"))
        {
            if (LivesRemaining > 1)
            {
                {
                    lifeIcons[LivesRemaining - 1].SetActive(false);
                    LivesRemaining--;
                    Debug.Log("Lives Remaining: " + LivesRemaining);
                    Respawn();
                }
            }
            else
            {
                LevelManager.instance.GameOver();
            }
        }
        
    }

    public void Respawn()
    {
        gameObject.transform.position = RespawnPosition;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        LevelManager.instance.currentTrack.GetComponent<BoxCollider>().isTrigger = true;
    }
    public void SetRespawn(Vector3 position)
    {
        RespawnPosition = new Vector3(0, 0.57f, position.z);
    }

}
