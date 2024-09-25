using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    // Propiedades básicas
    [SerializeField] public string cName;
    [SerializeField] private float maxHealth;

    private float currentHealth;

    [SerializeField] public int actionPoints;
    [SerializeField] public int currentActionPoints;
    [SerializeField] public float speed;
    [SerializeField] public float armorRating;
    [SerializeField] public float armorDurability;

    public LogDisplay logDisplay;

    private bool isSelected = false;
    public Material defaultMaterial;         // Material original del personaje
    public Material selectedMaterial;        // Material de selección, que será translucido
    public GameObject selectionIndicator;

    private Renderer characterRenderer;
    
    public virtual void Start()
    {
        logDisplay = FindAnyObjectByType<LogDisplay>();
        currentHealth = maxHealth;

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(false); // Iniciar el indicador oculto
        }

        // Asegúrate de que el material de selección esté configurado como transparente
        if (selectedMaterial != null)
        {
            selectedMaterial.SetFloat("_Mode", 3); // Transparent Mode
            Color selectedColor = selectedMaterial.color;
            selectedColor.a = 0.5f;  // Hacerlo semi-transparente
            selectedMaterial.color = selectedColor;
        }
    }

    public void SelectCharacter()
    {
        logDisplay.ClearCombatLog();
        isSelected = true;

        if (selectedMaterial != null)
        {
            GetComponent<Renderer>().material = selectedMaterial;  // Asigna el material de selección
        }

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(true); // Mostrar el indicador de selección
        }

        logDisplay.UpdateCombatLog($"{cName} ha sido seleccionado.");
    }

    public void DeselectCharacter()
    {
        isSelected = false;

        if (defaultMaterial != null)
        {
            GetComponent<Renderer>().material = defaultMaterial;  // Volver al material original
        }

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(false); // Ocultar el indicador de selección
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        string message = $"{cName} ha recibido {amount} de daño. Salud restante: {currentHealth}";
        logDisplay.UpdateCombatLog(message);

        if (currentHealth <= 0)
        {
            // Muerte del personaje (puedes implementar esto luego)
        }
    }

    public void ConsumeActionPoint()
    {
        if (currentActionPoints > 0)
        {
            currentActionPoints--;
        }
    }

    public bool CanAct()
    {
        return currentActionPoints > 0;
    }

    public void ResetActionPoints(int maxActionPoints)
    {
        currentActionPoints = actionPoints;
    }

    public virtual void PerformAction()
    {
        // Implementar acciones específicas en clases derivadas
    }
}
