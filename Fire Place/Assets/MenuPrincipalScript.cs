using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipalScript : MonoBehaviour
{
	public Material BlackPlaneMaterial;

	public GameObject btnJogar, btnContinuar, textJogar, textContinuar;

	public float fadeSpeed = 1f;

	private bool fadding = false;

	private Color curColorBlack, curColorWhite, curColorGray;

	float startTime;

    // Start is called before the first frame update
    void Start()
    {
		BlackPlaneMaterial.SetColor("_BaseColor", Color.black);

		curColorBlack = Color.black;
		curColorWhite = Color.white;
		curColorGray = textJogar.GetComponent<Text>().color;
	}

    // Update is called once per frame
    void Update()
    {
		float t;
		if (fadding)
		{
			t = (Time.time - startTime) * fadeSpeed;
			curColorBlack = Color.Lerp(curColorBlack, Color.clear, t);
			curColorWhite = Color.Lerp(curColorWhite, Color.clear, t);
			curColorGray = Color.Lerp(curColorGray, Color.clear, t);

			BlackPlaneMaterial.SetColor("_BaseColor", curColorBlack);
			btnJogar.GetComponent<Image>().color = curColorWhite;
			btnContinuar.GetComponent<Image>().color = curColorWhite;
			textJogar.GetComponent<Text>().color = curColorGray;
			textContinuar.GetComponent<Text>().color = curColorGray;

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
	}


}
