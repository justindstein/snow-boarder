using UnityEngine;
using UnityEngine.Events;

public class HeadTrigger : MonoBehaviour
{
    public UnityEvent HeadCollisionEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.HeadCollisionEvent.Invoke();
    }
}
