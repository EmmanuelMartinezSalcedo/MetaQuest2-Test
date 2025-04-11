using UnityEngine;

public class foam : MonoBehaviour
{
    public float lifetime = 3f; // Tiempo antes de desaparecer
    public float decelerationRate = 5f; // Tasa de desaceleración (agresiva)
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (rb != null)
        {
            // Reducir la velocidad rápidamente
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, decelerationRate * Time.deltaTime);
        }
    }
}
