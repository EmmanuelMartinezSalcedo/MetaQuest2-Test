using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DispararFoamConDosManos : XRGrabInteractable
{
    public GameObject foamPrefab;
    public Transform foamSpawner;
    public float foamSpeed = 10f;
    public float fireRate = 0.1f;
    public AudioSource foamAudioSource;

    public bool isA;
    public bool isB;
    public bool isC;

    public Transform anchor1;

    private float nextFireTime = 0f;
    private bool dosManosActivas = false;
    private bool primeraManoEsIzquierda = false;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        string name = args.interactorObject.transform.name.ToLower();

        // Si aún no hay ninguna mano, solo permitir si es la izquierda
        if (interactorsSelecting.Count == 0)
        {
            if (!name.Contains("interactor l"))
            {
                var interactor = args.interactorObject as XRBaseInteractor;
                if (interactor != null && interactor.interactionManager != null)
                {
                    interactor.interactionManager.CancelInteractableSelection((IXRSelectInteractable)this);
                }

                Debug.Log("Debe agarrar primero la mano izquierda.");
                return;
            }

            primeraManoEsIzquierda = true;
        }

        base.OnSelectEntering(args);
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        VerificarManos();

        // Rotar ancla según mano
        string name = args.interactorObject.transform.name.ToLower();
        if (name.Contains("interactor l"))
        {
            Vector3 euler = anchor1.localEulerAngles;
            euler.z = 90f;
            anchor1.localEulerAngles = euler;
        }
        else if (name.Contains("interactor r"))
        {
            Vector3 euler = anchor1.localEulerAngles;
            euler.z = -90f;
            anchor1.localEulerAngles = euler;
        }
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

                if (foamAudioSource != null && !foamAudioSource.isPlaying)
                {
                    foamAudioSource.Play();
                }
            }
        }
        else
        {
            if (foamAudioSource != null && foamAudioSource.isPlaying)
            {
                foamAudioSource.Stop();
            }
        }
    }

    private void VerificarManos()
    {
        dosManosActivas = interactorsSelecting.Count >= 2;

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
            foam foamScript = foam.GetComponent<foam>();

            if (foamScript != null)
            {
                foamScript.isA = isA;
                foamScript.isB = isB;
                foamScript.isC = isC;
            }

            Rigidbody rb = foam.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = foamSpawner.forward * foamSpeed;
            }
        }
    }
}
