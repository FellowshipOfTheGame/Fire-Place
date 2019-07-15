using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPauseScript : MonoBehaviour
{
	public GameObject volSlider, quality;

    // Update is called once per frame
    void Update()
    {
		AudioListener.volume = volSlider.GetComponent<Slider>().value;

		switch (quality.GetComponent<Dropdown>().value)
		{
			case 0:
				QualitySettings.SetQualityLevel((int)QualityLevel.Fast);
				break;
			case 1:
				QualitySettings.SetQualityLevel((int)QualityLevel.Good);
				break;
			case 2:
				QualitySettings.SetQualityLevel((int)QualityLevel.Fantastic);
				break;
		}
    }

	public void SairJogo()
	{
		Application.Quit();
	}
}
