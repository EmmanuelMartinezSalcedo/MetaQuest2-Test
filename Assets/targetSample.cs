using UnityEngine;

public class CubeTarget : MonoBehaviour
{
    public float maxLife = 100f;
    private float currentLife;
    private Vector3 initialScale;

    public bool isA;
    public bool isB;
    public bool isC;

    private Renderer rend;

    void Start()
    {
        currentLife = maxLife;
        initialScale = transform.localScale;

        rend = GetComponent<Renderer>();
        CambiarColorPorTipo();
    }

    private void OnCollisionEnter(Collision collision)
    {
        foam foamScript = collision.gameObject.GetComponent<foam>();
        if (foamScript != null)
        {
            if (EsMatch(foamScript))
            {
                // Match correcto, reducir vida
                float damage = 10f; // Puedes ajustar cuánto daño hace un impacto
                RecibirDanio(damage);
            }

            // Siempre destruir el foam al tocar
            Destroy(collision.gameObject);
        }
    }

    void RecibirDanio(float damage)
    {
        currentLife -= damage;
        currentLife = Mathf.Max(currentLife, 0f);

        // Actualizar la escala proporcionalmente
        float lifePercent = currentLife / maxLife;
        transform.localScale = initialScale * lifePercent;

        if (currentLife <= 0f)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log(gameObject.name + " ha sido destruido!");
        Destroy(gameObject);
    }

    bool EsMatch(foam foamScript)
    {
        return (isA && foamScript.isA) || (isB && foamScript.isB) || (isC && foamScript.isC);
    }

    void CambiarColorPorTipo()
    {
        if (rend == null)
        {
            Debug.LogWarning("No hay Renderer en el cubo!");
            return;
        }

        if (isA)
        {
            rend.material.color = Color.red;
        }
        else if (isB)
        {
            rend.material.color = Color.green;
        }
        else if (isC)
        {
            rend.material.color = Color.blue;
        }
        else
        {
            rend.material.color = Color.white; // Por si no tiene tipo definido
        }
    }
}
