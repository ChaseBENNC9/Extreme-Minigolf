using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public enum TrackTypes
{
    Regular,
    Hazard,
    Final
}
public class Track : MonoBehaviour
{
    [SerializeField]
    private TrackTypes type;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.AddTrack(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GolfBall")
        {
            if( type == TrackTypes.Hazard)
            {
                PlayerController.i.sectionMoves = 0;
                Debug.Log("Hazard Start");

            }
            LevelManager.instance.currentTrack = this.gameObject;

            LevelManager.instance.GenerateSection();

        }

    }

        void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "GolfBall")
        {
            this.GetComponent<BoxCollider>().isTrigger = false;

            if (type == TrackTypes.Hazard)
            {
                LevelManager.instance.UpdateScore();
                Debug.Log("Hazard End");
            }
        }

    }

}
