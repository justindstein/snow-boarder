using UnityEngine;

public class GroundController : MonoBehaviour
{
    public FloatVariable DefaultSpeed;

    void Start()
    {
        this.GetComponent<SurfaceEffector2D>().speed = this.DefaultSpeed.Value;
    }
}
