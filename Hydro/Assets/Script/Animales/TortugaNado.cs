using UnityEngine;

public class TortugaNado : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadNado = 1.5f;
    public float velocidadRotacion = 1.0f;

    [Header("Patrullaje")]
    public float radioPatrullaje = 20f;
    public float tiempoCambioDireccion = 6f;

    [Header("Ondulación de Nado")]
    public float frecuenciaOndulacion = 1.5f;
    public float amplitudOndulacion = 0.3f;

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

        // 2. Ondulación suave arriba/abajo para simular el aleteo marino
        float desplazamientoY = Mathf.Sin(Time.time * frecuenciaOndulacion) * amplitudOndulacion * Time.deltaTime;
        transform.position += new Vector3(0, desplazamientoY, 0);

        // 3. Rotar hacia el punto objetivo actual (solo en el plano horizontal)
        Vector3 direccion = (puntoObjetivo - transform.position);
        direccion.y = 0; // Mantiene el cuerpo nivelado en XZ

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * velocidadRotacion);
        }

        // 4. Cambiar de dirección por tiempo o cuando llegue cerca del objetivo
        temporizador += Time.deltaTime;
        float distanciaAlObjetivo = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(puntoObjetivo.x, 0, puntoObjetivo.z));

        if (temporizador >= tiempoCambioDireccion || distanciaAlObjetivo < 2f)
        {
            ObtenerNuevoPuntoAleatorio();
        }
    }

    void ObtenerNuevoPuntoAleatorio()
    {
        // Genera un punto al azar dentro del radio de patrullaje manteniendo la misma altura (Y)
        Vector2 punto2D = Random.insideUnitCircle * radioPatrullaje;
        puntoObjetivo = posicionInicial + new Vector3(punto2D.x, 0, punto2D.y);
        temporizador = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Application.isPlaying ? posicionInicial : transform.position, radioPatrullaje);
    }
}