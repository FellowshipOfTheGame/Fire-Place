using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	public enum States { Default, Lendo, Fogueira };

	private States state;
	public void setState(States state) { this.state = state; }
	public States getState() { return state; }
	[SerializeField] private Animator anim;

	public float acceleration = 2f;
	public float maxVelocity = 8.5f;
	public float gravityScale = 1;

	private float extraGravity = 0;

	private Transform mainCamera;

	private Rigidbody rgbd;

    // Start is called before the first frame update
    void Start()
    {

		mainCamera = Camera.main.transform;

		extraGravity = gravityScale - 1;
		rgbd = GetComponent<Rigidbody>();

		state = States.Default;
    }

	// Update is called once per frame
	void FixedUpdate()
	{
		switch (state)
		{
			case States.Default:

				//MOVEMENT INPUT---------------------------------------------------------------------------
				if (Vector3.Magnitude(rgbd.velocity) <= maxVelocity)                                //se a velocidade não é máxima
				{
					
					float horAxis = Input.GetAxis("Horizontal");
					float verAxis = Input.GetAxis("Vertical");

					
					if (horAxis != 0)                                                     //Adiciona força de acordo com
						rgbd.AddForce(mainCamera.right * Mathf.Sign(horAxis) * acceleration, ForceMode.Force);           //o eixo de input

					if (verAxis != 0)
						rgbd.AddForce(mainCamera.forward * Mathf.Sign(verAxis) * acceleration, ForceMode.Force);
				}

				if(Vector3.Magnitude(new Vector3(rgbd.velocity.x, 0,rgbd.velocity.z)) >= 0.5f)
				{
					anim.SetBool("isWalking", true);
				}
				else
				{
					anim.SetBool("isWalking", false);
				}


				break;
		}

		
		//GRAVITY SCALE----------------------------------------------------------------------------
		extraGravity = gravityScale - 1;							//adiciona gravidade extra
		rgbd.AddForce(Physics.gravity * extraGravity);


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
