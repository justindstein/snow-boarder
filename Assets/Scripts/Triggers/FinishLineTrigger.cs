using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    [Header("Events")]
    public GameEvent FinishLineCrossed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.FinishLineCrossed.Raise(this, other);
    }
}
