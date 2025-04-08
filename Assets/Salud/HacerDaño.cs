using UnityEngine;

public class HacerDaño : MonoBehaviour
{
    public float cantidadDaño;
    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player") && other.GetComponent<Codigo_Salud>())
        {
            other.GetComponent<Codigo_Salud>().recibirDaño(cantidadDaño);
        }
    }
}
