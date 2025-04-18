using UnityEngine;

public class firetest : MonoBehaviour
{
    public float life = 100f; // Vida inicial
    public float damagePerSecond = 1f; // Cuánta vida pierde por segundo de contacto

    private bool isTouchingFoam = false; // ¿Está tocando espuma?

    void Update()
    {
        if (isTouchingFoam)
        {
            life -= damagePerSecond;
            Debug.Log("Ayudin");
            if (life <= 0f)
            {
                Die();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Foam"))
        {
            life -= damagePerSecond;
            Debug.Log("Ayudin");
            if (life <= 0f)
            {
                Die();
            }
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Foam"))
        {
            isTouchingFoam = false;
        }
    }

    void Die()
    {
        Debug.Log("🔥 El fuego se apagó!");
        Destroy(gameObject);
    }
}
