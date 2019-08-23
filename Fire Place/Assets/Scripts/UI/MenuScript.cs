using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using Cinemachine;

using FirePlace;
using FirePlace.Camera;

namespace FirePlace.UI
{

	public class MenuScript : MonoBehaviour
	{
		public Material BlackPlaneMaterial;

		public EventSystem menuEventSys;
		public Image btnJogar, btnContinuar, btnOpcoes, btnSair;
		public Text textJogar, textContinuar, textOpcoes, textSair;
		public Image logo;

		public float fadeSpeed = 0.01f;

		private Color curButtonColor, curTextColor, curColorWhite, curColorBlack;

		public string firstLevel = "Lv1";

		[SerializeField] private CinemachineVirtualCamera menuCamera = null;
		[SerializeField] private string startVirtualCameraName = "GameStartVCamera";

		public float switchCameraDelay = 10f;
		public float giveControlDelay = 5f;

		private float startTime;

		// Start is called before the first frame update
		void Start()
		{

			menuEventSys.SetSelectedGameObject(btnJogar.gameObject);

			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			BlackPlaneMaterial.SetColor("_BaseColor", Color.black);

			curButtonColor = btnJogar.GetComponent<Image>().color;
			curTextColor = textJogar.GetComponent<Text>().color;

			curColorBlack = Color.black;
			curColorWhite = Color.white;
		}

		public void StartNewGame()
		{

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			// Disables the menu buttons.
			btnJogar.GetComponent<Button>().interactable = false;
			btnContinuar.GetComponent<Button>().interactable = false;
			btnOpcoes.GetComponent<Button>().interactable = false;
			btnSair.GetComponent<Button>().interactable = false;

			// Starts loading the first level.
			StartCoroutine(LoadFirstLevel());

		}
		
		private IEnumerator LoadFirstLevel() 
		{

			// Starts loading the level.
			AsyncOperation level = SceneManager.LoadSceneAsync(firstLevel,  LoadSceneMode.Additive);
			while(!level.isDone) yield return null;

			// When the level is done loading, starts fading to the scene.
			StartCoroutine(Fading());
			
			// Start switching camera.
			StartCoroutine(SwitchCamera());

		}

		// Fades between the menu screen and the level.
		private IEnumerator Fading()
		{

			startTime = Time.time;

			// Fades the colors.
			while(curColorWhite.a > 0.01f)
			{

				// Calculates the delta time.
				float t = (Time.time - startTime) * fadeSpeed;

				// Increases the color fade.
				curButtonColor = Color.Lerp(curButtonColor, Color.clear, t);
				curTextColor = Color.Lerp(curTextColor, Color.clear, t);
				curColorWhite = Color.Lerp(curColorWhite, Color.clear, t);

				btnJogar.color = curButtonColor;
				btnContinuar.color = curButtonColor;
				btnOpcoes.color = curButtonColor;
				btnSair.color = curButtonColor;

				textJogar.color = curTextColor;
				textContinuar.color = curTextColor;
				textOpcoes.color = curTextColor;
				textSair.color = curTextColor;

				logo.color = curColorWhite;

				yield return null;

			}

			// Disables the UI objects.
			btnJogar.gameObject.SetActive(false);
			btnContinuar.gameObject.SetActive(false);
			btnOpcoes.gameObject.SetActive(false);
			btnSair.gameObject.SetActive(false);

			logo.gameObject.SetActive(false);

			startTime = Time.time;
			while(curColorBlack.a > 0.01f)
			{

				// Calculates the delta time.
				float t = (Time.time - startTime) * fadeSpeed;

				// Increases the color fade.
				BlackPlaneMaterial.SetColor("_BaseColor", curColorBlack);
				curColorBlack = Color.Lerp(curColorBlack, Color.clear, t);

				yield return null;

			}
		}

		private IEnumerator SwitchCamera () 
		{

			// Waits for delay and changes the camera.
			float time = 0;

			while(time < switchCameraDelay) 
			{
				time += Time.fixedDeltaTime;
				yield return new WaitForFixedUpdate();
			}

			// Plays the stand up animation.
			PlayerBehaviour.instance.gameObject.GetComponentInChildren<Animator>().SetBool("isSeated", false);

			StartCoroutine(GivePlayerControl());

			// Switch cameras.
			menuCamera.gameObject.SetActive(false);
			CinemachineVirtualCamera startCam = GameObject.Find(startVirtualCameraName).GetComponent<CinemachineVirtualCamera>();
			CameraController.instance.SetCamera(startCam);

		}

		private IEnumerator GivePlayerControl() {

			// Waits for delay and enables the player.
			float time = 0;

			while(time < giveControlDelay) 
			{
				time += Time.fixedDeltaTime;
				yield return new WaitForFixedUpdate();
			}

			// Enables the Player.
			PlayerBehaviour.instance._collider.enabled = true;
			PlayerBehaviour.instance._rigidbody.useGravity = true;

			// Waits for a small amount of time for Physics to settle.
			time = 0;
			while(time < 0.1f) 
			{
				time += Time.fixedDeltaTime;
				yield return new WaitForFixedUpdate();
			}

			// Enables the correct event system.
			menuEventSys.enabled = false;

			PlayerBehaviour.instance.paused.eventSystem.enabled = true;

			// Enables the player.
			PlayerBehaviour.instance.setState(PlayerBehaviour.States.Default);

		}

		public void QuitGame()
		{

			Debug.Log("Application.Quit();");
			Application.Quit();

		}
	}
}
