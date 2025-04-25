using UnityEngine;

public class BreakObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"El breaker toc? a: {other.name}");
        if (other.gameObject.layer == LayerMask.NameToLayer("Breakable"))
        {
            MeshDestroy meshDestroy = other.GetComponentInParent<MeshDestroy>();

            if (meshDestroy != null)
            {
                meshDestroy.TryDestroy();
            }
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
