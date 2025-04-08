using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public ParticleSystem sprayParticles;

    private void Update()
    {
        // Detecta cuando se presiona el clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0)) // 0 es el botón izquierdo del mouse
        {
            Debug.Log("Started");
        }

        // Detecta cuando se suelta el clic izquierdo del mouse
        if (Input.GetMouseButtonUp(0)) // 0 es el botón izquierdo del mouse
        {
            Debug.Log("Ended");
        }
    }
}
