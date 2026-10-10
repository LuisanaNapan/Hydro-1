using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlCabina : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidadBase = 5f;
    public float multiplicadorTurbo = 2.5f;
    public float velocidadGiro = 45f;

    [Header("Configuración de Salida / Escena")]
    public string nombreEscenaMar = "Beach - resort";

    [Header("Imágenes / Objetos ON (Encendido)")]
    public GameObject btnIzquierdaOn;
    public GameObject btnDerechaOn;
    public GameObject btnSumergirOn;
    public GameObject btnEmergerOn;
    public GameObject btnAdelanteOn;
    public GameObject btnAtrasOn;
    public GameObject btnTurboOn;

    private float velocidadActual;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            BotonSalirCabinaOnClick();
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            velocidadActual = velocidadBase * multiplicadorTurbo;
            SetEstado(btnTurboOn, true);
        }
        else
        {
            velocidadActual = velocidadBase;
            SetEstado(btnTurboOn, false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -velocidadGiro * Time.deltaTime, 0, Space.Self);
            SetEstado(btnIzquierdaOn, true);
        }
        else
        {
            SetEstado(btnIzquierdaOn, false);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, velocidadGiro * Time.deltaTime, 0, Space.Self);
            SetEstado(btnDerechaOn, true);
        }
        else
        {
            SetEstado(btnDerechaOn, false);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * velocidadActual * Time.deltaTime, Space.World);
            SetEstado(btnEmergerOn, true);
        }
        else
        {
            SetEstado(btnEmergerOn, false);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * velocidadActual * Time.deltaTime, Space.World);
            SetEstado(btnSumergirOn, true);
        }
        else
        {
            SetEstado(btnSumergirOn, false);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.forward * velocidadActual * Time.deltaTime, Space.Self);
            SetEstado(btnAdelanteOn, true);
        }
        else
        {
            SetEstado(btnAdelanteOn, false);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.back * velocidadActual * Time.deltaTime, Space.Self);
            SetEstado(btnAtrasOn, true);
        }
        else
        {
            SetEstado(btnAtrasOn, false);
        }
    }

    void SetEstado(GameObject obj, bool activo)
    {
        if (obj != null)
        {
            obj.SetActive(activo);
        }
    }

    public void BotonSalirCabinaOnClick()
    {
        if (!string.IsNullOrEmpty(nombreEscenaMar))
        {
            SceneManager.LoadScene(nombreEscenaMar);
        }
    }
}