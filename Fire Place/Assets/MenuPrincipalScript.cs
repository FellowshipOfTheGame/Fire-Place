using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipalScript : MonoBehaviour
{
	public Material BlackPlaneMaterial;

	public GameObject btnJogar, btnContinuar, btnOpcoes, btnSair;
	public GameObject textJogar, textContinuar, textOpcoes, textSair;
	public GameObject logo;

	public float fadeSpeed = 1f;

	private bool fadding = false;

	private Color curButtonColor, curTextColor, curColorWhite, curColorBlack;

	float startTime;

    // Start is called before the first frame update
    void Start()
    {
		BlackPlaneMaterial.SetColor("_BaseColor", Color.black);

		curButtonColor = btnJogar.GetComponent<Image>().color;
		curTextColor = textJogar.GetComponent<Text>().color;

		curColorBlack = Color.black;
		curColorWhite = Color.white;
	}

    // Update is called once per frame
    void Update()
    {
		float t;
		if (fadding)
		{
			t = (Time.time - startTime) * fadeSpeed;

			curButtonColor = Color.Lerp(curButtonColor, Color.clear, t);
			curTextColor = Color.Lerp(curTextColor, Color.clear, t);

			curColorBlack = Color.Lerp(curColorBlack, Color.clear, t);
			curColorWhite = Color.Lerp(curColorWhite, Color.clear, t);

			BlackPlaneMaterial.SetColor("_BaseColor", curColorBlack);

			btnJogar.GetComponent<Image>().color = curButtonColor;
			btnContinuar.GetComponent<Image>().color = curButtonColor;
			btnOpcoes.GetComponent<Image>().color = curButtonColor;
			btnSair.GetComponent<Image>().color = curButtonColor;

			textJogar.GetComponent<Text>().color = curTextColor;
			textContinuar.GetComponent<Text>().color = curTextColor;
			textOpcoes.GetComponent<Text>().color = curTextColor;
			textSair.GetComponent<Text>().color = curTextColor;

			logo.GetComponent<Image>().color = curColorWhite;

			if(curColorWhite.a < 0.1)
			{
				btnJogar.SetActive(false);
				btnContinuar.SetActive(false);
			}
		}
    }

	public void StartNewGame()
	{
		fadding = true;
		startTime = Time.time;
		
		GameObject player = GameObject.FindWithTag("Player");
		player.GetComponent<PlayerBehaviour>().enabled = true;

		btnJogar.GetComponent<Button>().interactable = false;
		btnContinuar.GetComponent<Button>().interactable = false;
		btnOpcoes.GetComponent<Button>().interactable = false;
		btnSair.GetComponent<Button>().interactable = false;

	}

	public void QuitGame()
	{

		Debug.Log("Application.Quit();");
		Application.Quit();

	}


}
