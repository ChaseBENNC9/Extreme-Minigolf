using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Track : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.AddTrack(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GolfBall")
        {
            LevelManager.instance.currentTrack = this.gameObject;
            LevelManager.instance.GenerateSection();
        }

    }

}
