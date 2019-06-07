using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public enum States { Default, Lendo, Fogueira };

	private States state;
	public void setState(States state) { this.state = state; }
	public States getState() { return state; }
	[SerializeField] private Animator anim = null;

	public float velocity = 8.5f;
	public float gravityScale = 1;

	private Transform mainCamera;

	private Rigidbody rgbd;

    // Start is called before the first frame update
    void Start()
    {

		mainCamera = Camera.main.transform;
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
				float horAxis = Input.GetAxis("Horizontal");
				float verAxis = Input.GetAxis("Vertical");

				float yVel = rgbd.velocity.y;

				rgbd.velocity = (mainCamera.right * horAxis * velocity) 
				              + (mainCamera.forward * verAxis * velocity);

				rgbd.velocity = new Vector3(rgbd.velocity.x, yVel, rgbd.velocity.z);

				if(rgbd.useGravity) {

					rgbd.velocity += Physics.gravity * Time.deltaTime;

				}

				if(new Vector2(rgbd.velocity.x, rgbd.velocity.z).magnitude >= 0.5f)
				{
					anim.SetBool("isWalking", true);
				}
				else
				{
					anim.SetBool("isWalking", false);
				}


				break;
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