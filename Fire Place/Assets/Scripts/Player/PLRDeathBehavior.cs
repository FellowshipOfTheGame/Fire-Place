using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLRDeathBehavior : MonoBehaviour
{

	public bool allowDeath = true;
	public float iceDamage = 1;

	private bool takingDamage = true;
	public bool isTakingDamage() { return takingDamage; }
	public void setTakingDamage(bool val) { takingDamage = val; }

	private float health = 100;
	public float getHealth() { return health; }

	private Vector3 lastFirePos;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

		// Does cold damage to player.
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


		if (health == 0 && allowDeath)
		{
			Debug.Log("Dead");
			transform.position = lastFirePos;
			health = 100;
		}
		

		Debug.Log("Health = " + health);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Fire")
			takingDamage = false;

		lastFirePos = transform.position;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Fire")
			takingDamage = true;
	}
}
