using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using FirePlace.FX;

namespace FirePlace.UI
{

	public class OptionsMenu : MonoBehaviour
	{

		public Slider volSlider;

		public Dropdown quality;

		public Toggle ambientOcclusion;
		public Toggle bloom;
		public Toggle fxaa;

		void Start()
		{

			if(PlayerPrefs.HasKey("Volume"))
				volSlider.value = PlayerPrefs.GetFloat("Volume");
			else
			{
				PlayerPrefs.SetFloat("Volume", 1.0f);
				volSlider.value = 1.0f;
			}

			UpdateVolume();

			quality.value = QualitySettings.GetQualityLevel();

			if(PlayerPrefs.HasKey("AO"))
				ambientOcclusion.isOn = (PlayerPrefs.GetInt("AO") == 1);
			else
			{
				PlayerPrefs.SetInt("AO", 1);
				ambientOcclusion.isOn = true;
			}
			UpdateAO();

			if(PlayerPrefs.HasKey("Bloom"))
				bloom.isOn = (PlayerPrefs.GetInt("Bloom") == 1);
			else
			{
				PlayerPrefs.SetInt("Bloom", 1);
				bloom.isOn = true;
			}
			UpdateBloom();

			if(PlayerPrefs.HasKey("FXAA"))
				fxaa.isOn = (PlayerPrefs.GetInt("FXAA") == 1);
			else
			{
				PlayerPrefs.SetInt("FXAA", 1);
				fxaa.isOn = true;
			}
			UpdateFXAA();

		}

		public void UpdateVolume()
		{

			AudioListener.volume = volSlider.value;
			PlayerPrefs.SetFloat("Volume", volSlider.value);

		}

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

		public void UpdateAO()
		{

			EffectController.UpdateAO(ambientOcclusion.isOn);

			if(ambientOcclusion.isOn)
				PlayerPrefs.SetInt("AO", 1);
			else
				PlayerPrefs.SetInt("AO", 0);

		}

		public void UpdateBloom()
		{

			EffectController.UpdateBloom(bloom.isOn);

			if(bloom.isOn)
				PlayerPrefs.SetInt("Bloom", 1);
			else
				PlayerPrefs.SetInt("Bloom", 0);

		}

		public void UpdateFXAA()
		{

			AAController.UpdateAA(fxaa.isOn);

			if(fxaa.isOn)
				PlayerPrefs.SetInt("FXAA", 1);
			else
				PlayerPrefs.SetInt("FXAA", 0);

		}

		public void ExitGame()
		{
			Application.Quit();
		}

	}

}
