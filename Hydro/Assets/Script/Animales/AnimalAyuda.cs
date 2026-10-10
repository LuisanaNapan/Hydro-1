using System.Collections;
using UnityEngine;
using TMPro;

public class AnimalAyuda : MonoBehaviour
{
    [Header("UI References")]
    public GameObject alertaGrupoPanel;   // Arrastra el objeto 'AlertaGrupo'
    public TextMeshProUGUI alertText;      // Arrastra el texto 'Alerta' o 'Tutorial' de dentro de AlertaGrupo

    [Header("Mensaje Personalizado")]
    [TextArea(2, 4)]
    public string mensajeTutorial = "¡Animal en peligro!\nPresiona L para salvar";

    [Header("Settings")]
    public float blinkSpeed = 0.5f;

    private Coroutine blinkCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MostrarAlerta();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApagarAlerta();
        }
    }

    public void MostrarAlerta()
    {
        if (alertaGrupoPanel != null)
        {
            alertaGrupoPanel.SetActive(true);
        }

        if (alertText != null)
        {
            alertText.text = mensajeTutorial;

            if (blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkText());
            }
        }
    }

    public void ApagarAlerta()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        if (alertText != null)
        {
            alertText.enabled = true; // Restablece la visibilidad
        }

        if (alertaGrupoPanel != null)
        {
            alertaGrupoPanel.SetActive(false); // Apaga todo el grupo UI
        }
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            if (alertText != null)
            {
                alertText.enabled = !alertText.enabled;
            }
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    private void OnDisable()
    {
        // Si el animal es destruido mientras el jugador está cerca, apaga la UI
        ApagarAlerta();
    }
}