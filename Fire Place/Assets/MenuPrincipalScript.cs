using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipalScript : MonoBehaviour
{
	public Material BlackPlaneMaterial;
	public GameObject blackPanel, textJogar, assistCamera;
	public GameObject mainCamera;
	public float fadeSpeed = 1f;
	private bool fadding = false;
	private Color curColorBlack, curColorWhite;

	public string playerSceneName = "Player";

	float startTime;

    // Start is called before the first frame update
    void Start()
    {
		BlackPlaneMaterial.SetColor("_BaseColor", Color.black);

		curColorBlack = Color.black;
		curColorWhite = Color.white;

		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

		assistCamera.transform.position = mainCamera.transform.position;
		assistCamera.transform.rotation = mainCamera.transform.rotation;
	}

    // Update is called once per frame
    void Update()
    {
		assistCamera.transform.position = mainCamera.transform.position;
		assistCamera.transform.rotation = mainCamera.transform.rotation;

		float t;
		if (fadding)
		{
			t = (Time.time - startTime) * fadeSpeed;
			curColorBlack = Color.Lerp(curColorBlack, Color.clear, t);
			curColorWhite = Color.Lerp(curColorWhite, Color.clear, t);

			BlackPlaneMaterial.SetColor("_BaseColor", curColorBlack);
			textJogar.GetComponent<Text>().color = curColorWhite;

			if (curColorWhite.a < 0.1)
			{
				blackPanel.SetActive(false);
				textJogar.SetActive(false);
			}
		}

		if (Input.anyKeyDown)
		{
			fadding = true;
			startTime = Time.time;
		}
    }
}
