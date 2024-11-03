using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ProjectionLine class is used to change the color of the projection line based on the distance between the start and end points
/// </summary>
public class ProjectionLine : MonoBehaviour
{
    public Gradient fullGradient;
    public Gradient mediumGradient;
    public Gradient lowGradient;

    public const float FULL_THRESHOLD = 2.5f;
    public const float MEDIUM_THRESHOLD = 1.5f;

    void Update()
    {
        if(PlayerController.i.startPos != Vector3.zero && PlayerController.i.endPos != Vector3.zero)
        {
            float distance = Vector3.Distance(PlayerController.i.startPos, PlayerController.i.endPos);
            if(distance >= FULL_THRESHOLD)
            {
                PlayerController.i.line.colorGradient = fullGradient;
            }
            else if(distance >= MEDIUM_THRESHOLD)
            {
                PlayerController.i.line.colorGradient = mediumGradient;
            }
            else
            {
                PlayerController.i.line.colorGradient = lowGradient;
            }
        }
    }
}
