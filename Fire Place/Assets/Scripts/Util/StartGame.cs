using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirePlace.Util
{

    // Laods the necessary scenes to start a game.
    public class StartGame : MonoBehaviour
    {

        void Start()
        {
            
            Time.timeScale = 1;

            // Keeps this object while loading the scenes.
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(LoadScenes());

        }

        IEnumerator LoadScenes() {

            // Loads the base scenery objects and the player.
            AsyncOperation loadScenery = SceneManager.LoadSceneAsync("Scenery",  LoadSceneMode.Additive);
            AsyncOperation loadPlayer = SceneManager.LoadSceneAsync("Player",  LoadSceneMode.Additive);

            // Wait for scenes to finish loading.
            while(!loadScenery.isDone || !loadPlayer.isDone) yield return null;

            // When those scenes are loaded, loads the menu.
            AsyncOperation loadMenu = SceneManager.LoadSceneAsync("Menu",  LoadSceneMode.Additive);

            // Waits for the menu.
            while (!loadMenu.isDone) yield return null;

            // Destroy this object.
            Destroy(this.gameObject);

        }
    }

}