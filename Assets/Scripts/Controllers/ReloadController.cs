using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadController : MonoBehaviour
{
    public float crashReloadDelay;

    public float finishReloadDelay;

    public void OnCrashReloadScene()
    {
        Invoke("reloadScene", this.crashReloadDelay);
    }

    public void OnFinishReloadScene()
    {
        Invoke("reloadScene", this.finishReloadDelay);
    }

    private void reloadScene()
    {
        SceneManager.LoadScene("Scene0");
    }
}

