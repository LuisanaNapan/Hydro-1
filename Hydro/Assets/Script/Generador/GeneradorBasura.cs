using UnityEngine;

public class GeneradorBasura : MonoBehaviour
{
    [Header("Configuración de Basura")]
    public GameObject[] prefabsPlastico;
    public Transform[] puntosDeSpawn;

    [Header("Tiempos")]
    public float tiempoEntreSpawn = 60f;
    private float temporizador;

    private void Start()
    {
        // Genera la primera oleada de basura inmediatamente en el segundo 0
        GenerarBasuraEnPuntos();

        // Reinicia el temporizador para la siguiente oleada
        temporizador = tiempoEntreSpawn;
    }

    private void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            GenerarBasuraEnPuntos();
            temporizador = tiempoEntreSpawn; // Reinicia la cuenta
        }
    }

    private void GenerarBasuraEnPuntos()
    {
        if (prefabsPlastico.Length == 0 || puntosDeSpawn.Length == 0) return;

        // Recorre los puntos de spawn y coloca un plástico en cada uno (o de forma aleatoria)
        foreach (Transform punto in puntosDeSpawn)
        {
            if (punto != null)
            {
                // Elegir un prefab de plástico al azar de la lista
                int indiceRandom = Random.Range(0, prefabsPlastico.Length);
                Instantiate(prefabsPlastico[indiceRandom], punto.position, punto.rotation);
            }
        }
    }
}