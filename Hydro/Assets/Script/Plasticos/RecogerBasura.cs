using UnityEngine;

public class RecogerBasura : MonoBehaviour
{
    [Header("UI Minitutorial")]
    public GameObject tutorialPanel; // Arrastra tu panel de la Hierarchy aquí

    private GameObject plasticoCercano;

    private void Start()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // Al presionar H recoge el plástico si está cerca
        if (Input.GetKeyDown(KeyCode.H) && plasticoCercano != null)
        {
            Recoger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plastico"))
        {
            plasticoCercano = other.gameObject;

            if (tutorialPanel != null)
            {
                tutorialPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plastico") && other.gameObject == plasticoCercano)
        {
            plasticoCercano = null;

            if (tutorialPanel != null)
            {
                tutorialPanel.SetActive(false);
            }
        }
    }

    public void Recoger()
    {
        if (plasticoCercano != null)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddTrash(1);
            }

            Destroy(plasticoCercano);
            plasticoCercano = null;

            if (tutorialPanel != null)
            {
                tutorialPanel.SetActive(false);
            }
        }
    }
}