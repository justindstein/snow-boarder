using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Tooltip("Name of the scene you wish to load.")]
    public string SceneName;

    /// <summary>
    /// Load a scene with delay.
    /// </summary>
    /// <param name="delay"></param>
    public void LoadScene(float delay)
    {
        Invoke("LoadScene", delay);
    }

    /// <summary>
    /// Load a scene.
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene(this.SceneName);
    }
}

