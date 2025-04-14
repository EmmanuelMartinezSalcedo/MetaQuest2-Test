using UnityEngine;

public class foam : MonoBehaviour
{
    public float lifetime = 3f; // Tiempo antes de desaparecer
    public float decelerationRate = 5f; // Tasa de desaceleración
    public bool isA;
    public bool isB;
    public bool isC;

    private Rigidbody rb;
    private Renderer rend;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>(); // Obtener el Renderer para cambiar color

        CambiarColorPorTipo();

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, decelerationRate * Time.deltaTime);
        }
    }

    void CambiarColorPorTipo()
    {
        if (rend == null)
        {
            Debug.LogWarning("No hay Renderer en el objeto Foam!");
            return;
        }

        if (isA)
        {
            rend.material.color = Color.red; // Por ejemplo, tipo A = rojo
        }
        else if (isB)
        {
            rend.material.color = Color.green; // Tipo B = verde
        }
        else if (isC)
        {
            rend.material.color = Color.blue; // Tipo C = azul
        }
        else
        {
            rend.material.color = Color.white; // Si no tiene tipo, blanco
        }
    }
}
