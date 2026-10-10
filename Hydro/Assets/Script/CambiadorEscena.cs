using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiadorEscena : MonoBehaviour
{
    [Header("Configuración de Cambio de Escena")]
    public string nombreEscenaSiguiente = "Beach - resort";

    [Header("UI del Menú en esta Escena (Opcional)")]
    public GameObject canvasMenuInicio; // Arrastra tu Menú de inicio aquí si estás en la escena del mar

    // Esta variable recuerda que el juego ya empezó, incluso si cambiamos de escena
    public static bool yaInicioPartida = false;

    private void Start()
    {
        // Si ya habíamos iniciado partida y regresamos del mar, ocultamos el menú automáticamente
        if (yaInicioPartida && canvasMenuInicio != null)
        {
            canvasMenuInicio.SetActive(false);
        }
    }

    private void Update()
    {
        // Al presionar la tecla 'V' cambia directamente de escena
        if (Input.GetKeyDown(KeyCode.V))
        {
            CambiarDeEscena();
        }
    }

    // Este método lo asignas a tus botones (Boton Salir, Boton Jugar, etc.)
    public void CambiarDeEscena()
    {
        // Marcamos que la partida ya arrancó para que no vuelva a salir el menú
        yaInicioPartida = true;

        if (!string.IsNullOrEmpty(nombreEscenaSiguiente))
        {
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
    }
}