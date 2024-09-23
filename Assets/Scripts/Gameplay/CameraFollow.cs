using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float offset;
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
