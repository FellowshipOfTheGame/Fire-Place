using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

	public Slider volSlider;
	public Dropdown quality;

	public void UpdateQuality()
	{
		switch (quality.value)
		{
			case 0:
				QualitySettings.SetQualityLevel(0);
				break;
			case 1:
				QualitySettings.SetQualityLevel(1);
				break;
			case 2:
				QualitySettings.SetQualityLevel(2);
				break;
			case 3:
				QualitySettings.SetQualityLevel(3);
				break;
		}
	}

	public void UpdateVolume()
	{
		AudioListener.volume = volSlider.value;
	}

	public void ExitGame()
	{
		Application.Quit();
	}

}
