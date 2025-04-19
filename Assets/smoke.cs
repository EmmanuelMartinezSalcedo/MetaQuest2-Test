using UnityEngine;

public class smoke : MonoBehaviour
{
    public float lifetime = 2f;
    public float riseSpeed = 0.5f;
    public float swayAmplitude = 0.1f;
    public float swayFrequency = 2f;

    private float timer = 0f;
    private Vector3 initialPosition;
    private Material material;

    void Start()
    {
        initialPosition = transform.position;

        // Clonar el material para que no afecte a otras instancias
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            if (material.HasProperty("_Color"))
            {
                Color color = material.color;
                color.a = 1f;
                material.color = color;
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Movimiento vertical y oscilación horizontal
        float rise = riseSpeed * timer;
        float sway = Mathf.Sin(timer * swayFrequency) * swayAmplitude;
        transform.position = initialPosition + new Vector3(sway, rise, 0f);

        // Desvanecerse
        if (material != null && material.HasProperty("_Color"))
        {
            Color color = material.color;
            color.a = Mathf.Lerp(1f, 0f, timer / lifetime);
            material.color = color;
        }

        // Destruir cuando termina el tiempo
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
