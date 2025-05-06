using UnityEngine;

public class HandInput : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private bool isMoving = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Obtenemos la cámara principal
    }

    void Update()
    {
        if (isMoving && mainCamera != null)
        {
            // Recalculamos cada frame la dirección de la cámara (sin verticalidad)
            Vector3 forward = mainCamera.transform.forward;
            forward.y = 0f;                // Evita movimiento vertical
            forward.Normalize();           // Para que la magnitud sea 1
            // Movemos el objeto en esa dirección
            transform.position += forward * speed * Time.deltaTime;
        }
    }

    // Llamar para empezar a avanzar
    public void Avanzar()
    {
        isMoving = true;
    }

    // Llamar para detener el movimiento
    public void Detenerse()
    {
        isMoving = false;
    }
}
