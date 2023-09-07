using UnityEngine;
using UnityEngine.Events;

public class PlayerJump : MonoBehaviour
{
    [Tooltip("Vertical force applied to player when jumping")]
    public float jumpForce;

    public IntVariable RemainingJumps;

    [Tooltip("Event invoked when player jumps.")]
    public UnityEvent PlayerJumpEvent;

    private Rigidbody2D rigidBody;

    private bool inputJump;


    private void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.inputJump = true;
        }
    }

    private void FixedUpdate()
    {
        // TODO: jump on spacebar release, if space bar is held down for longer, then increase height of jump slightly
        // Player jump
        if (this.inputJump && this.RemainingJumps.Value > 0)
        {
            this.Jump(this.jumpForce);
            this.RemainingJumps.ApplyChange(-1);
            this.PlayerJumpEvent.Invoke();
        }

        // Player has attempted to jump but they have no remaining jumps.
        else if(this.inputJump)
        {
            this.inputJump = false;
        }
    }

    private void Jump(float force)
    {
        this.rigidBody.velocity = new Vector2(this.rigidBody.velocity.x, (Vector2.up.y * force));
        this.inputJump = false;
    }
}
