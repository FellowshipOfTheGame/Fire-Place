using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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

		void Awake()
		{

			// Gets the Volume options.
			if(PlayerPrefs.HasKey("Volume"))
				volSlider.value = PlayerPrefs.GetFloat("Volume");
			else
			{
				PlayerPrefs.SetFloat("Volume", 1.0f);
				volSlider.value = 1.0f;
			}
			UpdateVolume();

			// Gets the Quality options.
			quality.value = QualitySettings.GetQualityLevel();

			// Gets the Ambient Occlusion options.
			if(PlayerPrefs.HasKey("AO"))
				ambientOcclusion.isOn = (PlayerPrefs.GetInt("AO") == 1);
			else
			{
				PlayerPrefs.SetInt("AO", 1);
				ambientOcclusion.isOn = true;
			}
			UpdateAO();

			// Gets the Bloom options.
			if(PlayerPrefs.HasKey("Bloom"))
				bloom.isOn = (PlayerPrefs.GetInt("Bloom") == 1);
			else
			{
				PlayerPrefs.SetInt("Bloom", 1);
				bloom.isOn = true;
			}
			UpdateBloom();

			// Gets the FXAA options.
			if(PlayerPrefs.HasKey("FXAA"))
				fxaa.isOn = (PlayerPrefs.GetInt("FXAA") == 1);
			else
			{
				PlayerPrefs.SetInt("FXAA", 1);
				fxaa.isOn = true;
			}
			UpdateFXAA();

		}

		// Updates the game Volume.
		public void UpdateVolume()
		{

			AudioListener.volume = volSlider.value;
			PlayerPrefs.SetFloat("Volume", volSlider.value);

		}

		// Updates the game Quality, needs restart.
		public void UpdateQuality()
		{

			// Sets the Quality.
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

			// Ensures the Quality setting has been saved.
			PlayerPrefs.Save();

			// Restarts the game.
			// - Opens a new game.
			if(Application.platform == RuntimePlatform.WindowsPlayer)
				Process.Start(Application.dataPath + "/../FirePlace.exe");
			else if(Application.platform == RuntimePlatform.LinuxPlayer)
				Process.Start(Application.dataPath + "/../FirePlace.x86_64"); 
			// - Closes the current game.
 			Application.Quit();

		}

		// Resets the Quality setting.
		public void ResetQuality()
		{
			quality.value = QualitySettings.GetQualityLevel();
		}

		// Updates the game Ambient Occlusion.
		public void UpdateAO()
		{

			EffectController.UpdateAO(ambientOcclusion.isOn);

			if(ambientOcclusion.isOn)
				PlayerPrefs.SetInt("AO", 1);
			else
				PlayerPrefs.SetInt("AO", 0);

		}

		// Updates the game Bloom.
		public void UpdateBloom()
		{

			EffectController.UpdateBloom(bloom.isOn);

			if(bloom.isOn)
				PlayerPrefs.SetInt("Bloom", 1);
			else
				PlayerPrefs.SetInt("Bloom", 0);

		}

		// Updates the game FXAA.
		public void UpdateFXAA()
		{

			AAController.UpdateAA(fxaa.isOn);

			if(fxaa.isOn)
				PlayerPrefs.SetInt("FXAA", 1);
			else
				PlayerPrefs.SetInt("FXAA", 0);

		}

		// Updates the game Exits the game.
		public void ExitGame()
		{
			Application.Quit();
		}

	}

}
