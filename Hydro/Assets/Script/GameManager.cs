using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Textos Contadores")]
    public TextMeshProUGUI trashCounterText;    // Texto de Basura Recogida
    public TextMeshProUGUI coinsCounterText;    // Texto de Monedas Recogidas
    public TextMeshProUGUI rescuedAnimalsText; // Texto de Animales Salvados

    private int trashCount = 0;
    private int coinsCount = 0;
    private int rescuedAnimalsCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateTrashUI();
        UpdateCoinsUI();
        UpdateAnimalsUI();
    }

    // --- CONTADOR BASURA ---
    public void AddTrash(int amount = 1)
    {
        trashCount += amount;
        UpdateTrashUI();
    }

    private void UpdateTrashUI()
    {
        if (trashCounterText != null)
            trashCounterText.text = trashCount.ToString();
    }

    // --- CONTADOR MONEDAS ---
    public void AddCoins(int amount = 1)
    {
        coinsCount += amount;
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        if (coinsCounterText != null)
            coinsCounterText.text = coinsCount.ToString();
    }

    // --- CONTADOR ANIMALES SALVADOS ---
    public void AddRescuedAnimal(int amount = 1)
    {
        rescuedAnimalsCount += amount;
        UpdateAnimalsUI();
    }

    private void UpdateAnimalsUI()
    {
        if (rescuedAnimalsText != null)
            rescuedAnimalsText.text = rescuedAnimalsCount.ToString();
    }
}