using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorInicioJuego : MonoBehaviour
{
    // Variable estática que no se destruye al cambiar de escena
    public static bool yaInicioPartida = false;

    [Header("UI y Cámaras de la Escena Principal")]
    public GameObject canvasMenuInicio; // El Canvas del Menú Principal
    public GameObject camaraMenu;       // La cámara que enfoca el menú al principio
    public GameObject camaraJuego;      // La cámara que sigue al jugador/playa

    private void Start()
    {
        // Si regresamos de la 'VistaCabina', nos saltamos el menú
        if (yaInicioPartida)
        {
            ActivarModoJuego();
        }
    }

    private void Update()
    {
        // Si ya empezó la partida y presiona 'V', va a la cabina
        if (yaInicioPartida && Input.GetKeyDown(KeyCode.V))
        {
            IrACabina();
        }
    }

    // Método que se asigna al Botón 'JUGAR' del Menú Principal
    public void BotonJugarOnClick()
    {
        yaInicioPartida = true;
        ActivarModoJuego();
    }

    private void ActivarModoJuego()
    {
        // Oculta el menú y cambia la cámara al jugador
        if (canvasMenuInicio != null) canvasMenuInicio.SetActive(false);
        if (camaraMenu != null) camaraMenu.SetActive(false);
        if (camaraJuego != null) camaraJuego.SetActive(true);
    }

    public void IrACabina()
    {
        SceneManager.LoadScene("VistaCabina");
    }
}