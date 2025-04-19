using UnityEngine;
using System.Collections;

public class foam : MonoBehaviour
{
    public float lifetime = 3f;              // Tiempo total de vida
    public float decelerationRate = 0.98f;      // Tasa de desaceleración
    public float shrinkDuration = 0.5f;      // Cuánto tarda en reducirse antes de desaparecer

    public bool isA;
    public bool isB;
    public bool isC;

    private Rigidbody rb;
    private Renderer rend;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        CambiarColorPorTipo();

        // Iniciar la rutina de desaparición
        StartCoroutine(ShrinkAndDestroy());
    }

    void Update()
    {
        if (rb != null)
        {
            rb.linearVelocity *= 1f - (decelerationRate * Time.deltaTime);
        }
    }


    void CambiarColorPorTipo()
    {
        if (rend == null) return;

        if (isA)
            rend.material.color = Color.red;
        else if (isB)
            rend.material.color = Color.green;
        else if (isC)
            rend.material.color = Color.blue;
        else
            rend.material.color = Color.white;
    }

    IEnumerator ShrinkAndDestroy()
    {
        yield return new WaitForSeconds(lifetime - shrinkDuration);

        Vector3 initialScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            float t = elapsed / shrinkDuration;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
