using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    // Propiedades básicas

    [SerializeField] public string cName;
    [SerializeField] private float maxHealth;

    private float currentHealt;

    [SerializeField] private int actionPoints;       
    [SerializeField] public float speed;            
    [SerializeField] public float armorRating;      
    [SerializeField] public float armorDurability;  


    public List<BaseWeapon> weaponSlots;      
    //public List<Ability> abilitySlots;    

    public LogDisplay logDisplay;

    void Start()
    {
        logDisplay = FindAnyObjectByType<LogDisplay>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        currentHealt -= amount;
        string message = $"{cName} ha recibido {amount} de daño. Salud restante: {currentHealt}";
        logDisplay.UpdateCombatLog(message);
        
        if (currentHealt <= 0)
        {
            //YES
        }
    }


    public void PerformAction(){
        string message = $"Make noise";
        logDisplay.UpdateCombatLog(message);
        Debug.Log("Action");
    }
}
