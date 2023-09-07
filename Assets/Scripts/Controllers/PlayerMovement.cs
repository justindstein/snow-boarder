using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FloatVariable TorqueForce;

    public FloatVariable SpeedNormalizationRate;

    public FloatVariable MinSpeed;

    public FloatVariable MaxSpeed;

    public FloatVariable AccelerationRate;

    public FloatVariable DefaultSpeed;

    public BoolVariable IsAlive;

    private Rigidbody2D rigidBody;
    private SurfaceEffector2D surfaceEffector2D;

    private float inputVertical;
    private float inputHorizontal;

    private void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.surfaceEffector2D = GameObject.FindGameObjectWithTag("Ground").GetComponent<SurfaceEffector2D>();
        this.IsAlive.SetValue(true);
    }

    private void Update()
    {
        if(this.IsAlive.Value) {
            this.inputVertical = Input.GetAxisRaw("Vertical");
            this.inputHorizontal = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        // Player rotation
        this.rigidBody.AddTorque(this.inputHorizontal * this.TorqueForce.Value);

        // User-input induced speed change
        if (this.inputVertical != 0)
        {
            float oldSpeed = this.surfaceEffector2D.speed;
            this.surfaceEffector2D.speed = this.getUpdatedSpeed(this.inputVertical, this.AccelerationRate.Value, this.surfaceEffector2D.speed, this.MinSpeed.Value, this.MaxSpeed.Value);
            Debug.Log(string.Format("PlayerController.updateSpeed user input speed change [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
        }

        // Speed normalization
        else if (!this.isDefaultSpeed(this.surfaceEffector2D.speed, this.DefaultSpeed.Value))
        {
            float oldSpeed = this.surfaceEffector2D.speed;
            this.surfaceEffector2D.speed = getNormalizedSpeed(this.surfaceEffector2D.speed, this.DefaultSpeed.Value, this.SpeedNormalizationRate.Value);
            Debug.Log(string.Format("PlayerController.normalizeSpeed [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
        }
    }

    /// <summary>
    /// Calculate updated speed based on user input not to exceed maxSpeed or to fall below minSpeed.
    /// </summary>
    /// <param name="inputVertical"></param>
    /// <param name="accelerationRate"></param>
    /// <param name="currentSpeed"></param>
    /// <param name="minSpeed"></param>
    /// <param name="maxSpeed"></param>
    /// <returns></returns>
    private float getUpdatedSpeed(float inputVertical, float accelerationRate, float currentSpeed, float minSpeed, float maxSpeed)
    {
        return Mathf.Min(
            Mathf.Max(
                minSpeed
                , currentSpeed + (inputVertical * Time.deltaTime * accelerationRate)
            )
            , maxSpeed
        );
    }

    /// <summary>
    /// Determines if currentSpeed is approximately equal to defaultSpeed.
    /// </summary>
    /// <param name="currentSpeed"></param>
    /// <param name="defaultSpeed"></param>
    /// <returns>True if the two speeds are approximately equal.</returns>
    private bool isDefaultSpeed(float currentSpeed, float defaultSpeed)
    {
        return Mathf.Approximately(currentSpeed, defaultSpeed);
    }

    /// <summary>
    /// Normalize player's speed back towards defaultSpeed
    /// </summary>
    /// /// <param name="currentSpeed"></param>
    /// <param name="defaultSpeed"></param>
    /// <param name="speedNormalizationRate"></param>
    private float getNormalizedSpeed(float currentSpeed, float defaultSpeed, float speedNormalizationRate)
    {
        float speedDifferential = (currentSpeed - defaultSpeed) / speedNormalizationRate;
        return Mathf.Approximately(defaultSpeed, currentSpeed - speedDifferential)
            ? defaultSpeed
            : currentSpeed - speedDifferential
        ;
    }
}
