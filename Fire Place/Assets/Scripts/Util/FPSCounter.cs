using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{

    private bool show = false;
    private float fps = 0;

    private Coroutine calculateFPS = null;

    void Start() 
    {

        

    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.F8)) show =! show;

        if(show) 
        {
            if(calculateFPS == null)
            {
                fps = 1 / Time.deltaTime;
                calculateFPS = StartCoroutine(CalculateFPS());
            }
        }
        else
        {

            if(calculateFPS != null)
            {
                StopCoroutine(calculateFPS);
                calculateFPS = null;
            }

        }

    }

    IEnumerator CalculateFPS() 
    {

        // Samples FPS 100 times and them updates the counter with the average.
        float secFrames = 0;
        float counter = 0;

        while(show)
        {

            if(counter == 100)
            {

                fps = 1 / (secFrames / 100);
                secFrames = 0;
                counter = 0;

            } 
            else
            {

                secFrames += Time.deltaTime;
                counter++;

            }

            yield return null;

        }

    }

    void OnGUI()
    {


        if(show)
            GUI.Label(new Rect(10,10,200,200), "FPS: " + Mathf.Floor(fps));

    }

}
