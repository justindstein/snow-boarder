using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HeadTrigger : MonoBehaviour
{
    [Tooltip("Event invoked when collision occurs.")]
    public UnityEvent HeadCollisionEvent;

    [Tooltip("Collection of GameObjects that will produce a HeadCollisionEvent when collided with.")]
    public GameObject[] CollisionCandidates;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (new HashSet<GameObject>(this.CollisionCandidates).Contains(other.gameObject))
        {
            this.HeadCollisionEvent.Invoke();
        }
    }
}
