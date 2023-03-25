using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.1f;

    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
        target.position, smoothSpeed);

        transform.position = smoothFollow;
    }
}