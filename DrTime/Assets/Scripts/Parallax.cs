using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length;
    private float startpos;

    public GameObject camera;
    //Intensity of the effect
    public float parallaxEffect;
       
    // Start is called before the first frame update
    void Start(){
        //Start x-position of the game object
        startpos = transform.position.x;

        //Gets the length (x-component) of the sprite which the script affects
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update(){
        float temp = (camera.transform.position.x * (1 - parallaxEffect));

        float distance = (camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        //changes the position of the background (gameObject) based on the camera's position
        if (temp > startpos + length){
            startpos += length;
        }
        else if(temp < startpos - length){
            startpos -= length;
        }
    }
}
