using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	public float acceleration = 2f;
	public float maxVelocity = 8.5f;
	public float gravityScale = 1;

	private float extraGravity = 0;

	private Rigidbody rgbd;

    // Start is called before the first frame update
    void Start()
    {
		extraGravity = gravityScale - 1;
		rgbd = GetComponent<Rigidbody>();        
    }

	// Update is called once per frame
	void Update()
	{
		//GRAVITY SCALE----------------------------------------------------------------------------
		extraGravity = gravityScale - 1;							//adiciona gravidade extra
		rgbd.AddForce(Physics.gravity * extraGravity);

		//MOVEMENT INPUT---------------------------------------------------------------------------
		if (Vector3.Magnitude(rgbd.velocity) <= maxVelocity)                                //se a velocidade não é máxima
		{
			if (Input.GetKey(KeyCode.LeftArrow))                                                     //Adiciona força de acordo com
				rgbd.AddForce(new Vector3(1, 0, 0) * acceleration, ForceMode.Force);           //o eixo de input

			if (Input.GetKey(KeyCode.RightArrow))
				rgbd.AddForce(new Vector3(-1, 0, 0) * acceleration, ForceMode.Force);

			if (Input.GetKey(KeyCode.UpArrow))
				rgbd.AddForce(new Vector3(0, 0, -1) * acceleration, ForceMode.Force);

			if (Input.GetKey(KeyCode.DownArrow))
				rgbd.AddForce(new Vector3(0, 0, 1) * acceleration, ForceMode.Force);
		}

		//ROTATION----------------------------------------------------------------------------------
		Vector3 projection = new Vector3(rgbd.velocity.x, 0, rgbd.velocity.z);
		float yAngle = Vector3.Angle(projection, new Vector3(1, 0, 0));

		if (Vector3.Magnitude(rgbd.velocity) > 1)
		{
			if (rgbd.velocity.z > 0)
				transform.eulerAngles = new Vector3(0, -yAngle, 0);
			else
				transform.eulerAngles = new Vector3(0, yAngle, 0);
		}

		if (Input.GetKey(KeyCode.R))
		{

			transform.position = new Vector3(42, 9, 22);
		}

	}
}
