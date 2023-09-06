using UnityEngine;
using UnityEngine.Events;

public class FinishLineTrigger : MonoBehaviour
{
    public UnityEvent FinishLineCrossedEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.FinishLineCrossedEvent.Invoke();
    }
}
