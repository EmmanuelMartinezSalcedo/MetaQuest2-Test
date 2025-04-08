using UnityEngine;

public class Fire : MonoBehaviour
{
    public enum FireType { A, B, C }
    public FireType fireType;

    public float health = 100f;
    public float damagePerHit = 10f;

    public ParticleSystem fireParticles;
    public Light fireLight;

    private float initialParticleRate;
    private float initialLightIntensity;

    void Start()
    {
        // Guardar valores iniciales
        if (fireParticles != null)
        {
            var emission = fireParticles.emission;
            initialParticleRate = emission.rateOverTime.constant;
        }

        if (fireLight != null)
        {
            initialLightIntensity = fireLight.intensity;
        }
    }

    public void Damage(FoamType incomingFoam)
    {
        // Solo se daña si el foam coincide
        if ((int)incomingFoam == (int)fireType)
        {
            health -= damagePerHit;
            Debug.Log("🔥 Fuego dañado por: " + incomingFoam);

            UpdateVisuals();

            if (health <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("💨 El foam no es efectivo contra este fuego: " + incomingFoam);
        }
    }

    void UpdateVisuals()
    {
        float healthRatio = Mathf.Clamp01(health / 100f);

        if (fireParticles != null)
        {
            var emission = fireParticles.emission;
            emission.rateOverTime = initialParticleRate * healthRatio;
        }

        if (fireLight != null)
        {
            fireLight.intensity = initialLightIntensity * healthRatio;
        }
    }

    void Die()
    {
        Debug.Log("🔥🔥🔥 El fuego se ha extinguido!");
        if (fireParticles != null) fireParticles.Stop();
        if (fireLight != null) fireLight.enabled = false;
        Destroy(gameObject, 2f); // Darle un tiempito antes de desaparecer
    }

    public void DamageByFoam(FoamType foamType)
{
    Debug.Log("Fuego golpeado con foam tipo: " + foamType);

    // Verificar si el foamType coincide con el fireType
    if ((int)foamType == (int)fireType)
    {
        // Si coincide, aplicamos daño
        health -= damagePerHit;
        Debug.Log("✅ ¡Foam correcto! Daño aplicado.");

        UpdateVisuals();

        if (health <= 0f)
        {
            Die();
        }
    }
    else
    {
        Debug.Log("❌ Este foam no puede apagar este tipo de fuego.");
    }
}

}
