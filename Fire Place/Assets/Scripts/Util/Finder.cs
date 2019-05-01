using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fireplace.Util
{
    public class Finder
    {
        public static GameObject FindRootObject(string sceneName, string tag)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.IsValid())
            {
                GameObject[] rootObjects = scene.GetRootGameObjects();
                foreach (GameObject root in rootObjects)
                {
                    if (root.CompareTag(tag))
                    {
                        return root;
                    }
                }
                Debug.LogWarning($"{nameof(Finder)}: Couldn't find object with tag {tag} in scene {sceneName}");
            }
            else
            {
                Debug.LogWarning($"{nameof(Finder)}: Couldn't find scene with name {sceneName}");
            }
            return null;
        }
    } 
}