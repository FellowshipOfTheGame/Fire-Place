using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateNote : MonoBehaviour
{
	public GameObject letterE;
	public GameObject MainCamera;
	public GameObject noteOnCanvas;

	public Sprite noteImage;
	//public string noteText;
	//public Text test;

	private bool showing = false;
	public bool isShowing() { return showing; }
	private bool inRange = false, popping = false;
	public GameObject player;

	public float fadeSpeed = 10f;
	float startTime;

	public Vector3 curScale;

	void Start()
	{
		//noteOnCanvas.SetActive(true);
		noteOnCanvas = GameObject.FindGameObjectWithTag("noteOnCanvas");
		player = GameObject.FindGameObjectWithTag("Player");
		MainCamera = Camera.main.gameObject;

		noteOnCanvas.GetComponentInChildren<Image>().enabled = false;
		noteOnCanvas.GetComponentInChildren<RectTransform>().localScale = Vector3.zero;
		curScale = Vector3.zero;
		//noteOnCanvas.GetComponentInChildren<Text>().enabled = false;

	}
	void Update()
	{
		//letterE.transform.LookAt(MainCamera.transform);

		if (inRange)
		{
			if (!showing) {
				if (Input.GetKeyDown(KeyCode.E))
				{
					//Debug.Log("Entra");
					showing = true;
					popping = true;
					startTime = Time.time;
					player.GetComponent<PlayerBehavior>().setState(PlayerBehavior.States.Lendo);

					noteOnCanvas.GetComponentInChildren<Image>().sprite = noteImage;
					//noteOnCanvas.GetComponentInChildren<Text>().text = noteText;

					noteOnCanvas.GetComponentInChildren<Image>().enabled = true;
					//noteOnCanvas.GetComponentInChildren<Text>().enabled = true;


				}
			}
			else {
				if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
				{
					//Debug.Log("Sai");
					showing = false;
					popping = true;
					startTime = Time.time;
					player.GetComponent<PlayerBehavior>().setState(PlayerBehavior.States.Default);

//					noteOnCanvas.GetComponentInChildren<Image>().enabled = false;
					//noteOnCanvas.GetComponentInChildren<Text>().enabled = false;
				}
			}
		}

		float t;
		if (popping)
		{
			t = (Time.time - startTime) * fadeSpeed;

			Debug.Log("curScale = " + curScale);
			if (showing)
			{
				curScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
				if (curScale.x > 0.95f)
				{
					Debug.Log("entra");
					curScale = Vector3.one;
					popping = false;
				}
			}
			else
			{

				Debug.Log("roda");
				curScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
				if (curScale.x < 0.05f)
				{

					Debug.Log("entra (curScale = " + curScale + ")");
					curScale = Vector3.zero;
					popping = false;
					noteOnCanvas.GetComponentInChildren<Image>().enabled = false;
				}
			}

			noteOnCanvas.GetComponentInChildren<RectTransform>().localScale = curScale;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			letterE.GetComponent<MeshRenderer>().enabled = true;
			inRange = true;
		}	
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			letterE.GetComponent<MeshRenderer>().enabled = false;
			inRange = false;
		}
	}

	private void showNote(bool show)
	{

		if (show)
		{
		}
	}
}
