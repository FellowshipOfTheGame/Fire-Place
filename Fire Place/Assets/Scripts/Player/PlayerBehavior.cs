using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
	public enum States { Default, Lendo, Fogueira };

	private States state;
	public void setState(States state) { this.state = state; }
	public States getState() { return state; }
	[SerializeField] private Animator anim = null;

	public float acceleration = 2f;
	public float maxVelocity = 8.5f;
	public float gravityScale = 1;

	private float extraGravity = 0;

	private Transform mainCamera;

	private Rigidbody rgbd;

	//public Vector3 dest;
	//public float facingY;
	public float tolDistToDest = 0.2f, tolAngulo = 1, turningSpeed = 0.5f;

	private bool controllable = true;
	public void setControllable(bool val) { controllable = val; }
	public bool getControllable() { return controllable; }

	//public float db;
	//public float angle;

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
					
					if (horAxis != 0 && controllable)                                                     //Adiciona força de acordo com
						rgbd.AddForce(mainCamera.right * Mathf.Sign(horAxis) * acceleration, ForceMode.Force);           //o eixo de input

					if (verAxis != 0 && controllable)
						rgbd.AddForce(mainCamera.forward * Mathf.Sign(verAxis) * acceleration, ForceMode.Force);
				}

				if(Vector3.Magnitude(new Vector2(rgbd.velocity.x,rgbd.velocity.z)) >= 0.5f)
				{
					anim.SetBool("isWalking", true);
				}
				else
				{
					anim.SetBool("isWalking", false);
				}


				break;

			case States.Fogueira:

				if (Input.anyKeyDown)
				{
					//ANIMAÇÃO DE LEVANTAR
					state = States.Default;
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
			//Debug.Log("MAGNITUDE = " + (Vector3.Magnitude(rgbd.velocity)));
			if (rgbd.velocity.z > 0)
			{
				transform.eulerAngles = new Vector3(0, -yAngle, 0);
				//Debug.Log("Transform angle");
			}
			else
			{
				transform.eulerAngles = new Vector3(0, yAngle, 0);
				//Debug.Log("Transform angle");
			}
		}

	}

	public void Sit(Vector3 keyPos, float facingY)
	{
		if (state != States.Fogueira)
			StartCoroutine(WaitGetToPosition(keyPos, facingY));
	}

	private IEnumerator WaitGetToPosition(Vector3 keyPos, float facingY)
	{
		Vector3 distance = keyPos - transform.position;				//calcula o vetor distancia
		distance = distance.normalized;                             //normaliza
		distance = new Vector3(distance.x, 0, distance.z);			//projeta no plano

		while ((transform.position - keyPos).magnitude > tolDistToDest)				//enquanto a distancia até o destino for maior que a tolerancia
		{
			if (Vector3.Magnitude(rgbd.velocity) <= maxVelocity)			//aplica uma força no player para andar
			{
				rgbd.AddForce(distance * acceleration, ForceMode.Force);
			}
			yield return null;
		}
		

		if (facingY > transform.eulerAngles.y)								//ajeita o angulo
		{
			if (facingY - transform.eulerAngles.y > 180)
			{
				while (Mathf.Abs(transform.eulerAngles.y - facingY) > tolAngulo)
				{
					transform.eulerAngles -= new Vector3(0, turningSpeed, 0);
					yield return null;
				}
			}
			else
			{
				while (Mathf.Abs(transform.eulerAngles.y - facingY) > tolAngulo)
				{
					transform.eulerAngles += new Vector3(0, turningSpeed, 0);
					yield return null;
				}
			}
		}
		else
		{
			if (transform.eulerAngles.y - facingY > 180)
			{
				while (Mathf.Abs(transform.eulerAngles.y - facingY) > tolAngulo)
				{
					transform.eulerAngles += new Vector3(0, turningSpeed, 0);
					yield return null;
				}
			}
			else
			{
				while (Mathf.Abs(transform.eulerAngles.y - facingY) > tolAngulo)
				{
					transform.eulerAngles -= new Vector3(0, turningSpeed, 0);
					yield return null;
				}
			}
		}

		//set animation to sitted   GetComponentInChildren<Animator>().
		state = States.Fogueira;
	}
}
