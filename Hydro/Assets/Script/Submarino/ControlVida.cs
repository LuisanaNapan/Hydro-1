using UnityEngine;
using TMPro; // Si usas TextMeshPro
using UnityEngine.UI; // Si usas Text clásico de UI
using Ilumisoft.HealthSystem;

public class ControlTextoVida : MonoBehaviour
{
    [Header("Referencias")]
    public Health componenteSalud; // El objeto con el script Health (VistaCabina)

    [Header("UI Text (Usa solo uno)")]
    public TextMeshProUGUI textoVidaTMP; // Asigna si usas TextMeshPro
    public Text textoVidaUI;             // Asigna si usas Text UI tradicional

    [Header("Formato")]
    public bool mostrarPorcentaje = false; // Activa si prefieres ver "80%" en vez de "80 / 100"

    private void Start()
    {
        // Si no asignaste la salud en el inspector, intenta buscarla en el Player
        if (componenteSalud == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                componenteSalud = player.GetComponentInChildren<Health>();
            }
        }

        // Actualiza el texto al iniciar el juego
        ActualizarTexto();
    }

    private void Update()
    {
        // Actualiza el contador constantemente si la salud existe
        if (componenteSalud != null)
        {
            ActualizarTexto();
        }
    }

    void ActualizarTexto()
    {
        float vidaActual = Mathf.Max(0, componenteSalud.CurrentHealth);
        float vidaMaxima = componenteSalud.MaxHealth;

        string cadenaTexto = "";

        if (mostrarPorcentaje)
        {
            float porcentaje = (vidaActual / vidaMaxima) * 100f;
            cadenaTexto = $"{Mathf.RoundToInt(porcentaje)}%";
        }
        else
        {
            cadenaTexto = $"{Mathf.RoundToInt(vidaActual)} / {Mathf.RoundToInt(vidaMaxima)}";
        }

        // Asigna la cadena al tipo de texto que estés usando
        if (textoVidaTMP != null)
        {
            textoVidaTMP.text = cadenaTexto;
        }
        if (textoVidaUI != null)
        {
            textoVidaUI.text = cadenaTexto;
        }
    }
}