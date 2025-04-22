using UnityEngine;

public class handInput : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private Vector3 moveDirection;
    private bool isMoving = false;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Obtenemos la cámara principal
    }

    void Update()
    {
        if (isMoving)
        {
            // Mover en la dirección calculada
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    public void Avanzar()
    {
        if (mainCamera != null)
        {
            // Tomamos la dirección hacia donde mira la cámara (sin verticalidad)
            Vector3 forward = mainCamera.transform.forward;
            forward.y = 0; // Opcional: evita que se mueva hacia arriba/abajo
            moveDirection = forward.normalized;
            isMoving = true;
        }
    }

    public void Detenerse()
    {
        isMoving = false;
    }
}
