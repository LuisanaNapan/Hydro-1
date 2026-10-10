using UnityEngine;

public class MantaRayaNado : MonoBehaviour
{
    [Header("Movimiento Base")]
    public float velocidadNado = 2f;
    public float velocidadRotacion = 0.8f; // Gira más lento y amplio que la tortuga

    [Header("Patrullaje")]
    public float radioPatrullaje = 35f;
    public float tiempoCambioDireccion = 8f;

    [Header("Efecto de OLA / Planeo")]
    public float frecuenciaOndulacion = 0.8f; // Movimiento más pausado
    public float amplitudOndulacion = 0.4f;
    public float InclinacionAlGirar = 25f;    // Se inclina hacia los lados al dar la vuelta

    private Vector3 posicionInicial;
    private Vector3 puntoObjetivo;
    private float temporizador;
    private float anguloInclinacionActual;

    void Start()
    {
        posicionInicial = transform.position;
        ObtenerNuevoPuntoAleatorio();
    }

    void Update()
    {
        // 1. Desplazamiento constante hacia adelante
        transform.Translate(Vector3.forward * velocidadNado * Time.deltaTime);

        // 2. Ondulación vertical pausada (simula el planeo en el agua)
        float desplazamientoY = Mathf.Sin(Time.time * frecuenciaOndulacion) * amplitudOndulacion * Time.deltaTime;
        transform.position += new Vector3(0, desplazamientoY, 0);

        // 3. Dirección horizontal hacia el objetivo
        Vector3 direccion = (puntoObjetivo - transform.position);
        direccion.y = 0; // Se mantiene nivelada en el eje vertical

        if (direccion != Vector3.zero)
        {
            // Rotación suave hacia el punto
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion.normalized);

            // Calculamos cuánto está girando para inclinar las aletas (eje Z)
            float anguloGiro = Vector3.SignedAngle(transform.forward, direccion.normalized, Vector3.up);
            float inclinacionDeseada = Mathf.Clamp(-anguloGiro, -InclinacionAlGirar, InclinacionAlGirar);

            // Suavizamos la inclinación lateral
            anguloInclinacionActual = Mathf.Lerp(anguloInclinacionActual, inclinacionDeseada, Time.deltaTime * 2f);

            // Aplicamos la rotación combinada (mirar al punto + inclinación de aletas)
            Vector3 eulerRot = rotacionDeseada.eulerAngles;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(eulerRot.x, eulerRot.y, anguloInclinacionActual), Time.deltaTime * velocidadRotacion);
        }

        // 4. Temporizador para elegir nuevo rumbo
        temporizador += Time.deltaTime;
        float distanciaAlObjetivo = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(puntoObjetivo.x, 0, puntoObjetivo.z));

        if (temporizador >= tiempoCambioDireccion || distanciaAlObjetivo < 3f)
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
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(Application.isPlaying ? posicionInicial : transform.position, radioPatrullaje);
    }
}