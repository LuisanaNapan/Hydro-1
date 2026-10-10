using System.Collections;
using UnityEngine;
using TMPro;

public class AccionesSubmarino : MonoBehaviour
{
    [Header("UI General de Alerta")]
    public GameObject alertaGrupo; // Arrastra 'AlertaGrupo'
    public GameObject objetoL;     // Arrastra el contenedor 'L'
    public GameObject objetoH;     // Arrastra el contenedor 'H'

    [Header("Configuración de Parpadeo")]
    public float blinkSpeed = 0.4f;
    private Coroutine blinkCoroutine;
    private TextMeshProUGUI textAlertaL;

    private PlasticoIdentificador plasticoCercanoScript;
    private bool esPeligroAnimal = false;

    [Header("Configuración de Disparo (Misil)")]
    public GameObject prefabMisil;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 1000f;

    [Header("Configuración de UI Mapa")]
    public GameObject panelMapa;

    private void Start()
    {
        // Busca el componente de texto dentro de L para hacer el parpadeo
        if (objetoL != null)
        {
            textAlertaL = objetoL.GetComponentInChildren<TextMeshProUGUI>();
        }

        OcultarTodasLasAlertas();
    }

    private void Update()
    {
        if (plasticoCercanoScript != null)
        {
            // Revisa en tiempo real si el plástico tiene un animal cerca
            esPeligroAnimal = plasticoCercanoScript.tieneAnimalEnPeligro;

            // Actualiza los paneles visibles
            ActualizarEstadoUI();

            // Entrada por teclado según el estado
            if (esPeligroAnimal && Input.GetKeyDown(KeyCode.L))
            {
                EjecutarAccionSalvar();
            }
            else if (!esPeligroAnimal && Input.GetKeyDown(KeyCode.H))
            {
                EjecutarAccionRecoger();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moneda"))
        {
            if (GameManager.Instance != null) GameManager.Instance.AddCoins(1);
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Plastico"))
        {
            plasticoCercanoScript = other.GetComponent<PlasticoIdentificador>();

            if (plasticoCercanoScript == null)
            {
                plasticoCercanoScript = other.gameObject.AddComponent<PlasticoIdentificador>();
            }

            if (alertaGrupo != null) alertaGrupo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plastico"))
        {
            plasticoCercanoScript = null;
            OcultarTodasLasAlertas();
        }
    }

    private void ActualizarEstadoUI()
    {
        if (alertaGrupo != null && !alertaGrupo.activeSelf)
            alertaGrupo.SetActive(true);

        if (esPeligroAnimal)
        {
            // Muestra L (Salvar) y oculta H (Recoger normal)
            if (objetoH != null) objetoH.SetActive(false);

            if (objetoL != null && !objetoL.activeSelf)
            {
                objetoL.SetActive(true);
                IniciarParpadeo();
            }
        }
        else
        {
            // Muestra H (Recoger normal) y oculta L (Salvar)
            DetenerParpadeo();
            if (objetoL != null) objetoL.SetActive(false);
            if (objetoH != null && !objetoH.activeSelf) objetoH.SetActive(true);
        }
    }

    // --- PARPADEO ---
    private void IniciarParpadeo()
    {
        if (blinkCoroutine == null && textAlertaL != null)
        {
            blinkCoroutine = StartCoroutine(BlinkRoutine());
        }
    }

    private void DetenerParpadeo()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        if (textAlertaL != null)
        {
            textAlertaL.enabled = true;
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            if (textAlertaL != null)
            {
                textAlertaL.enabled = !textAlertaL.enabled;
            }
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    private void OcultarTodasLasAlertas()
    {
        DetenerParpadeo();
        if (objetoL != null) objetoL.SetActive(false);
        if (objetoH != null) objetoH.SetActive(false);
        if (alertaGrupo != null) alertaGrupo.SetActive(false);
        esPeligroAnimal = false;
    }

    // --- ACCIONES DE JUEGO ---
    public void EjecutarAccionRecoger()
    {
        if (plasticoCercanoScript != null && !esPeligroAnimal)
        {
            if (GameManager.Instance != null) GameManager.Instance.AddTrash(1);
            Destroy(plasticoCercanoScript.gameObject);
            plasticoCercanoScript = null;
            OcultarTodasLasAlertas();
        }
    }

    public void EjecutarAccionSalvar()
    {
        if (plasticoCercanoScript != null && esPeligroAnimal)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddTrash(1);
                GameManager.Instance.AddRescuedAnimal(1);
            }
            Destroy(plasticoCercanoScript.gameObject);
            plasticoCercanoScript = null;
            OcultarTodasLasAlertas();
        }
    }

    // --- BOTONES PANTALLA ---
    public void BotonSalvarOnClick() => EjecutarAccionSalvar();
    public void BotonRecogerOnClick() => EjecutarAccionRecoger();

    public void BotonMisilOnClick()
    {
        if (prefabMisil != null && puntoDisparo != null)
        {
            GameObject misil = Instantiate(prefabMisil, puntoDisparo.position, puntoDisparo.rotation);
            Rigidbody rb = misil.GetComponent<Rigidbody>();
            if (rb != null) rb.AddForce(puntoDisparo.forward * fuerzaDisparo);
        }
    }

    public void BotonMapaOnClick()
    {
        if (panelMapa != null) panelMapa.SetActive(!panelMapa.activeSelf);
    }
}