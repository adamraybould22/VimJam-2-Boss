using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicator : MonoBehaviour
{
    // Reference to the Image of the Indicator
    public Image Icon;

    // Reference to the Animation of the Image
    private Animation Animation;

    void Awake()
    {
        Icon = transform.Find("Button").GetComponent<Image>();
        Animation = transform.Find("Button").GetComponent<Animation>();
    }

    // Sets the Indicator to be Active or Not
    public void SetIndicator(bool value)
    {
        gameObject.SetActive(value);
    }
}
