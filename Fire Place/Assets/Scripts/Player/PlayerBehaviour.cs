using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
	public enum States { Default, Lendo, Fogueira, Pausado };

	private States state;
	public void setState(States state) { this.state = state; }
	public States getState() { return state; }

	public float velocity = 8.5f;
	public float gravityScale = 1;

	private bool gotDir = false;
	private Vector2 previousInput = Vector2.zero;

	private Transform mainCamera;
	public Transform cameraFollowPoint = null;

	private Vector3 cameraForward;
	private Vector3 cameraRight;

	private float health = 100;
	public float getHealth() { return health; }

	public bool allowDeath = true;
	public float iceDamage = 1;

	[System.Serializable]
	public class IceEffect
	{

		public PlayerBehaviour parent;

		public Image ice = null;
		public float maxIceAlpha = 0.9f;

		public Image vignette = null;	
		public float maxVignetteAlpha = 0.6f;

		public void UpdateEffect() 
		{

			if(ice != null)
			{

				Color iceEffectColor = ice.color;
				iceEffectColor.a = (1 - parent.getHealth()/100) * maxIceAlpha;
				ice.color = iceEffectColor;

			} else
				Debug.Log("HealthDecay.FixedUpdate: No ice effect!");

			if(ice != null)
				vignette.color = new Color(0, 0, 0, (1 - parent.getHealth()/100) * maxVignetteAlpha);
			else
				Debug.Log("HealthDecay.FixedUpdate: No vignette effect!");

		}

	}
	[SerializeField] private IceEffect iceEffect = new IceEffect();

	private bool takingDamage = true;
	public bool isTakingDamage() { return takingDamage; }
	public void setTakingDamage(bool val) { takingDamage = val; }

	[SerializeField] private Animator anim = null;
	private Rigidbody rgbd;

	public GameObject pauseMenu;

	private bool controllable = true;
	public void setControllable(bool val) { controllable = val; }
	public bool getControllable() { return controllable; }

	public float tolDistToDest = 0.2f, tolAngulo = 1, turningSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

		mainCamera = Camera.main.transform;
		rgbd = GetComponent<Rigidbody>();

		state = States.Default;
    }

	void Update()
	{

		switch (state)
		{
			case States.Default:

				// PAUSE ---------------------------------------------------------------------------
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					Pause();
					return;
				}

				// COLD ----------------------------------------------------------------------------------
				if (takingDamage)
				{
					if (health > 40) // Does more damage at start.
						health -= 3 * iceDamage * Time.deltaTime;
					else if(health > 0) // Does less damage when health is ending.
						health -= iceDamage * Time.deltaTime;
					else if(health < 0) // Clamps the life if it's lesser than 0.
						health = 0;

				} else { // Restores Player health.

					if (health < 100) // Quickly restores health.
						health += 10 * iceDamage * Time.deltaTime;
					else if(health > 100) // Clamps the life if it's larger than 100.
						health = 100;

				}

				iceEffect.UpdateEffect();


				if (health == 0 && allowDeath)
					Debug.Log("Dead");

				break;

			case States.Pausado:

				// UNPAUSE ---------------------------------------------------------------------------
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					Unpause();
					return;
				}

				break;

		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		switch (state)
		{
			case States.Default:

				// MOVEMENT ---------------------------------------------------------------------------
				Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

				float yVel = rgbd.velocity.y;

				if(!gotDir) {

					cameraRight = mainCamera.right;
					cameraForward = mainCamera.forward;

					gotDir = true;

				} if(inputAxis != previousInput) gotDir = false;

				Vector3 dir = ((cameraRight * inputAxis.x) + (cameraForward * inputAxis.y)).normalized;

				rgbd.velocity = new Vector3(dir.x  * velocity, yVel, dir.z * velocity);

				if(rgbd.useGravity)
					rgbd.velocity += Physics.gravity * Time.deltaTime;

				if(new Vector2(rgbd.velocity.x, rgbd.velocity.z).magnitude >= 0.5f) anim.SetBool("isWalking", true);
				else anim.SetBool("isWalking", false);

				previousInput = inputAxis;

				// ROTATION ----------------------------------------------------------------------------------
				Vector3 projection = new Vector3(rgbd.velocity.x, 0, rgbd.velocity.z);
				float yAngle = Vector3.Angle(projection, new Vector3(1, 0, 0));

				if (Vector3.Magnitude(rgbd.velocity) > 1)
				{
					if (rgbd.velocity.z > 0)
						transform.eulerAngles = new Vector3(0, -yAngle, 0);
					else
						transform.eulerAngles = new Vector3(0, yAngle, 0);
				}

				break;

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Fire")
			takingDamage = false;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Fire")
			takingDamage = true;
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
			if (Vector3.Magnitude(rgbd.velocity) <= velocity)			//aplica uma força no player para andar
			{
				rgbd.AddForce(distance * 2, ForceMode.Force);
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

	public void Pause()
	{
		state = States.Pausado;
		Time.timeScale = 0;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		pauseMenu.SetActive(true);

	}

	public void Unpause()
	{
		state = States.Default;
		Time.timeScale = 1;

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		pauseMenu.SetActive(false);

	}

}
