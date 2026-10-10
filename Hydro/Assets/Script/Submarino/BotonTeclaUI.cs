using UnityEngine;
using UnityEngine.UI;

public class BotonTeclaUI : MonoBehaviour
{
    [Header("Configuración de Tecla")]
    public KeyCode teclaAsignada = KeyCode.L; // Cambia la tecla desde el Inspector para cada botón

    [Header("Cambio Visual de Imagen (Opcional)")]
    public GameObject objetoOff; // Arrastra ej. RosaOff
    public GameObject objetoOn;  // Arrastra ej. RosaOn
    public float tiempoBrillo = 0.3f;

    private Button botonUI;
    private float tiempoApagar;

    void Start()
    {
        botonUI = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaAsignada))
        {
            // 1. Ejecuta todo lo que pusiste en el OnClick() del Inspector
            if (botonUI != null)
            {
                botonUI.onClick.Invoke();
            }

            // 2. Si asignaste los objetos Off/On, hace el cambio visual
            if (objetoOff != null && objetoOn != null)
            {
                EncenderVisual();
            }
        }

        // Vuelve a apagar la luz del botón después de unos instantes
        if (objetoOn != null && objetoOn.activeSelf && Time.time >= tiempoApagar)
        {
            ApagarVisual();
        }
    }

    void EncenderVisual()
    {
        objetoOff.SetActive(false);
        objetoOn.SetActive(true);
        tiempoApagar = Time.time + tiempoBrillo;
    }

    void ApagarVisual()
    {
        objetoOff.SetActive(true);
        objetoOn.SetActive(false);
    }
}