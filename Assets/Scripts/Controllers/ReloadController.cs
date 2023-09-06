using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadController : MonoBehaviour
{
    [Header("Crash Effects")]
    [SerializeField] private ParticleSystem crashEffects;
    [SerializeField] private float crashReloadDelay;

    [Header("Finish Effects")]
    [SerializeField] private ParticleSystem finishEffects;
    [SerializeField] private GameObject player;
    [SerializeField] private float finishReloadDelay;

    public void OnHeadCollision(Component sender, object data)
    {
        Debug.Log(string.Format("ReloadController.HeadCollision: [sender: {0}] [data: {1}]", sender, data));

        if (((Collider2D)data).tag == "Ground")
        {
            this.crashEffects.Play();
            Invoke("reloadScene", this.crashReloadDelay);
        }
    }

    public void OnFinishLineCrossed(Component sender, object data)
    {
        Debug.Log(string.Format("ReloadController.FinishLineCrossed: [sender: {0}] [data: {1}]", sender, data));

        // Player collision with finish line
        if (((Collider2D)data).gameObject.Equals(this.player))
        {
            finishEffects.Play();
            Invoke("reloadScene", this.finishReloadDelay);
        }
    }

    private void reloadScene()
    {
        SceneManager.LoadScene("Scene0");
    }
}

