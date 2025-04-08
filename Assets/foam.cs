using UnityEngine;

public enum FoamType
{
    FoamA,
    FoamB,
    FoamC
}

public class Bullet : MonoBehaviour
{
    public FoamType foamType = FoamType.FoamA; // Por defecto FoamA
    public float lifeTime = 3f;
    public float slowdownRate = 0.98f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (rb != null)
        {
            rb.linearVelocity *= slowdownRate;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FireHitbox"))
        {
            // Buscar si el objeto tiene un FireCollider
            FireCollider fireCollider = collision.gameObject.GetComponent<FireCollider>();
            if (fireCollider != null && fireCollider.parentFire != null)
            {
                // Llamar a DamageByFoam pasando el tipo de foam
                fireCollider.parentFire.DamageByFoam(foamType);
            }

            Destroy(gameObject); // Siempre destruir la bala después de pegar
        }
    }
}
