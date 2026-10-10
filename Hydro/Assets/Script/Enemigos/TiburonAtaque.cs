using UnityEngine;

public class TiburonAtaque : MonoBehaviour
{
    [Header("Objetivo (Submarino / Cámara)")]
    public Transform objetivo;

    [Header("Movimiento")]
    public float velocidadNado = 3f;
    public float velocidadRotacion = 2f;
    public float radioPatrullaje = 30f; // Distancia máxima que se aleja de donde apareció

    [Header("Ataque")]
    public float distanciaAtaque = 5f;
    public float tiempoEntreAtaques = 2.5f;

    private Animator animator;
    private float temporizador;
    private Vector3 posicionInicial;

    void Start()
    {
        animator = GetComponent<Animator>();
        posicionInicial = transform.position; // Guarda dónde lo generó Variado.cs

        if (objetivo == null && Camera.main != null)
        {
            objetivo = Camera.main.transform;
        }
    }

    void Update()
    {
        // 1. El tiburón avanza de forma constante hacia adelante
        transform.Translate(Vector3.forward * velocidadNado * Time.deltaTime);

        if (objetivo == null) return;

        float distanciaAlObjetivo = Vector3.Distance(transform.position, objetivo.position);

        if (animator != null)
        {
            animator.SetFloat("Distancia", distanciaAlObjetivo);
        }

        // 2. Si el objetivo está cerca, se orienta para morder
        if (distanciaAlObjetivo <= distanciaAtaque)
        {
            Vector3 objetivoHorizontal = new Vector3(objetivo.position.x, transform.position.y, objetivo.position.z);
            Vector3 direccionHorizontal = (objetivoHorizontal - transform.position).normalized;

            if (direccionHorizontal != Vector3.zero)
            {
                Quaternion rotacionDeseada = Quaternion.LookRotation(direccionHorizontal);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * velocidadRotacion);
            }

            temporizador += Time.deltaTime;
            if (temporizador >= tiempoEntreAtaques)
            {
                if (animator != null)
                {
                    animator.SetTrigger("Morder");
                }
                temporizador = 0f;
            }
        }
        else
        {
            temporizador = tiempoEntreAtaques;

            // 3. Si no está atacando y se aleja demasiado de su punto de spawn, gira hacia su centro
            float distanciaAlOrigen = Vector3.Distance(transform.position, posicionInicial);
            if (distanciaAlOrigen > radioPatrullaje)
            {
                Vector3 origenHorizontal = new Vector3(posicionInicial.x, transform.position.y, posicionInicial.z);
                Vector3 direccionOrigen = (origenHorizontal - transform.position).normalized;

                if (direccionOrigen != Vector3.zero)
                {
                    Quaternion rotacionOrigen = Quaternion.LookRotation(direccionOrigen);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotacionOrigen, Time.deltaTime * velocidadRotacion);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Application.isPlaying ? posicionInicial : transform.position, radioPatrullaje);
    }
}