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

    //public List<Ability> abilitySlots;    

    public LogDisplay logDisplay;

    private bool isSelected = false;
    public Material defaultMaterial;
    public Material selectedMaterial;
    public GameObject selectionIndicator;

    public virtual void Start()
    {
        logDisplay = FindAnyObjectByType<LogDisplay>();
        currentHealth = maxHealth;

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(false); // Iniciar el indicador oculto
        }
    }

    void Update()
    {
        
    }

    public void SelectCharacter()
    {
        isSelected = true;

        if (selectedMaterial != null)
        {
            GetComponent<Renderer>().material = selectedMaterial;
        }

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(true); // Mostrar el indicador
        }

        logDisplay.UpdateCombatLog($"{cName} ha sido seleccionado.");
    }

    public void DeselectCharacter()
    {
        isSelected = false;

        if (defaultMaterial != null)
        {
            GetComponent<Renderer>().material = defaultMaterial;
        }

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(false); // Ocultar el indicador
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        string message = $"{cName} ha recibido {amount} de daño. Salud restante: {currentHealth}";
        logDisplay.UpdateCombatLog(message);
        
        if (currentHealth <= 0)
        {
            //YES
        }
    }

    public void ConsumeActionPoint()
    {
        if (currentActionPoints > 0)
        {
            currentActionPoints--;
        }
    }

    // Método para verificar si puede realizar acciones
    public bool CanAct()
    {
        return currentActionPoints > 0;
    }

    // Método para resetear los puntos de acción al inicio del turno
    public void ResetActionPoints(int maxActionPoints)
    {
        currentActionPoints = actionPoints;
    }


    public virtual void PerformAction(){
        
    }
}
