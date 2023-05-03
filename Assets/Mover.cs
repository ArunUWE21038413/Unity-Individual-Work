using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Rails rails;

    private int currentSeg;
    private float transition;
    private bool isCompleted;
    private Camera camera;
    private Camera mainCam;

    private void Start()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        mainCam = Camera.main;
    }


    private void Update()
    {
        if (!rails)
            return;

        if (!isCompleted)
            Play();
        else 
        {
            camera.enabled = false;
            mainCam.enabled = true;
        }
        
        
    }

    private void Play()
    {
        transition += Time.deltaTime * 1 / 2.5f;
        if(transition > 1) 
        {
            transition = 0;
            currentSeg++;
        }
        else if(transition < 0)
        {
            transition = 1;
            currentSeg--;
        }

        if(rails.nodes.Length == currentSeg) 
        {
            isCompleted = true;
           
        }


        transform.position = rails.LinearPosition(currentSeg, transition);
        transform.rotation = rails.Orientation(currentSeg, transition);
    }
}
