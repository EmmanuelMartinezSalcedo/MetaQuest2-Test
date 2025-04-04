using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem extinguisherParticles; // Asigna el sistema de partículas del extintor en el Inspector
    public GameObject fire; // Asigna el objeto que representa el fuego en el Inspector

    private int hits = 0; // Contador de impactos de partículas
    public int hitsToExtinguish = 50; // Número de impactos requeridos para apagar el fuego

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Colisión detectada con: " + other.gameObject.name); // Ver si el evento se activa

        if (other.gameObject == extinguisherParticles.gameObject)
        {
            hits++;
            Debug.Log("Impactos recibidos: " + hits + "/" + hitsToExtinguish); // Ver la cuenta de impactos

            if (hits >= hitsToExtinguish)
            {
                Debug.Log("¡Fuego apagado!");
                fire.SetActive(false); // Apagar el fuego desactivándolo
            }
        }
    }
}
