using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
	private Vector3 anchorPoint;

	public GameObject player;



    // Start is called before the first frame update
    void Start()
    {
		anchorPoint = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

		transform.position = player.transform.position + anchorPoint;
    }
}
