using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    [Header("Configuración de Monedas")]
    public GameObject prefabMoneda; // Arrastra aquí el Prefab de la Moneda
    public Transform[] puntosSpawn;  // Asigna los puntos de spawn en el Inspector
    public float tiempoEntreSpawn = 45f; // Tiempo en segundos

    private List<GameObject> monedasActivas = new List<GameObject>();

    private void Start()
    {
        // Inicia la rutina de generación repetitiva
        InvokeRepeating(nameof(GenerarMoneda), 2f, tiempoEntreSpawn);
    }

    private void GenerarMoneda()
    {
        if (prefabMoneda == null || puntosSpawn.Length == 0) return;

        // Elige un punto de spawn al azar de la lista
        int indiceAleatorio = Random.Range(0, puntosSpawn.Length);
        Transform puntoElegido = puntosSpawn[indiceAleatorio];

        // Instancia la moneda en la posición del punto elegido
        GameObject nuevaMoneda = Instantiate(prefabMoneda, puntoElegido.position, puntoElegido.rotation);

        // Asegúrate de que la moneda tenga el Tag "Moneda"
        nuevaMoneda.tag = "Moneda";

        monedasActivas.Add(nuevaMoneda);
    }
}