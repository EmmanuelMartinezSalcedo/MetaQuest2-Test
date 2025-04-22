using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DispararFoamConDosManos : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    public GameObject foamPrefab;    // Prefab de la espuma
    public Transform foamSpawner;    // Lugar desde donde sale la espuma
    public float foamSpeed = 10f;     // Velocidad de la espuma
    public float fireRate = 0.1f;     // Cada cuánto dispara

    public bool isA;
    public bool isB;
    public bool isC;

    private bool dosManosActivas = false;
    private float nextFireTime = 0f;
    public AudioSource foamAudioSource;   // ← Arrastrás el AudioSource desde el inspector

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        VerificarManos();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        VerificarManos();
    }

    void Update()
    {
        if (dosManosActivas)
        {
            if (Time.time >= nextFireTime)
            {
                DispararFoam();
                nextFireTime = Time.time + fireRate;

                // Iniciar sonido si no está sonando
                if (foamAudioSource != null && !foamAudioSource.isPlaying)
                {
                    foamAudioSource.Play();
                }
            }
        }
        else
        {
            // Si no hay dos manos, detener el sonido si está sonando
            if (foamAudioSource != null && foamAudioSource.isPlaying)
            {
                foamAudioSource.Stop();
            }
        }
    }

    private void VerificarManos()
    {
        int manos = interactorsSelecting.Count;
        dosManosActivas = manos >= 2;

        // Cortar el audio si las manos bajan de 2
        if (!dosManosActivas && foamAudioSource != null && foamAudioSource.isPlaying)
        {
            foamAudioSource.Stop();
        }
    }

    private void DispararFoam()
    {
        if (foamPrefab != null && foamSpawner != null)
        {
            GameObject foam = Instantiate(foamPrefab, foamSpawner.position, foamSpawner.rotation);

            // PASAR los atributos al foam
            foam foamScript = foam.GetComponent<foam>();
            if (foamScript != null)
            {
                foamScript.isA = this.isA;
                foamScript.isB = this.isB;
                foamScript.isC = this.isC;
            }
            else
            {
                Debug.LogWarning("El prefab de Foam no tiene un script 'Foam' asignado!");
            }

            // Aplicar movimiento
            Rigidbody rb = foam.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = foamSpawner.forward * foamSpeed;
            }
        }
        else
        {
            Debug.LogWarning("Falta asignar FoamPrefab o FoamSpawner en el inspector!");
        }
    }
}
