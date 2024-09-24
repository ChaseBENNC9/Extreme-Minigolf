using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillSpin : MonoBehaviour
{
    public float animationSpeed = 1;
    private float AnimationSpeed
    {
        get { return AnimationSpeed; }
        set
        {
            AnimationSpeed = value;

        }
    }
    private Animator ac;
    // Start is called before the first frame update
    void Start()
    {
        ac = gameObject.transform.Find("spin").GetComponent<Animator>();
        ac.SetFloat("speed", animationSpeed);
    }

}
