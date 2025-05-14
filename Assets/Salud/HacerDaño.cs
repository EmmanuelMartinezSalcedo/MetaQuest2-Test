using UnityEngine;

public class HacerDaño : MonoBehaviour
{
    public float cantidadDaño;
    private void OnTriggerEnter (GameObject other)
    {
        Debug.Log("me miente");
        if (other.CompareTag("Player") && other.GetComponent<Codigo_Salud>())
        {
            other.GetComponent<Codigo_Salud>().recibirDaño(cantidadDaño);
            Debug.Log("me duele");
        }
    }
}
