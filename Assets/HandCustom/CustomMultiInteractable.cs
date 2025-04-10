using System.Collections.Generic;
using UnityEngine;


public class CustomMultiInteractable : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    private Renderer _renderer;

    [Header("Colors")]
    public Color colorWhenTwoInteractors = Color.green;
    public Color defaultColor = Color.white;

    protected override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (_renderer == null)
            return;

        // Check how many interactors are selecting this object
        int interactorCount = interactorsSelecting.Count;

        if (interactorCount == 2)
        {
            _renderer.material.color = colorWhenTwoInteractors;
        }
        else
        {
            _renderer.material.color = defaultColor;
        }
    }
}
