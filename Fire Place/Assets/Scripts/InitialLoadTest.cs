using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialLoadTest : MonoBehaviour
{
    protected void Start()
    {
        SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Lv1_WIP", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Loading");
    }
}