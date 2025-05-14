using GLTFast.Schema;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Collectible : XRGrabInteractable
{
    private IXRInteractor firstInteractor;
    private IXRInteractor secondInteractor;
    public AudioSource audioSource;
    private bool isDisappearing = false;
    public string objectID; // A, B o C

    void Start()
    {

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (firstInteractor == null)
        {
            firstInteractor = args.interactorObject;
        }
        else if (secondInteractor == null && args.interactorObject != firstInteractor)
        {
            secondInteractor = args.interactorObject;
        }

        if (firstInteractor != null && secondInteractor != null && !isDisappearing)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(1);
                ScoreManager.Instance.RegisterCollection(objectID); // <<--- REGISTRA
            }
            isDisappearing = true;
            PlaySoundAndDisappear();
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactorObject == firstInteractor)
            firstInteractor = null;
        else if (args.interactorObject == secondInteractor)
            secondInteractor = null;
    }

    private void PlaySoundAndDisappear()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            Debug.Log("Destructopiedra");
            // Espera la duración del clip antes de destruir el objeto
            Destroy(gameObject, audioSource.clip.length);
        }
        else
        {
            audioSource.Play();
            Debug.Log("No Destructopiedra");
            Destroy(gameObject);
        }
    }
}
