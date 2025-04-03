using System.Dynamic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGestureMovement : MonoBehaviour
{
    public Transform xrCamera;
    public float speed = 2.0f;
    private Rigidbody rb;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isMoving && xrCamera != null)
        {
            Vector3 forwardDirection = new Vector3(xrCamera.forward.x, 0, xrCamera.forward.z).normalized;
            rb.linearVelocity = forwardDirection * speed;
        }
    }

    public void OnRightFistBumpDetected()
    {
        // Set the flag to start continuous movement
        isMoving = true;
    }

    public void StopMovement()
    {
        // Set the flag to stop movement and zero out velocity
        isMoving = false;
        rb.linearVelocity = Vector3.zero;
    }
}