using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLineTrigger : MonoBehaviour
{
    [Tooltip("Event invoked when player crosses finish line.")]
    public UnityEvent FinishLineCrossedEvent;

    [Tooltip("GameObjects to interact with.")]
    public GameObject[] TriggerCandidates;

    private HashSet<GameObject> triggerCandidates;

    private void Awake()
    {
        this.triggerCandidates = new HashSet<GameObject>(this.TriggerCandidates);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.triggerCandidates.Contains(other.gameObject))
        {
            this.FinishLineCrossedEvent.Invoke();
        }
    }
}
