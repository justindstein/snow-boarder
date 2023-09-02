using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TODO: jump on spacebar release, if space bar is held down for longer, then increase height of jump slightly

    [SerializeField] private float reloadDelay;
    [SerializeField] private ParticleSystem jumpEffects;

    [SerializeField] private float torqueForce; // -5
    [SerializeField] private float jumpForce; // 7.5

    private Rigidbody2D rigidBody;
    private SurfaceEffector2D surfaceEffector2D;

    private float inputVertical;
    private float inputHorizontal;
    private bool inputJump;

    private static float MIN_SPEED = 3f;
    private static float MAX_SPEED = 25f;
    private static float ACCELERATION_RATE = 10f;

    private static float DEFAULT_SPEED;
    private static float EXPONENTIAL_SPEED_NORMALIZATION_RATE = 10f;

    void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.surfaceEffector2D = GameObject.FindGameObjectWithTag("Ground").GetComponent<SurfaceEffector2D>();
        PlayerController.DEFAULT_SPEED = this.surfaceEffector2D.speed;
    }

    void Update()
    {
        this.inputVertical = Input.GetAxisRaw("Vertical");
        this.inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) this.inputJump = true;
    }

    void FixedUpdate()
    {
        //Debug.Log(string.Format("[Horizontal: {0}] [Vertical: {1}] [isJump {2}] ", this.inputHorizontal, this.inputVertical, this.inputJump));

        // Player rotation
        //this.transform.Rotate(new Vector3(0, 0, (this.inputHorizontal * this.rotateVelocity)), Space.Self);
        this.rigidBody.AddTorque(this.inputHorizontal * this.torqueForce);

        // TODO: Jump only if player is on ground
        // Player jump
        if (this.inputJump)
        {
            this.jump();
        }

        // Speed change
        if (this.inputVertical != 0)
        {
            this.updateSpeed();
        }
        else if (!Mathf.Approximately(this.surfaceEffector2D.speed, PlayerController.DEFAULT_SPEED))
        {
            normalizeSpeed();
        }

    }

    // TODO: Document
    void jump()
    {
        this.rigidBody.velocity = new Vector2(this.rigidBody.velocity.x, (Vector2.up.y * this.jumpForce));
        this.inputJump = false;
        this.jumpEffects.Play();
    }

    // TODO: Document and break-down into smaller pieces.
    // Input-driven speedup/slowdown
    void updateSpeed()
    {
        float oldSpeed = this.surfaceEffector2D.speed;

        this.surfaceEffector2D.speed = Mathf.Min(
            Mathf.Max(
                PlayerController.MIN_SPEED
                , this.surfaceEffector2D.speed + (this.inputVertical * Time.deltaTime * PlayerController.ACCELERATION_RATE)
            )
            , PlayerController.MAX_SPEED
        );

        Debug.Log(string.Format("PlayerController.updateSpeed user input speed change [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
    }

    // TODO: Document and break-down into smaller pieces.
    // Player speed normalized back to DEFAULT_SPEED in the absence of user input
    void normalizeSpeed()
    {
        float oldSpeed = this.surfaceEffector2D.speed;
        
        float speedDifferential = (this.surfaceEffector2D.speed - PlayerController.DEFAULT_SPEED) / PlayerController.EXPONENTIAL_SPEED_NORMALIZATION_RATE;
        this.surfaceEffector2D.speed = Mathf.Approximately(PlayerController.DEFAULT_SPEED, this.surfaceEffector2D.speed - speedDifferential)
            ? PlayerController.DEFAULT_SPEED
            : this.surfaceEffector2D.speed - speedDifferential
        ;

        Debug.Log(string.Format("PlayerController.normalizeSpeed [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
    }
}
