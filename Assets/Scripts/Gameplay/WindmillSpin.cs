using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// WindmillSpin class is used to make the windmill spin
/// </summary>
public class WindmillSpin : MonoBehaviour
{
    /// <summary>
    /// How fast the windmill spins
    /// </summary>
    public float animationSpeed = 1;
    private float AnimationSpeed
    {
        get { return AnimationSpeed; }
        set
        {
            AnimationSpeed = value;

        }
    }
    /// <summary>
    /// The Animator Controller
    /// </summary>
    private Animator ac;
    // Start is called before the first frame update
    void Start()
    {
        ac = gameObject.transform.Find("spin").GetComponent<Animator>();
        ac.SetFloat("speed", animationSpeed);
    }

}
