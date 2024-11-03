using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Hole class is used to detect if the golf ball has fallen into the hole
/// </summary>
public class Hole : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GolfBall")
        {
            
            LevelManager.instance.GameOver(true);
        }

    }
}
