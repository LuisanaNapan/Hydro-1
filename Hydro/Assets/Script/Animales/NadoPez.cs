using UnityEngine;

public class NadoPez : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 3f;
    public float velocidadGiro = 2f;

    // Guardamos un rumbo que cambia paulatinamente
    private float tiempoSiguienteCambio;
    private float giroActual;

    void Update()
    {
        // 1. Avanza constantemente hacia adelante (respetando la rotación local)
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

        // 2. Cambia el objetivo de giro cada cierto tiempo aleatorio
        if (Time.time > tiempoSiguienteCambio)
        {
            giroActual = Random.Range(-1f, 1f); // Un valor entre izquierda (-1) y derecha (1)
            tiempoSiguienteCambio = Time.time + Random.Range(2f, 5f); // Cambia cada 2 a 5 segundos
        }

        // 3. Aplica un giro suave continuo en el eje Y
        transform.Rotate(0, giroActual * velocidadGiro * Time.deltaTime * 10f, 0);
    }
}