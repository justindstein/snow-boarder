using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject followGameObject;

    private void LateUpdate()
    {
        this.transform.position = new Vector3(followGameObject.transform.position.x, followGameObject.transform.position.y, this.transform.position.z);
    }
}