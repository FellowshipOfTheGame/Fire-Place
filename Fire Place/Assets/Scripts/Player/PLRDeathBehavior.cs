using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLRDeathBehavior : MonoBehaviour
{
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
		if (takingDamage)
		{
			if (health > 40)
				health -= 3 * iceDamage / 100;
			else
				health -= iceDamage / 100;

		}
		else if (health < 100)
			health += 10 * iceDamage / 100;

		if (health <= 0)
		{
			Debug.Log("Dead");
			transform.position = lastFirePos;
			health = 100;
		}
		

		// Debug.Log("Health = " + health);
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
