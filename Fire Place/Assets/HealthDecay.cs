using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDecay : MonoBehaviour
{
	public GameObject player;

	public float alpha = 0;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void Update()
    {
		alpha = (1 - player.GetComponent<PlayerBehavior>().getHealth()/100) * 0.6f;

		GetComponent<Image>().color = new Color(0, 0, 0, alpha);
    }
}
