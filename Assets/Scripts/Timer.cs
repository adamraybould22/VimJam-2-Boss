using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{   
    [Tooltip("The Starting Time of the Timer")]
    public float startingTime;

    [Tooltip("The Current Countdown Time")]
    public float currentTime;

    private Text TimerGUI;

    private void Start()
    {
        TimerGUI = GetComponent<Text>();

        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;   
        TimerGUI.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0;
            SceneManager.LoadScene("Winner");
        }
    }
}
