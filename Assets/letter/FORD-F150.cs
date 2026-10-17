using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 targetPosition = Camera.main.transform.position;

        // Flatten the target position to match the object's height
        targetPosition.y = transform.position.y;

        transform.rotation = Quaternion.LookRotation(transform.position - targetPosition);
    }
}
