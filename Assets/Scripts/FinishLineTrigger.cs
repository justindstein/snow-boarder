using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float reloadDelay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("FinishLineTrigger.OnTriggerEnter2D: " + other.gameObject.name);

        // Player collision with finish line
        if (other.gameObject.Equals(this.player))
        {
            Invoke("reloadScene", this.reloadDelay);
        }
    }

    private void reloadScene()
    {
        SceneManager.LoadScene("Scene0");
    }
}
