using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MenuPrincipalScript : MonoBehaviour
{
	public Material BlackPlaneMaterial;

	public Image btnJogar, btnContinuar, btnOpcoes, btnSair;
	public Text textJogar, textContinuar, textOpcoes, textSair;
	public Image logo;

	public float fadeSpeed = 0.01f;

	private bool fadding = false;

	private Color curButtonColor, curTextColor, curColorWhite, curColorBlack;

	public string firstLevel = "Lv1";

	[SerializeField] private CinemachineVirtualCamera menuCamera = null;
    [SerializeField] private string startVirtualCameraName = "GameStartVCamera";

	public float switchCameraDelay = 10f;

	private float startTime;

    // Start is called before the first frame update
    void Start()
    {
		BlackPlaneMaterial.SetColor("_BaseColor", Color.black);

		curButtonColor = btnJogar.GetComponent<Image>().color;
		curTextColor = textJogar.GetComponent<Text>().color;

		curColorBlack = Color.black;
		curColorWhite = Color.white;
	}

	public void StartNewGame()
	{

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
		startTime = Time.time;

		// Enables the Player.
		GameObject player = GameObject.FindWithTag("Player");
		player.GetComponent<PlayerBehaviour>().enabled = true;
		player.GetComponent<Rigidbody>().useGravity = true;
		
		// Start switching camera.
		StartCoroutine(SwitchCamera());

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

		// Switch cameras.
        menuCamera.gameObject.SetActive(false);
        GameObject.Find(startVirtualCameraName).GetComponent<CinemachineVirtualCamera>().enabled = true;

    }

	// Fades between the menu screen and the level.
	private IEnumerator Fading()
	{

		// Fades the colors.
		while(curColorWhite.a > 0.01f)
		{

			// Calculates the delta time.
			float t = (Time.time - startTime) * fadeSpeed;

			// Increases the color fade.

			BlackPlaneMaterial.SetColor("_BaseColor", curColorBlack);

			curButtonColor = Color.Lerp(curButtonColor, Color.clear, t);
			curTextColor = Color.Lerp(curTextColor, Color.clear, t);

			curColorBlack = Color.Lerp(curColorBlack, Color.clear, t);
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

	}

	public void QuitGame()
	{

		Debug.Log("Application.Quit();");
		Application.Quit();

	}


}
