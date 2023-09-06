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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (new HashSet<GameObject>(this.TriggerCandidates).Contains(other.gameObject))
        {
            this.HeadCollisionEvent.Invoke();
        }
    }
}
