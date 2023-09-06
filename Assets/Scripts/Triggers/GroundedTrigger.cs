using UnityEngine;
using UnityEngine.Events;

public class GroundedTrigger : MonoBehaviour
{
    [Tooltip("Event invoked when player comes in contact with ground.")]
    public UnityEvent PlayerGroundedEvent;

    [Tooltip("Event invoked when player comes out of contact with ground.")]
    public UnityEvent PlayerAirbornEvent;

    [Tooltip("GameObjects to interact with.")]
    public GameObject[] TriggerCandidates;

    // TODO: move HashSet instantiation to Awake
    // TODO: add Collider2D other args
    private void OnTriggerEnter2D()
    {
        this.PlayerGroundedEvent.Invoke();
    }

    private void OnTriggerExit2D()
    {
        this.PlayerAirbornEvent.Invoke();
    }
}
