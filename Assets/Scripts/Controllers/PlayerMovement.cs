using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed at which player rotates.")]
    public float TorqueForce;

    [Tooltip("Rate at which speed normlizes to default speed when no user input is provided.")]
    public float SpeedNormalizationRate;

    [Tooltip("Minimum player speed.")]
    public float MinimumSpeed;

    [Tooltip("Maximum player speed.")]
    public float MaximumSpeed;

    [Tooltip("Rate at which user input speeds up slows down player.")]
    public float AccelerationRate;

    public FloatVariable DefaultSpeed;

    private Rigidbody2D rigidBody;
    private SurfaceEffector2D surfaceEffector2D;

    private float inputVertical;
    private float inputHorizontal;

    private void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.surfaceEffector2D = GameObject.FindGameObjectWithTag("Ground").GetComponent<SurfaceEffector2D>();
    }

    private void Update()
    {
        this.inputVertical = Input.GetAxisRaw("Vertical");
        this.inputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // Player rotation
        this.rigidBody.AddTorque(this.inputHorizontal * this.TorqueForce);

        // User-input induced speed change
        if (this.inputVertical != 0)
        {
            this.updateSpeed();
        }

        // Speed normalization
        else if (!Mathf.Approximately(this.surfaceEffector2D.speed, this.DefaultSpeed.Value))
        {
            normalizeSpeed(this.SpeedNormalizationRate);
        }
    }

    // TODO: Document and break-down into smaller pieces.
    // Input-driven speedup/slowdown
    private void updateSpeed()
    {
        float oldSpeed = this.surfaceEffector2D.speed;

        this.surfaceEffector2D.speed = Mathf.Min(
            Mathf.Max(
                this.MinimumSpeed
                , this.surfaceEffector2D.speed + (this.inputVertical * Time.deltaTime * this.AccelerationRate)
            )
            , this.MaximumSpeed
        );

        Debug.Log(string.Format("PlayerController.updateSpeed user input speed change [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
    }

    // TODO: add defaultSpeed as a system variable

    // TODO: Document and break-down into smaller pieces.
    // Player speed normalized back to DEFAULT_SPEED in the absence of user input
    private void normalizeSpeed(float speedNormalizationRate)
    {
        float oldSpeed = this.surfaceEffector2D.speed;

        float speedDifferential = (this.surfaceEffector2D.speed - this.DefaultSpeed.Value) / speedNormalizationRate;
        this.surfaceEffector2D.speed = Mathf.Approximately(this.DefaultSpeed.Value, this.surfaceEffector2D.speed - speedDifferential)
            ? this.DefaultSpeed.Value
            : this.surfaceEffector2D.speed - speedDifferential
        ;

        Debug.Log(string.Format("PlayerController.normalizeSpeed [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
    }
}
