using UnityEngine;

public class GroundedTrigger : MonoBehaviour
{
    [Header("Events")]
    public GameEvent PlayerGrounded;
    public GameEvent PlayerAirborn;

    private void OnTriggerEnter2D()
    {
        this.PlayerGrounded.Raise(this, null);
    }

    private void OnTriggerExit2D()
    {
        this.PlayerAirborn.Raise(this, null);
    }
}
