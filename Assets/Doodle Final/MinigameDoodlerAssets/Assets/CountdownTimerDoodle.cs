using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimerDoodle : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 80f;
    public DoodlePlayer Playerscript;

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if (Playerscript.dead == false && Playerscript.gamestart == true)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            Playerscript.OnPlayerDeath();
        }
    }

}