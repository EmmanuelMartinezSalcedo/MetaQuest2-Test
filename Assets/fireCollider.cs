using UnityEngine;

public class FireCollider : MonoBehaviour
{
    public Fire parentFire; // Referencia al script FireA

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Foam"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null && parentFire != null)
            {
                parentFire.DamageByFoam(bullet.foamType); // 👈 Aquí pasamos el foamType, NO el tag
            }
        }
    }
}
