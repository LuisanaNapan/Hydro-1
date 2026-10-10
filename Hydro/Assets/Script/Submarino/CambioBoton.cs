using UnityEngine;

public class ToggleBotonesUI : MonoBehaviour
{
    [Header("Objetos UI de los Botones")]
    public GameObject botonApagado;   // Palanca arriba
    public GameObject botonEncendido; // Palanca abajo

    private bool estaEncendido = false;

    void Start()
    {
        // Forzamos el estado visual correcto al iniciar la escena
        ActualizarEstado();
    }

    public void Alternar()
    {
        estaEncendido = !estaEncendido;
        ActualizarEstado();
    }

    private void ActualizarEstado()
    {
        if (botonApagado != null) botonApagado.SetActive(!estaEncendido);
        if (botonEncendido != null) botonEncendido.SetActive(estaEncendido);
    }
}