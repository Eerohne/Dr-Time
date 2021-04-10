using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string scene;
    // Update is called once per frame
    void Update()
    {
        //once the space button is pressed to the scene switches to the scene desired
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
