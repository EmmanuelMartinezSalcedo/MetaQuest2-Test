using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;

    private float fireCooldown = 0f;
    private FoamType selectedFoamType = FoamType.FoamA;

    void Update()
    {
        HandleInput();
        HandleFireCooldown();
    }

    void HandleInput()
    {
        // Cambiar tipo de foam con teclas 1, 2, 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedFoamType = FoamType.FoamA;
            Debug.Log("Seleccionado FoamA");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedFoamType = FoamType.FoamB;
            Debug.Log("Seleccionado FoamB");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedFoamType = FoamType.FoamC;
            Debug.Log("Seleccionado FoamC");
        }

        // Disparar mientras se mantenga presionado el botón izquierdo del mouse
        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }

    void HandleFireCooldown()
    {
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRb = bulletObject.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
        }

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.foamType = selectedFoamType;

            // Cambiar color según el tipo de foam
            Renderer renderer = bullet.GetComponent<Renderer>();
            if (renderer != null)
            {
                switch (selectedFoamType)
                {
                    case FoamType.FoamA:
                        renderer.material.color = Color.white; // Blanco para FoamA
                        break;
                    case FoamType.FoamB:
                        renderer.material.color = Color.blue;  // Azul para FoamB
                        break;
                    case FoamType.FoamC:
                        renderer.material.color = Color.green; // Verde para FoamC
                        break;
                }
            }
        }
    }
}
