using UnityEngine;

public class BreakObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
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
