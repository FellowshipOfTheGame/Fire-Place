using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using FirePlace;

namespace FirePlace.Util
{

	public class ActivateNote : MonoBehaviour
	{

		[SerializeField] private Sprite noteImage = null;
		[SerializeField] private Vector2 interactIconOffset = new Vector2(48, 48);

		private bool showing = false;
		public bool isShowing() { return showing; }

		private bool inRange = false;

		void Update()
		{

			if (inRange)
			{

				PlayerBehaviour.instance.hud.UpdateIconPosition(gameObject, interactIconOffset);
				
				if (Input.GetButtonDown("Use"))
					if(PlayerBehaviour.instance.getState() == PlayerBehaviour.States.Default)
					{
						ToogleNote(true);
						return;
					}
				

			}

			if (showing && Input.GetButtonDown("Use") && PlayerBehaviour.instance.getState() == PlayerBehaviour.States.Lendo)
				ToogleNote(false);
			else if (showing && Input.GetButtonDown("Cancel"))
				ToogleNote(false);

		}

		void OnTriggerEnter(Collider other)
		{

			if(other.tag == "Player")
			{
				PlayerBehaviour.instance.hud.interactIcon.enabled = true;
				inRange = true;
			}

		}

		void OnTriggerExit(Collider other)
		{

			if (other.tag == "Player")
			{
				PlayerBehaviour.instance.hud.interactIcon.enabled = false;
				inRange = false;
			}

		}

		private void ToogleNote(bool show)
		{

			showing = show;

			if (show)
			{

				PlayerBehaviour.instance.setState(PlayerBehaviour.States.Lendo);

				PlayerBehaviour.instance.hud.noteImage.sprite = noteImage;
				PlayerBehaviour.instance.hud.noteImage.preserveAspect = true;
				PlayerBehaviour.instance.hud.noteSound.Play();

			} else {

				PlayerBehaviour.instance.setState(PlayerBehaviour.States.Default);

			}

			PlayerBehaviour.instance.hud.notesObject.SetActive(show);

		}
	}
}
