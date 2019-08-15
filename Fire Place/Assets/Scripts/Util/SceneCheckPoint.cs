using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheckPoint : MonoBehaviour
{
	public enum Action { Load, Unload };
	//public enum Tipo { Clique, Colisao };

	//public Tipo tipo = Tipo.Clique;
	public Action action = Action.Load;

	public bool addictive = true;

	public string sceneName = "";

	private bool after = false;

	void Start ()
	{

		after = false;

	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{	

			if (action == Action.Load) // If the scene has already been loaded unloads the scene.
			{ 

				if(after)
					SceneManager.UnloadSceneAsync(sceneName);
				else
				{
					if (!addictive)
						SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
					else
						SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
				}

				after = !after;

			}
			else if(action == Action.Unload)
			{
				if(after) // If the scene has already been unloaded loads back the scene.
				{
					if(!addictive)
						SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
					else
						SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
				} else
					SceneManager.UnloadSceneAsync(sceneName);

				after = !after;

			}
		}
	}
}
