using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public bool activated = true;

    public float maxTimeInSeconds;

    int seconds;
    int minutes;

    string timerText;

    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    { 
        text = GetComponent<TextMeshProUGUI>();
        text.text = "--:--";
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            maxTimeInSeconds -= Time.deltaTime;

            minutes = Mathf.RoundToInt(maxTimeInSeconds) / 60;
            seconds = Mathf.RoundToInt(maxTimeInSeconds) - minutes * 60;

            timerText = minutes.ToString("D2") + ":" + seconds.ToString("D2");

            if (timerText.Equals(text.text)) return;

            text.text = timerText;

            if(maxTimeInSeconds <= 1 )//||((minutes == 0) == (seconds == 0)))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSystem>().life = 0f;
                activated = false;
            }
        }
    }
}
