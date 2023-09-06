using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HeadTrigger : MonoBehaviour
{
    [Tooltip("Event invoked when collision occurs.")]
    public UnityEvent HeadCollisionEvent;

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
            this.HeadCollisionEvent.Invoke();
        }
    }
}
