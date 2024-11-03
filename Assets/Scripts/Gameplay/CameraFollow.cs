using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// CameraFollow class is used to make the camera follow the golf ball
/// </summary>
public class CameraFollow : MonoBehaviour
{
    private float offset; // offset between the camera and the golf ball
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position.z - GameObject.Find("GolfBall").transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.Find("GolfBall").transform.position.z + offset);
    }
}
