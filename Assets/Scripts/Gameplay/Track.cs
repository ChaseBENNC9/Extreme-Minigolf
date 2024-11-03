using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Track Types are used to determine different the type of track
/// </summary>
public enum TrackTypes
{
    Regular, // Empty track
    Hazard, // Contains a hazard / obstacle
    Final // The final track containing the hole
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
/// <summary>
/// When the golf ball enters the trigger, the current track is updated and a new section is generated
/// </summary>
/// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GolfBall")
        {

            if( type == TrackTypes.Hazard)
            {
                PlayerController.i.sectionMoves = 0;
                LevelManager.instance.RangeOfHazard(true);

            }
            LevelManager.instance.currentTrack = this.gameObject;

            LevelManager.instance.GenerateSection();
            other.gameObject.GetComponent<PlayerController>().SetRespawn(this.transform.position);

        }

    }


/// <summary>
/// When the golf ball exits the trigger, the box collider upates to be solid to prevent the golf ball from going back
/// </summary>
/// <param name="other"></param>
        void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "GolfBall")
        {
            if (type == TrackTypes.Final)
            {
                return;
            }


            this.GetComponent<BoxCollider>().isTrigger = false;

            if (type == TrackTypes.Hazard)
            {
                LevelManager.instance.UpdateScore();
                LevelManager.instance.RangeOfHazard(false);
            }
        }

    }

}
