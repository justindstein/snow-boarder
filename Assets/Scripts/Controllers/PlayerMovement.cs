using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float TorqueForce; // -5

    private Rigidbody2D rigidBody;
    private SurfaceEffector2D surfaceEffector2D;
    private float defaultSpeed;

    private float inputVertical;
    private float inputHorizontal;

    private void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.surfaceEffector2D = GameObject.FindGameObjectWithTag("Ground").GetComponent<SurfaceEffector2D>();
        this.defaultSpeed = this.surfaceEffector2D.speed;
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
        else if (!Mathf.Approximately(this.surfaceEffector2D.speed, this.defaultSpeed))
        {
            normalizeSpeed(this.EXPONENTIAL_SPEED_NORMALIZATION_RATE);
        }
    }

    private static float MIN_SPEED = 3f;
    private static float MAX_SPEED = 28f;
    private static float ACCELERATION_RATE = 10f;

    // TODO: Document and break-down into smaller pieces.
    // Input-driven speedup/slowdown
    private void updateSpeed()
    {
        float oldSpeed = this.surfaceEffector2D.speed;

        this.surfaceEffector2D.speed = Mathf.Min(
            Mathf.Max(
                PlayerMovement.MIN_SPEED
                , this.surfaceEffector2D.speed + (this.inputVertical * Time.deltaTime * PlayerMovement.ACCELERATION_RATE)
            )
            , PlayerMovement.MAX_SPEED
        );

        Debug.Log(string.Format("PlayerController.updateSpeed user input speed change [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
    }


    private float EXPONENTIAL_SPEED_NORMALIZATION_RATE = 10f;

    // TODO: add defaultSpeed as a system variable

    // TODO: Document and break-down into smaller pieces.
    // Player speed normalized back to DEFAULT_SPEED in the absence of user input
    private void normalizeSpeed(float speedNormalizationRate)
    {
        float oldSpeed = this.surfaceEffector2D.speed;

        float speedDifferential = (this.surfaceEffector2D.speed - this.defaultSpeed) / speedNormalizationRate;
        this.surfaceEffector2D.speed = Mathf.Approximately(this.defaultSpeed, this.surfaceEffector2D.speed - speedDifferential)
            ? this.defaultSpeed
            : this.surfaceEffector2D.speed - speedDifferential
        ;

        Debug.Log(string.Format("PlayerController.normalizeSpeed [from: {0}] [to: {1}]!", oldSpeed, this.surfaceEffector2D.speed));
    }
}
