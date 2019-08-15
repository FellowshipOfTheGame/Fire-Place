using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitPlayerScript : MonoBehaviour
{
	private PlayerBehaviour player;

	public MeshRenderer letterE;

	public Transform dest;
	public float angle = 0.0f;
	private float facingY;

	private bool inRange = false;

	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		facingY = transform.eulerAngles.y + angle;
    }

	void Update()
	{
		if(inRange)
		{
			if (Input.GetButtonDown("Use"))
			{
				player.Sit(dest.position, facingY);
				
				Destroy(transform.parent.gameObject);

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
}
