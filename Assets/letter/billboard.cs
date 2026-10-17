using UnityEngine;

public class billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 target = Camera.main.transform.position;

        transform.rotation = Quaternion.LookRotation(transform.position - target);
    }
}
