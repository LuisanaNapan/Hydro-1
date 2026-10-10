using UnityEngine;

public class IconoMapaIsometrico : MonoBehaviour
{
    [Header("Referencias 3D")]
    public Transform jugador;            // Submarino (PlayerInicio)
    public Vector2 mundoMin = new Vector2(0f, 0f);       // Esquina (0,0) del terreno 3D
    public Vector2 mundoMax = new Vector2(1000f, 1000f);   // Esquina (1000,1000) del terreno 3D

    [Header("4 Puntos de Esquina en la UI")]
    [Tooltip("Punto visual en la imagen donde correspondería el punto 3D (X:0, Z:0)")]
    public RectTransform esquinaInfIzq;

    [Tooltip("Punto visual en la imagen donde correspondería el punto 3D (X:1000, Z:0)")]
    public RectTransform esquinaInfDer;

    [Tooltip("Punto visual en la imagen donde correspondería el punto 3D (X:0, Z:1000)")]
    public RectTransform esquinaSupIzq;

    [Tooltip("Punto visual en la imagen donde correspondería el punto 3D (X:1000, Z:1000)")]
    public RectTransform esquinaSupDer;

    private RectTransform miRect;

    private void Awake()
    {
        miRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (jugador == null || esquinaInfIzq == null || esquinaInfDer == null || esquinaSupIzq == null || esquinaSupDer == null)
            return;

        // 1. Calculamos el porcentaje 3D del jugador (0.0 a 1.0)
        float pctX = Mathf.InverseLerp(mundoMin.x, mundoMax.x, jugador.position.x);
        float pctZ = Mathf.InverseLerp(mundoMin.y, mundoMax.y, jugador.position.z);

        // 2. Leemos las posiciones UI de las 4 esquinas
        Vector2 posInfIzq = esquinaInfIzq.anchoredPosition;
        Vector2 posInfDer = esquinaInfDer.anchoredPosition;
        Vector2 posSupIzq = esquinaSupIzq.anchoredPosition;
        Vector2 posSupDer = esquinaSupDer.anchoredPosition;

        // 3. Interpolación Bilineal (mapea la deformación/rotación exacta de la imagen)
        Vector2 abajo = Vector2.Lerp(posInfIzq, posInfDer, pctX);
        Vector2 arriba = Vector2.Lerp(posSupIzq, posSupDer, pctX);

        Vector2 posicionFinalUI = Vector2.Lerp(abajo, arriba, pctZ);

        // 4. Asignamos la posición al ícono
        miRect.anchoredPosition = posicionFinalUI;

        // 5. Rotación de la flechita
        miRect.localRotation = Quaternion.Euler(0, 0, -jugador.eulerAngles.y);
    }
}