using System.Collections;
using UnityEngine;

public class Transicion : MonoBehaviour
{
    [Header("Posiciones de Referencia")]
    public Transform posicionMenu;    // Aquí arrastras PuntoCamaraMenu
    public Transform posicionJuego;   // Aquí arrastras PuntoCamaraJuego

    [Header("Configuración")]
    public float duracionTransicion = 2.0f;
    public GameObject UI_MenuPrincipal;

    private bool enTransicion = false;

    void Start()
    {
        if (posicionMenu != null)
        {
            transform.position = posicionMenu.position;
            transform.rotation = posicionMenu.rotation;
        }
    }

    public void IniciarJuego()
    {
        if (!enTransicion && posicionJuego != null)
        {
            StartCoroutine(MoverCamara());
        }
    }

    IEnumerator MoverCamara()
    {
        enTransicion = true;

        if (UI_MenuPrincipal != null)
        {
            UI_MenuPrincipal.SetActive(false);
        }

        float tiempoTranscurrido = 0f;
        Vector3 posInicial = transform.position;
        Quaternion rotInicial = transform.rotation;

        while (tiempoTranscurrido < duracionTransicion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = tiempoTranscurrido / duracionTransicion;
            t = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(posInicial, posicionJuego.position, t);
            transform.rotation = Quaternion.Lerp(rotInicial, posicionJuego.rotation, t);

            yield return null;
        }

        transform.position = posicionJuego.position;
        transform.rotation = posicionJuego.rotation;
    }
}