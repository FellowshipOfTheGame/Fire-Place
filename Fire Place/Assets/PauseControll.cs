using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControll : MonoBehaviour
{
	public GameObject pauseStuff;

	private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (!paused)
			{
				paused = true;
				Time.timeScale = 0;

				pauseStuff.SetActive(true);
			}
			else
			{
				paused = false;
				Time.timeScale = 1;

				pauseStuff.SetActive(false);
			}
		}
    }

	public void Unpause()
	{
		paused = false;
		Time.timeScale = 1;

		pauseStuff.SetActive(false);
	}
}
