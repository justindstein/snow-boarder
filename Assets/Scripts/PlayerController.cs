using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private float inputVertical;
    private float inputHorizontal;
    private bool inputJump;

    //[SerializeField] private float rotateVelocity; // -2
    [SerializeField] private float torqueForce; // -5
    [SerializeField] private float jumpForce; // 7.5

    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.inputVertical = Input.GetAxisRaw("Vertical");
        this.inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) this.inputJump = true;
    }

    private void FixedUpdate()
    {
        Debug.Log(string.Format("[Horizontal: {0}] [Vertical: {1}] [isJump {2}] ", this.inputHorizontal, this.inputVertical, this.inputJump));

        // Player rotation
        //this.transform.Rotate(new Vector3(0, 0, (this.inputHorizontal * this.rotateVelocity)), Space.Self);
        this.rigidBody.AddTorque(this.inputHorizontal * this.torqueForce);

        // TODO: Jump only if player is on ground
        // Player jump
        if (this.inputJump)
        {
            Debug.Log("Jumping");
            this.rigidBody.velocity = new Vector2(this.rigidBody.velocity.x, (Vector2.up.y * this.jumpForce));
            this.inputJump = false;
        }
    }
}
