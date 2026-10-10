using UnityEngine;

public class BalanceoAlga : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Ángulo máximo de inclinación (en grados)")]
    public float anguloInclinacion = 8f;

    [Tooltip("Velocidad de balanceo")]
    public float velocidad = 2f;

    [Header("Efecto Corriente Marina")]
    [Tooltip("Sensibilidad al estiramiento vertical suave")]
    public float intensidadEstiramiento = 0.05f;

    // Desfase individual para que no todas las algas se muevan idénticas
    private float desfaseAleatorio;
    private Quaternion rotacionInicial;
    private Vector3 escalaInicial;

    void Start()
    {
        rotacionInicial = transform.localRotation;
        escalaInicial = transform.localScale;

        // Genera un desfase para que cada alga baile a su propio ritmo
        desfaseAleatorio = Random.Range(0f, 100f);
    }

    void Update()
    {
        // 1. Cálculo de onda senoidal suave basada en el tiempo
        float tiempo = (Time.time * velocidad) + desfaseAleatorio;
        float anguloZ = Mathf.Sin(tiempo) * anguloInclinacion;
        float anguloX = Mathf.Cos(tiempo * 0.7f) * (anguloInclinacion * 0.5f); // Un leve bamboleo secundario

        // 2. Aplica la rotación sobre la pose inicial
        transform.localRotation = rotacionInicial * Quaternion.Euler(anguloX, 0f, anguloZ);

        // 3. Pequeño efecto de "respiración" o estiramiento con el agua
        float factorEscalaY = 1f + (Mathf.Sin(tiempo * 1.5f) * intensidadEstiramiento);
        transform.localScale = new Vector3(escalaInicial.x, escalaInicial.y * factorEscalaY, escalaInicial.z);
    }
}