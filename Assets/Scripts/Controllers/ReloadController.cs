using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ReloadController : MonoBehaviour
{
    public void ReloadScene(float delay)
    {
        Invoke("reloadScene", delay);
    }

    private void reloadScene()
    {
        SceneManager.LoadScene("Scene0");
    }
}

