using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    [Header("Events")]
    public GameEvent HeadCollision;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HeadTrigger.OnTriggerEnter2D");
        this.HeadCollision.Raise(this, other);
    }
}
