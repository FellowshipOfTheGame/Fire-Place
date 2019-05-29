using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	public enum Tipo { Clique, Colisao };

	public Tipo tipo = Tipo.Clique;

	public bool addictive = true;

	public string sceneName = "";

	private string path;

	// Start is called before the first frame update
	void Start()
    {
		path = "Assets/Scenes/" + sceneName + ".unity";
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		Scene[] activeScenes;

		bool sceneIsActive = false;

		if (tipo == Tipo.Colisao)
		{
			if (other.gameObject.tag == "Player") {

				activeScenes = SceneManager.GetAllScenes();

				for(int i = 0; i < activeScenes.Length; i++)
				{
					if (activeScenes[i].name == sceneName)
						sceneIsActive = true;
				}

				if (!sceneIsActive)
				{
					Debug.Log("Entra");
					SceneManager.LoadScene(path, LoadSceneMode.Additive);
				}

			}
		}	
	}
}
