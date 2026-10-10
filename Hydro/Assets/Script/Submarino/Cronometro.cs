using UnityEngine;
using TMPro; // Si usas TextMeshPro
// using UnityEngine.UI; // Descomenta esta línea si usas el Text antiguo de Unity UI

public class Cronometro : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_Text textoTiempo; // Cambia a "public Text textoTiempo;" si no usas TextMeshPro

    private float tiempoTranscurrido;
    private bool contando = true;

    void Update()
    {
        if (contando)
        {
            // Suma el tiempo real en segundos
            tiempoTranscurrido += Time.deltaTime;

            // Calcula minutos y segundos
            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);

            // Le da formato de 2 dígitos "00:00"
            if (textoTiempo != null)
            {
                textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            }
        }
    }

    // Método opcional para detener el tiempo cuando el submarino sea destruido
    public void DetenerCronometro()
    {
        contando = false;
    }
}