using UnityEngine;
using UnityEngine.Events;

public class FinishLineTrigger : MonoBehaviour
{
    [Tooltip("Event invoked when player crosses finish line.")]
    public UnityEvent FinishLineCrossedEvent;

    [Tooltip("GameObjects to interact with.")]
    public GameObject[] TriggerCandidates;

    // TODO: move HashSet instantiation to Awake
    private void OnTriggerEnter2D(Collider2D other)
    {
        this.FinishLineCrossedEvent.Invoke();
    }
}
