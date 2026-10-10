using UnityEngine;

public class PlasticoIdentificador : MonoBehaviour
{
    public bool tieneAnimalEnPeligro = false;

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si un animal entra al área del plástico
        if (other.CompareTag("Animal"))
        {
            tieneAnimalEnPeligro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detecta si el animal se alejó del plástico
        if (other.CompareTag("Animal"))
        {
            tieneAnimalEnPeligro = false;
        }
    }
}