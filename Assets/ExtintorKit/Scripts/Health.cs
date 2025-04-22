using UnityEngine;
using TMPro; // <-- Importante: necesitas esto para usar TextMeshPro

public class Health : MonoBehaviour
{
    public float health = 200.0f;
    public float oxygen = 200.0f;

    public float oxygenLossRate = 5.0f;
    public float oxygenRecoveryRate = 20.0f;
    public float fireDamage = 10.0f;

    private bool inFireZone = false;

    // Referencias a los objetos de texto en la UI
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI oxygenText;

    public Transform cameraTransform;
    public bool followX = true;
    public bool followY = true;
    public bool followZ = true;

    void Start()
    {
        Debug.Log("Script Started");
        Debug.Log("Salud inicial: " + health);

        UpdateUI();
    }

    public void Breathe()
    {
        oxygen += oxygenRecoveryRate * Time.deltaTime;
    }

    void Update()
    {
        Vector3 targetPos = transform.position;

        if (followX) targetPos.x = cameraTransform.position.x;
        if (followY) targetPos.y = cameraTransform.position.y - 1.0f;
        if (followZ) targetPos.z = cameraTransform.position.z;

        transform.position = targetPos;
        // Oxígeno disminuye constantemente
        oxygen -= oxygenLossRate * Time.deltaTime;

        // Recargar oxígeno si el jugador respira (por ejemplo, tecla "R")
        if (Input.GetKey(KeyCode.R))
        {
            oxygen += oxygenRecoveryRate * Time.deltaTime;
        }

        // Clamp para mantener los valores entre 0 y 200
        health = Mathf.Clamp(health, 0, 200);
        oxygen = Mathf.Clamp(oxygen, 0, 200);

        // Si está en zona de fuego, perder salud
        if (inFireZone)
        {
            health -= fireDamage * Time.deltaTime;
        }

        // Actualizar la UI
        UpdateUI();

        if (health <= 0 || oxygen <= 0)
        {
            Debug.Log("Jugador ha muerto.");
            // Aquí puedes manejar la muerte del jugador
        }
    }

    void UpdateUI()
    {
        if (healthText != null)
            healthText.text = $"Vida: {health:F0}";

        if (oxygenText != null)
            oxygenText.text = $"Oxígeno: {oxygen:F0}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Debug.Log("Entró en fuego");
            inFireZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Debug.Log("Salió del fuego");
            inFireZone = false;
        }
    }
}
