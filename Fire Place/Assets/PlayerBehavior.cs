using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	public float acceleration = 2f;
	public float maxVelocity = 8.5f;
	public float gravityScale = 1;
	public float iceDamage = 1;

	private bool takingDamage = true;
	public bool isTakingDamage() { return takingDamage; }
	public void setTakingDamage(bool val) { takingDamage = val; }

	private float health = 100;
	public float getHealth() { return health; }

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
				rgbd.AddForce(new Vector3(-1, 0, 0) * acceleration, ForceMode.Force);           //o eixo de input

			if (Input.GetKey(KeyCode.RightArrow))
				rgbd.AddForce(new Vector3(1, 0, 0) * acceleration, ForceMode.Force);

			if (Input.GetKey(KeyCode.UpArrow))
				rgbd.AddForce(new Vector3(0, 0, 1) * acceleration, ForceMode.Force);

			if (Input.GetKey(KeyCode.DownArrow))
				rgbd.AddForce(new Vector3(0, 0, -1) * acceleration, ForceMode.Force);
		}

		//ROTATION----------------------------------------------------------------------------------
		/*float yAngle = Vector3.Angle(new Vector3(rgbd.velocity.x, 0, 0), new Vector3(0, 0, rgbd.velocity.z));

		transform.eulerAngles = new Vector3(0, yAngle, 0);*/



		//ICE DAMAGE-------------------------------------------------------------------------------

		if (takingDamage)
		{
			if (health > 40)
				health -= 3 * iceDamage / 100;
			else
				health -= iceDamage / 100;

		}
		else if (health < 100)
			health += 5 * iceDamage / 100;

		if (health <= 0)
			Debug.Log("Dead");


		if (Input.GetKey(KeyCode.R))
		{

			transform.position = new Vector3(42, 9, 22);
		}

		Debug.Log("Health = " + health);
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
}
