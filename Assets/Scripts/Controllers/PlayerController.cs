using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // TODO: jump on spacebar release, if space bar is held down for longer, then increase height of jump slightly

    [SerializeField] private float reloadDelay;
    [SerializeField] private ParticleSystem jumpEffects;

    //[SerializeField] private float rotateVelocity; // -2
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

    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        //this.surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        this.surfaceEffector2D = GameObject.FindGameObjectWithTag("Ground").GetComponent<SurfaceEffector2D>();
    }

    void Update()
    {
        this.inputVertical = Input.GetAxisRaw("Vertical");
        this.inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) this.inputJump = true;
        //if (Input.GetKey(KeyCode.UpArrow)) this.inputSpeedBoost = true;
    }

    private void FixedUpdate()
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

        // Speed acceleration/deceleration
        this.surfaceEffector2D.speed = Mathf.Min(
            Mathf.Max(
                PlayerController.MIN_SPEED
                , this.surfaceEffector2D.speed + (this.inputVertical * Time.deltaTime * PlayerController.ACCELERATION_RATE)
            )
            , PlayerController.MAX_SPEED
        );
    }

    private void jump()
    {
        this.rigidBody.velocity = new Vector2(this.rigidBody.velocity.x, (Vector2.up.y * this.jumpForce));
        this.inputJump = false;
        this.jumpEffects.Play();
    }

    private void speedBoost()
    {

    }
}
