using UnityEngine;

public class CubeTarget : MonoBehaviour
{
    public float maxLife = 100f;
    private float currentLife;
    private Vector3 initialScale;

    public bool isA;
    public bool isB;
    public bool isC;

    public GameObject smokePrefab; // ← Aquí arrastras tu prefab en el Inspector

    [Range(0f, 1f)]
    public float smokeSpawnChance = 0.7f; // 70% de probabilidad por defecto

    private Renderer rend;

    void Start()
    {
        currentLife = maxLife;
        initialScale = transform.localScale;

        rend = GetComponent<Renderer>();
        CambiarColorPorTipo();
    }

    private void OnCollisionEnter(Collision collision)
    {
        foam foamScript = collision.gameObject.GetComponent<foam>();
        if (foamScript != null)
        {
            if (smokePrefab != null && Random.value < smokeSpawnChance)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-0.2f, 0.2f),
                    0f,
                    Random.Range(-0.2f, 0.2f)
                );

                Vector3 spawnPosition = collision.transform.position + randomOffset;
                Instantiate(smokePrefab, spawnPosition, Quaternion.identity);
            }

            if (EsMatch(foamScript))
            {
                float damage = 1f;
                RecibirDanio(damage);
            }
            else{
                float damage = -2f;
                RecibirDanio(damage);
            }

            Destroy(collision.gameObject);
        }
    }

    void RecibirDanio(float damage)
    {
        currentLife -= damage;
        currentLife = Mathf.Max(currentLife, 0f);

        float lifePercent = currentLife / maxLife;
        transform.localScale = initialScale * lifePercent;

        if (currentLife <= 0f)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log(gameObject.name + " ha sido destruido!");
        Destroy(gameObject);
    }

    bool EsMatch(foam foamScript)
    {
        return (isA && foamScript.isA) || (isB && foamScript.isB) || (isC && foamScript.isC);
    }

    void CambiarColorPorTipo()
    {
        if (rend == null) return;

        if (isA) rend.material.color = Color.red;
        else if (isB) rend.material.color = Color.green;
        else if (isC) rend.material.color = Color.blue;
        else rend.material.color = Color.white;
    }
}
