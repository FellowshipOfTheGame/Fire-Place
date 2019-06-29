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
	public string noteText;
	//public Text test;

	private bool showing = false;
	public bool isShowing() { return showing; }
	private bool inRange = false;
	public GameObject player;

	void Start()
	{
		//noteOnCanvas.SetActive(true);
		noteOnCanvas = GameObject.FindGameObjectWithTag("noteOnCanvas");
		player = GameObject.FindGameObjectWithTag("Player");
		MainCamera = Camera.main.gameObject;

		noteOnCanvas.GetComponentInChildren<Image>().enabled = false;
		noteOnCanvas.GetComponentInChildren<Text>().enabled = false;

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
					player.GetComponent<PlayerBehaviour>().setState(PlayerBehaviour.States.Lendo);

					noteOnCanvas.GetComponentInChildren<Image>().sprite = noteImage;
					noteOnCanvas.GetComponentInChildren<Text>().text = noteText;

					noteOnCanvas.GetComponentInChildren<Image>().enabled = true;
					noteOnCanvas.GetComponentInChildren<Text>().enabled = true;


				}
			}
			else {
				if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Use"))
				{
					//Debug.Log("Sai");
					showing = false;
					player.GetComponent<PlayerBehaviour>().setState(PlayerBehaviour.States.Default);

					noteOnCanvas.GetComponentInChildren<Image>().enabled = false;
					noteOnCanvas.GetComponentInChildren<Text>().enabled = false;
				}
			}
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
