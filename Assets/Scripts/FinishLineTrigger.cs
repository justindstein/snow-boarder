using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("FinishLineTrigger.OnTriggerEnter2D: " + other.gameObject.name);

        // Player collision with finish line
        if (other.gameObject.Equals(this.player))
        {
            Debug.Log("Player collision with finish line");
        }
        else
        {
            //Debug.Log(string.Format("not equals: {0} {1}", other.gameObject, this.player));
        }
    }
}
