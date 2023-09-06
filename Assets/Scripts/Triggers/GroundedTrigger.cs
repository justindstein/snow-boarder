using UnityEngine;
using UnityEngine.Events;

public class GroundedTrigger : MonoBehaviour
{
    public UnityEvent PlayerGroundedEvent;
    public UnityEvent PlayerAirbornEvent;

    private void OnTriggerEnter2D()
    {
        Debug.Log("OnTriggerEnter2D");
        this.PlayerGroundedEvent.Invoke();
    }

    private void OnTriggerExit2D()
    {
        Debug.Log("OnTriggerExit2D");
        this.PlayerAirbornEvent.Invoke();
    }
}
