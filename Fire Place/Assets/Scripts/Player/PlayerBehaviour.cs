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

	public Transform cameraFollowPoint = null;

	private bool gotDir = false;
	private Vector3 cameraForward;
	private Vector3 cameraRight;

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

				if(!gotDir && (horAxis != 0 || verAxis != 0)) {

					cameraRight = mainCamera.right;
					cameraForward = mainCamera.forward;

					gotDir = true;

				}

				if(horAxis == 0 && verAxis == 0) gotDir = false;

				Vector3 dir = ((cameraRight * horAxis) + (cameraForward * verAxis)).normalized;

				rgbd.velocity = new Vector3(dir.x  * velocity, yVel, dir.z * velocity);

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