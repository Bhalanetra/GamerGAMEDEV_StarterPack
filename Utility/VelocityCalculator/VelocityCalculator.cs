using UnityEngine;

public class VelocityCalculator : MonoBehaviour
{
    private Vector3 previousPosition;
    public Vector3 velocity;
    public float speed;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        velocity = (currentPosition - previousPosition) / Time.deltaTime;
        speed = velocity.magnitude;
        previousPosition = currentPosition;

        // Optional: Debug output
        Debug.Log("Velocity: " + velocity);
    }
}
