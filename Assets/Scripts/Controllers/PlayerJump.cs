using UnityEngine;
using UnityEngine.Events;

public class PlayerJump : MonoBehaviour
{
    public UnityEvent PlayerJumpEvent;

    public float jumpForce; // 7.5

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
        // TODO: Jump only if player is on ground
        // TODO: jump on spacebar release, if space bar is held down for longer, then increase height of jump slightly
        // Player jump
        if (this.inputJump)
        {
            this.Jump(this.jumpForce);
            this.PlayerJumpEvent.Invoke();
        }
    }

    // TODO: Document
    public void Jump(float force)
    {
        this.rigidBody.velocity = new Vector2(this.rigidBody.velocity.x, (Vector2.up.y * force));
        this.inputJump = false;
    }
}
