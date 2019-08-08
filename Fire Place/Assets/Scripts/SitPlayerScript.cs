using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitPlayerScript : MonoBehaviour
{
	private GameObject player;

	public GameObject letterE;

	public Vector3 dest;
	public float facingY;

	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");

		dest = new Vector3(0.5f, 0, 0.5f);
		dest = transform.TransformPoint(dest);

		facingY = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerStay(Collider collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			if (Input.GetKeyUp(KeyCode.E))
			{
				player.GetComponent<PlayerBehaviour>().Sit(dest, facingY);
				letterE.GetComponent<MeshRenderer>().enabled = false;
			}
		}	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			letterE.GetComponent<MeshRenderer>().enabled = true;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
			letterE.GetComponent<MeshRenderer>().enabled = false;
	}
}
