using UnityEngine;
using TMPro; // Si usas TextMeshPro

public class ContadorSalvados : MonoBehaviour
{
    public int animalesRescatados = 0;
    public TextMeshProUGUI textoContador; // Asigna tu texto de UI en el Inspector

    public void SumarAnimal()
    {
        animalesRescatados++;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (textoContador != null)
        {
            textoContador.text = "Salvados: " + animalesRescatados;
        }
    }
}