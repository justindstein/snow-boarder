using UnityEngine;

public class GroundSpeed : MonoBehaviour
{
    public FloatVariable DefaultSpeed;

    private SurfaceEffector2D surfaceEffector2D;

    void Start()
    {
        this.surfaceEffector2D = this.GetComponent<SurfaceEffector2D>();
        this.surfaceEffector2D.speed = this.DefaultSpeed.Value;
    }

    public float GetGroundSpeed()
    {
        return this.surfaceEffector2D.speed;
    }

    public float SetGroundSpeed(float groundSpeed)
    {
        return this.surfaceEffector2D.speed = groundSpeed;
    }
}
