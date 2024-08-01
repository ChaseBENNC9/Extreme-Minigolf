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

    public Vector3 startPos; //The starting position of the swipe
    public Vector3 endPos; //The ending position of the swipe
    public static PlayerController i;
    public LineRenderer line;




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

    }

    public void Launch(Vector3 delta)
    {
        Vector3 direction = new Vector3(-delta.x, 0, -delta.z);
        gameObject.GetComponent<Rigidbody>().AddForce(direction , ForceMode.Impulse);
        
    }

}
