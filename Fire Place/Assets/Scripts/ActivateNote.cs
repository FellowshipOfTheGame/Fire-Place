using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateNote : MonoBehaviour
{
	public MeshRenderer letterE;

	public Sprite noteImage;
	public string noteText;

	private Image noteOnCanvasImage;
	private Text noteOnCanvasText;

	private bool showing = false;
	public bool isShowing() { return showing; }

	private bool inRange = false;

	public PlayerBehaviour player;

	void Start()
	{
		//noteOnCanvas.SetActive(true);
		GameObject noteOnCanvas = GameObject.FindGameObjectWithTag("noteOnCanvas");

		noteOnCanvasImage = noteOnCanvas.GetComponentInChildren<Image>();
		noteOnCanvasText = noteOnCanvas.GetComponentInChildren<Text>();

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

		noteOnCanvasImage.enabled = false;
		noteOnCanvasText.enabled = false;

	}
	void Update()
	{

		if (inRange)
		{
			if (!showing) {

				if (Input.GetButtonDown("Use"))
				{
					//Debug.Log("Entra");
					ToogleNote(true);
				}

			} else {

				if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Use"))
				{
					//Debug.Log("Sai");
					ToogleNote(false);
				}

			}
		}

	}

	void OnTriggerEnter(Collider other)
	{

		if(other.tag == "Player")
		{
			letterE.enabled = true;
			inRange = true;
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{

			letterE.enabled = false;
			inRange = false;
		}

	}

	private void ToogleNote(bool show)
	{

		showing = show;

		if (show)
		{

			player.setState(PlayerBehaviour.States.Lendo);

			noteOnCanvasImage.sprite = noteImage;
			noteOnCanvasImage.preserveAspect = true;
			noteOnCanvasText.text = noteText;

		} else {

			player.setState(PlayerBehaviour.States.Default);

		}

		noteOnCanvasImage.enabled = show;
		noteOnCanvasText.enabled = show;

	}
}
