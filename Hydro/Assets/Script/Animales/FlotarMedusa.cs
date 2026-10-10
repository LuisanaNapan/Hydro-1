using UnityEngine;

public class FlotarMedusa : MonoBehaviour
{
    public float velocidadFlotacion = 1f;
    public float amplitud = 0.5f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Movimiento suave de arriba a abajo
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotacion) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);
    }
}