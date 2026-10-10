using UnityEngine;

public class ControlLuces : MonoBehaviour
{
    [Header("Configuración de Tecla")]
    public KeyCode teclaLinterna = KeyCode.F; // Tecla F o la que prefieras

    [Header("Luz de la Linterna")]
    public GameObject grupoLinternas; // ¡Cambiado a GameObject para aceptar el grupo completo!

    [Header("Visuales del Botón en Cabina")]
    public GameObject amarilloOff;
    public GameObject amarilloOn;

    private bool encendida = false;

    void Start()
    {
        // Estado inicial: Apagada
        ActualizarEstado();
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaLinterna))
        {
            ToggleLinterna();
        }
    }

    public void ToggleLinterna()
    {
        encendida = !encendida;
        ActualizarEstado();
    }

    void ActualizarEstado()
    {
        // 1. Activa o desactiva el objeto padre "Linternas" completo
        if (grupoLinternas != null)
        {
            grupoLinternas.SetActive(encendida);
        }

        // 2. Alterna las imágenes del botón en el tablero
        if (amarilloOff != null && amarilloOn != null)
        {
            amarilloOff.SetActive(!encendida);
            amarilloOn.SetActive(encendida);
        }
    }
}