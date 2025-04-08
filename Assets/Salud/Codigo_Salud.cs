using UnityEngine;
using UnityEngine.UI;

public class Codigo_Salud : MonoBehaviour
{
    public float Salud = 100;
    public float SaludMax = 100;

    [Header("Interfaz")]
    public Image BarraSalud;
    public Text TextoSalud;
    public CanvasGroup redRojos;
    void Update()
    {
        if (redRojos.alpha > 0)
        {
            redRojos.alpha -= Time.deltaTime;
        }
        ActualizarInterfaz();
    }
    public void recibirDaño(float daño)
    {
        Salud -= daño;
        redRojos.alpha = 1;
    }
    void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMax;
        TextoSalud.text = "+" + Salud.ToString("f0");
    }
}
