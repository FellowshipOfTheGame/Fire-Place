using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTriggerScript : MonoBehaviour
{
	public FinalCutSceneScript.State stateResponse;

	public GameObject cutsceneController;

    // Start is called before the first frame update
    void Start()
    {
		cutsceneController = GameObject.FindGameObjectWithTag("FinalCutsceneController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		if (stateResponse == FinalCutSceneScript.State.Middle)
			cutsceneController.GetComponent<FinalCutSceneScript>().SetStart();
		else
			cutsceneController.GetComponent<FinalCutSceneScript>().SetEnd();
	}
}
