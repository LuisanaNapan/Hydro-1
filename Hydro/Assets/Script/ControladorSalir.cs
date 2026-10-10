using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorSalir : MonoBehaviour
{
    // Nombre de tu escena principal/menú
    public string nombreEscenaMenu = "MenuPrincipal";

    // Función pública para asignar al botón de la UI
    public void VolverAlMenu()
    {
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}