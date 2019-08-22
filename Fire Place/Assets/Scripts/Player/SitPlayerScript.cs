using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitPlayerScript : MonoBehaviour
{

	public Transform dest;
	public float angle = 0.0f;
	private float facingY;

	[SerializeField] private Vector2 interactIconOffset = new Vector2(48, 48);

	private bool inRange = false;

	// Start is called before the first frame update
	void Start()
    {
		facingY = transform.eulerAngles.y + angle;
    }

	void Update()
	{
		if(inRange && PlayerBehaviour.instance.getState() == PlayerBehaviour.States.Default)
		{

			PlayerBehaviour.instance.hud.UpdateIconPosition(gameObject, interactIconOffset);

			if (Input.GetButtonDown("Use"))
			{
				
				PlayerBehaviour.instance.Sit(dest.position, facingY);
				PlayerBehaviour.instance.hud.interactIcon.enabled = false;

				Destroy(transform.parent.gameObject);

			}
		}	
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
}
