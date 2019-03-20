using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	public float movementForce = 2f;

	private Rigidbody rgbd;

    // Start is called before the first frame update
    void Start()
    {
		rgbd = GetComponent<Rigidbody>();
        
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//rgbd.velocity = Vector3.zero;
			rgbd.AddRelativeForce(new Vector3(-1, 0, 0) * movementForce, ForceMode.Force);
			Debug.Log("Get it");
		}

		else if (Input.GetKey(KeyCode.RightArrow))
		{
			//rgbd.velocity = Vector3.zero;
			rgbd.AddRelativeForce(new Vector3(1, 0, 0) * movementForce, ForceMode.Force);
		}

		else if (Input.GetKey(KeyCode.UpArrow))
		{
			//rgbd.velocity = Vector3.zero;
			rgbd.AddRelativeForce(new Vector3(0, 0, 1) * movementForce, ForceMode.Force);
		}

		else if (Input.GetKey(KeyCode.DownArrow))
		{
			//rgbd.velocity = Vector3.zero;
			rgbd.AddRelativeForce(new Vector3(0, 0, -1) * movementForce, ForceMode.Force);
		}
	}
}
