using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheckPoint : MonoBehaviour
{
	public enum Action { Load, Unload };
	//public enum Tipo { Clique, Colisao };

	//public Tipo tipo = Tipo.Clique;
	public Action acao = Action.Load;

	public bool addictive = true;

	public string sceneName = "";

	private string path;

	// Start is called before the first frame update
	void Start()
	{
		path = "Assets/Scenes/" + sceneName + ".unity";
	}

	void OnTriggerEnter(Collider other)
	{
		Scene[] activeScenes;

		GameObject lights = GameObject.Find("Directional Light");

		bool sceneIsActive = false;

		if (other.gameObject.tag == "Player")
		{

			activeScenes = SceneManager.GetAllScenes();

			for (int i = 0; i < activeScenes.Length; i++)
			{
				if (activeScenes[i].name == sceneName)
					sceneIsActive = true;
			}

			if (acao == Action.Load) {

				if (!addictive)
				{
//					lights.SetActive(false);
					SceneManager.LoadScene(path, LoadSceneMode.Single);
					lights.SetActive(false);
				}

				else if (!sceneIsActive)
				{
					lights.SetActive(false);
					SceneManager.LoadScene(path, LoadSceneMode.Additive);
				}
			}

			else if(acao == Action.Unload)
			{
				if (sceneIsActive)
				{
					//Debug.Log("Entra");
					SceneManager.UnloadScene(path);
					//SceneManager.LoadScene(path, LoadSceneMode.Additive);
				}

			}
		}
	}
}
