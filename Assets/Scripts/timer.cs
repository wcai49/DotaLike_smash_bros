using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float roundTime;
    public float reserveTime;

    Text timerText;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
        currentTime = roundTime;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = Mathf.RoundToInt(currentTime).ToString();
        if(currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }
        else
        {
            Debug.Log("Times Up!");
        }
    }
}
