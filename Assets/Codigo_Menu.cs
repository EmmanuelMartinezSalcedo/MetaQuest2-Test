using UnityEngine;

public class Codigo_Menu : MonoBehaviour
{
    // Prefab del extintor
    public GameObject prefabExtintor;

    // Punto donde se generarán los extintores
    public Transform itemGenerator;

    // Método para instanciar el extintor
    public void InstanciarExtintor()
    {
        if (prefabExtintor != null && itemGenerator != null)
        {
            Instantiate(prefabExtintor, itemGenerator.position, itemGenerator.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab de extintor o punto de generación no asignado.");
        }
    }

    public void Prueba(string mensaje)
    {
        Debug.Log(mensaje);
    }

    // Start is called before the first frame update
    void Start()
    {
        Prueba("Hola desde Start!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
