using UnityEngine;

public class FlotacionMedusa : MonoBehaviour
{
    public float velocidadFlotacion = 1f; // Qué tan rápido sube y baja
    public float amplitud = 0.5f;           // Qué tanto se desplaza verticalmente
    public float velocidadGiro = 5f;        // Suave rotación sobre su eje

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Movimiento oscilatorio suave arriba y abajo (onda Seno)
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotacion) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);

        // Ligera rotación para darle vida
        transform.Rotate(0, velocidadGiro * Time.deltaTime, 0);
    }
}