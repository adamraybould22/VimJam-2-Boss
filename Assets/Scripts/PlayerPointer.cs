using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    [Tooltip("The Current Number of Points a Player has")]
    public int points;

    public Text pointText;

    public void AddPoint(int num)
    {
        points += num;
        pointText.text = points.ToString("0");
    }
}
