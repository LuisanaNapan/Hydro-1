using UnityEngine;

public class AnguilaNado : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadNado = 2.5f;
    public float velocidadRotacion = 1.2f;

    [Header("Patrullaje")]
    public float radioPatrullaje = 15f;
    public float tiempoCambioDireccion = 5f;

    [Header("Serpenteo (Para modelos de 1 sola pieza)")]
    public float frecuenciaZigZag = 4f; // Qué tan rápido vibra a los lados
    public float anguloZigZag = 12f;    // Cuánto gira la cabeza a izquierda y derecha

    private Vector3 posicionInicial;
    private Vector3 puntoObjetivo;
    private float temporizador;

    void Start()
    {
        posicionInicial = transform.position;
        ObtenerNuevoPuntoAleatorio();
    }

    void Update()
    {
        // 1. Avanzar constantemente hacia adelante
        transform.Translate(Vector3.forward * velocidadNado * Time.deltaTime);

        // 2. Dirección horizontal hacia el punto de patrullaje
        Vector3 direccion = (puntoObjetivo - transform.position);
        direccion.y = 0; // Mantiene el cuerpo horizontal

        if (direccion != Vector3.zero)
        {
            // Calculamos la rotación base hacia el punto
            Quaternion rotacionBase = Quaternion.LookRotation(direccion.normalized);

            // Generamos la oscilación lateral de serpiente usando la función Seno
            float oscilacionZigZag = Mathf.Sin(Time.time * frecuenciaZigZag) * anguloZigZag;

            // Combinamos la rotación de dirección con el zig-zag en el eje Y
            Quaternion rotacionSerpenteante = rotacionBase * Quaternion.Euler(0, oscilacionZigZag, 0);

            // Aplicamos la rotación suavemente
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionSerpenteante, Time.deltaTime * velocidadRotacion);
        }

        // 3. Temporizador para cambiar de rumbo
        temporizador += Time.deltaTime;
        float distanciaAlObjetivo = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(puntoObjetivo.x, 0, puntoObjetivo.z));

        if (temporizador >= tiempoCambioDireccion || distanciaAlObjetivo < 2f)
        {
            ObtenerNuevoPuntoAleatorio();
        }
    }

    void ObtenerNuevoPuntoAleatorio()
    {
        Vector2 punto2D = Random.insideUnitCircle * radioPatrullaje;
        puntoObjetivo = posicionInicial + new Vector3(punto2D.x, 0, punto2D.y);
        temporizador = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Application.isPlaying ? posicionInicial : transform.position, radioPatrullaje);
    }
}