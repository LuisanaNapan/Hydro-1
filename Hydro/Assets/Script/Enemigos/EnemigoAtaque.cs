using UnityEngine;

public class EnemigoAtaque : MonoBehaviour
{
    [Header("Configuración de Persecución")]
    public Transform objetivoSubmarino;
    public float velocidadMovimiento = 3f;
    public float distanciaAtaque = 15f;

    [Header("Configuración de Daño")]
    public float cantidadDano = 20f;
    public float tiempoEntreAtaques = 1.5f;
    private float tiempoSiguienteAtaque = 0f;

    void Start()
    {
        if (objetivoSubmarino == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                objetivoSubmarino = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (objetivoSubmarino == null) return;

        float distancia = Vector3.Distance(transform.position, objetivoSubmarino.position);

        if (distancia <= distanciaAtaque)
        {
            transform.LookAt(objetivoSubmarino);
            transform.position = Vector3.MoveTowards(transform.position, objetivoSubmarino.position, velocidadMovimiento * Time.deltaTime);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ProcesarAtaque(collision.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        ProcesarAtaque(other.gameObject);
    }

    void ProcesarAtaque(GameObject objetoTocado)
    {
        if (objetoTocado.CompareTag("Player") || objetoTocado.transform.root.CompareTag("Player"))
        {
            if (Time.time >= tiempoSiguienteAtaque)
            {
                GameObject playerPrincipal = objetoTocado.transform.root.gameObject;

                // 1. Ejecuta el método EXACTO del sistema de Ilumisoft
                playerPrincipal.SendMessageUpwards("ApplyDamage", cantidadDano, SendMessageOptions.DontRequireReceiver);

                // 2. Por si el script Health está en un hijo o componente directo
                playerPrincipal.BroadcastMessage("ApplyDamage", cantidadDano, SendMessageOptions.DontRequireReceiver);

                tiempoSiguienteAtaque = Time.time + tiempoEntreAtaques;
                Debug.Log("¡Ataque ejecutado y daño aplicado con ApplyDamage!");
            }
        }
    }
}