using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDestroy : MonoBehaviour
{
    public float timerInSecs; // Timer before end of splash

    // Update is called once per frame
    void Update()
    {
        timerInSecs -= Time.deltaTime;

        if (timerInSecs <= 0f)
            Destroy(gameObject);
    }


}
