using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// FinishFlag class is used to display the current level on the finish flag
/// </summary>
public class FinishFlag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = GameManager.currentLevel.ToString();
    }


}
