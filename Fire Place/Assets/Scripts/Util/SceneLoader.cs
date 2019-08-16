using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirePlace.Util
    {

    public class SceneLoader : MonoBehaviour
    {
        
        public void LoadScene(string scene) {

            SceneManager.LoadScene(scene);

        }

    }

}
