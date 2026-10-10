using UnityEngine;

public class CamaraDron : MonoBehaviour
{
    [Header("Velocidades")]
    public float velocidadMovimiento = 10f;
    public float velocidadAumento = 25f; // Al mantener Shift
    public float sensibilidadRaton = 2f;

    private float rotacionX = 0f;
    private float rotacionY = 0f;
    private bool dronActivado = true;

    void Start()
    {
        // Guardamos la rotación inicial
        Vector3 rotacionInicial = transform.eulerAngles;
        rotacionX = rotacionInicial.x;
        rotacionY = rotacionInicial.y;
    }

    void Update()
    {
        // Con la tecla 'F' puedes activar/desactivar el modo dron
        if (Input.GetKeyDown(KeyCode.F))
        {
            dronActivado = !dronActivado;
            Cursor.lockState = dronActivado ? CursorLockMode.Locked : CursorLockMode.None;
        }

        if (!dronActivado) return;

        // --- 1. ROTACIÓN CON EL RATÓN ---
        rotacionY += Input.GetAxis("Mouse X") * sensibilidadRaton;
        rotacionX -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionX = Mathf.Clamp(rotacionX, -80f, 80f); // Evita que la cámara se voltee de cabeza

        transform.rotation = Quaternion.Euler(rotacionX, rotacionY, 0f);

        // --- 2. MOVIMIENTO TIPO DRON ---
        float velActual = Input.GetKey(KeyCode.LeftShift) ? velocidadAumento : velocidadMovimiento;

        // W, A, S, D -> Adelante, Izquierda, Atrás, Derecha
        float moverAdelante = Input.GetAxis("Vertical") * velActual * Time.deltaTime;
        float moverLado = Input.GetAxis("Horizontal") * velActual * Time.deltaTime;

        // Q -> Bajar, E -> Subir
        float moverVertical = 0f;
        if (Input.GetKey(KeyCode.E)) moverVertical = velActual * Time.deltaTime; // Subir
        if (Input.GetKey(KeyCode.Q)) moverVertical = -velActual * Time.deltaTime; // Bajar

        // Aplicar movimiento relativo a la dirección de la cámara
        transform.Translate(new Vector3(moverLado, moverVertical, moverAdelante));
    }
}