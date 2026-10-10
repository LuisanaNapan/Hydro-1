using UnityEngine;

public class MovimientoSubmarino : MonoBehaviour
{
    public float velocidad = 10f;
    public float velocidadRotacion = 60f;

    void Update()
    {
        // Avanzar y retroceder (W / S o Flechas Arriba/Abajo)
        float movimientoAvanzar = Input.GetAxis("Vertical") * velocidad * Time.deltaTime;
        transform.Translate(0, 0, movimientoAvanzar);

        // Girar a los lados (A / D o Flechas Izquierda/Derecha)
        float giro = Input.GetAxis("Horizontal") * velocidadRotacion * Time.deltaTime;
        transform.Rotate(0, giro, 0);

        // Subir y bajar en el agua (Espacio para subir, Shift para bajar)
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * (velocidad / 2) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * (velocidad / 2) * Time.deltaTime);
        }
    }
}