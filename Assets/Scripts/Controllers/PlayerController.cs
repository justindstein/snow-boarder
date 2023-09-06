using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float reloadDelay;
    [SerializeField] private ParticleSystem jumpEffects;
    [SerializeField] private ParticleSystem groundedEffects;

    [SerializeField] private float torqueForce; // -5
    [SerializeField] private float jumpForce; // 7.5

    private static float MIN_SPEED = 3f;
    private static float MAX_SPEED = 28f;
    private static float ACCELERATION_RATE = 10f;
    private static float EXPONENTIAL_SPEED_NORMALIZATION_RATE = 10f;

    private static float defaultSpeed;
    private Rigidbody2D rigidBody;
    private SurfaceEffector2D surfaceEffector2D;

    private float inputVertical;
    private float inputHorizontal;
    private bool inputJump;

    private void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.surfaceEffector2D = GameObject.FindGameObjectWithTag("Ground").GetComponent<SurfaceEffector2D>();
        PlayerController.defaultSpeed = this.surfaceEffector2D.speed;
    }

    private void Update()
    {
        this.inputVertical = Input.GetAxisRaw("Vertical");
        this.inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) this.inputJump = true;
    }

    private void FixedUpdate()
    {
        //Debug.Log(string.Format("[Horizontal: {0}] [Vertical: {1}] [isJump {2}] ", this.inputHorizontal, this.inputVertical, this.inputJump));

        // Player rotation
        this.rigidBody.AddTorque(this.inputHorizontal * this.torqueForce);

        // TODO: Jump only if player is on ground
        // TODO: jump on spacebar release, if space bar is held down for longer, then increase height of jump slightly
        // Player jump
        if (this.inputJump)
        {
            this.jump();
        }

        // User-input induced speed change
        if (this.inputVertical != 0)
        {
            this.updateSpeed();
        }

        // Speed normalization
        else if (!Mathf.Approximately(this.surfaceEffector2D.speed, PlayerController.defaultSpeed))
        {
            normalizeSpeed();
        }

    }

    // TODO: Document
    private void jump()
    {
        this.rigidBody.velocity = new Vector2(this.rigidBody.velocity.x, (Vector2.up.y * this.jumpForce));
        this.inputJump = false;
        this.jumpEffects.Play();
    }

    // TODO: Document and break-down into smaller pieces.
    // Input-driven speedup/slowdown
    private void updateSpeed()
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
    private void normalizeSpeed()
    {
        float oldSpeed = this.surfaceEffector2D.speed;

        float speedDifferential = (this.surfaceEffector2D.speed - PlayerController.defaultSpeed) / PlayerController.EXPONENTIAL_SPEED_NORMALIZATION_RATE;
        this.surfaceEffector2D.speed = Mathf.Approximately(PlayerController.defaultSpeed, this.surfaceEffector2D.speed - speedDifferential)
            ? PlayerController.defaultSpeed
            : this.surfaceEffector2D.speed - speedDifferential
        ;

        Debug.Log(string.Format("PlayerController.normalizeSpeed [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
    }

    //public void OnPlayerGrounded(Component sender, object data)
    //{
    //    Debug.Log(string.Format("PlayerController.OnPlayerGrounded [sender: {0}] [data: {1}]!", sender, data));
    //    this.groundedEffects.Play();
    //}

    //public void OnPlayerAirborn(Component sender, object data)
    //{
    //    Debug.Log(string.Format("PlayerController.OnPlayerAirborn [sender: {0}] [data: {1}]!", sender, data));
    //    this.groundedEffects.Stop();
    //}
}