/// <remarks>
/// Author: Chase Bennett-Hill
/// Date Created: 24 / 07 / 2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This Class Handles the Player's Input and Movement such as swiping and touching the screen.
/// </summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private bool canSwipe; //Prevents the player from swiping several without lifting their finger

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        canSwipe = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// This method is called during a touch event
    /// </summary>

    public void Touch(InputAction.CallbackContext context)
    {
        
        if(context.phase == InputActionPhase.Started)
        {
            Debug.Log("Touch Begin");

        }
        if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log("Touch End");
            canSwipe = true;
        }
    }
    /// <summary>
    /// This method is called during a swipe event, and adds force to the player object in the direction of the swipe
    /// </summary>
    public void OnSwipe(InputAction.CallbackContext context)
    {
        Debug.Log("Swipe");
        if (!canSwipe)
        {
            return;
        }
        canSwipe = false;
        Vector2 swipeDirection = context.ReadValue<Vector2>();
        if (swipeDirection.x == float.NaN ||swipeDirection.y == float.NaN || swipeDirection.x == Mathf.Infinity || swipeDirection.y == Mathf.Infinity)
        {
            return;
        }
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(swipeDirection.x, 0, swipeDirection.y) * 0.05f , ForceMode.Impulse);
    }


}