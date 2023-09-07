using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FloatVariable TorqueForce;

    public FloatVariable SpeedNormalizationRate;

    public FloatVariable MinSpeed;

    public FloatVariable MaxSpeed;

    public FloatVariable AccelerationRate;

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
        this.rigidBody.AddTorque(this.inputHorizontal * this.TorqueForce.Value);

        // User-input induced speed change
        if (this.inputVertical != 0)
        {
            this.updateSpeed();
        }

        // Speed normalization
        else if (!Mathf.Approximately(this.surfaceEffector2D.speed, this.DefaultSpeed.Value))
        {
            normalizeSpeed(this.SpeedNormalizationRate.Value);
        }
    }

    // TODO: Document and break-down into smaller pieces.
    // Input-driven speedup/slowdown
    private void updateSpeed()
    {
        float oldSpeed = this.surfaceEffector2D.speed;

        this.surfaceEffector2D.speed = Mathf.Min(
            Mathf.Max(
                this.MinSpeed.Value
                , this.surfaceEffector2D.speed + (this.inputVertical * Time.deltaTime * this.AccelerationRate.Value)
            )
            , this.MaxSpeed.Value
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
