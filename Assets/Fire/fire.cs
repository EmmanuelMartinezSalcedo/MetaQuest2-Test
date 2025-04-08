using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem extinguisherParticles; // Sistema de partículas del extintor
    public ParticleSystem fireParticles; // Sistema de partículas del fuego
    public GameObject fire; // Objeto que representa el fuego
    public Light fireLight; // Luz del fuego (Point Light)

    private int hits = 0; // Contador de impactos de partículas
    public int hitsToExtinguish = 50; // Número de impactos requeridos para apagar el fuego
    private ParticleSystem.EmissionModule fireEmission; // Controla la emisión de partículas del fuego
    private float initialEmissionRate; // Guarda la tasa inicial de emisión del fuego
    private float initialLightIntensity; // Guarda la intensidad inicial de la luz

    void Start()
    {
        fireEmission = fireParticles.emission; // Obtener el módulo de emisión
        initialEmissionRate = fireEmission.rateOverTime.constantMax; // Guardar el rate inicial (ej. 10)
        
        if (fireLight != null)
        {
            initialLightIntensity = fireLight.intensity; // Guardar la intensidad inicial de la luz
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject == extinguisherParticles.gameObject)
        {
            hits++;
            Debug.Log("Impactos recibidos: " + hits + "/" + hitsToExtinguish);

            // Reducir gradualmente la emisión de partículas del fuego
            float newRate = Mathf.Lerp(initialEmissionRate, 0, (float)hits / hitsToExtinguish);
            fireEmission.rateOverTime = newRate;
            Debug.Log("Nueva emisión de fuego: " + newRate);

            // Reducir la intensidad de la luz del fuego
            if (fireLight != null)
            {
                fireLight.intensity = Mathf.Lerp(initialLightIntensity, 0, (float)hits / hitsToExtinguish);
                Debug.Log("Nueva intensidad de luz: " + fireLight.intensity);
            }

            if (hits >= hitsToExtinguish)
            {
                Debug.Log("¡Fuego apagado!");
                fire.SetActive(false); // Apagar el fuego desactivándolo
                if (fireLight != null) fireLight.enabled = false; // Apagar la luz completamente
            }
        }
    }
}
