using UnityEngine;
using System.Collections;

public class trashDestroyer : MonoBehaviour
{
    public float interval = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyTrashRoutine());
    }

    // Update is called once per frame
    IEnumerator DestroyTrashRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            DestroyAllTrash();
        }
    }

    void DestroyAllTrash()
    {
        var allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int count = 0;

        foreach (var obj in allObjects)
        {
            if (obj.name == "TRASH")
            {
                Destroy(obj);
                count++;
            }
        }

        Debug.Log($"[TrashDestroyer] Destruidos {count} objetos llamados 'TRASH'");
    }
}
