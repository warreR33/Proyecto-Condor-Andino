using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType{
    Dexterity,
    Strength,
    Intelect
}

public enum WeaponClass{

    //FUERZA
    sword, 
    greatsword,
    mace,
    sledgehammer,

    //DESTRESA
    dagger,
    fist,
    spear,
    bow,
    
    //INTELIGENCIA
    staff,
    wand,
    spellbook,
    talisman,
    
    //ESCUDO
    shield,
}

public abstract class BaseWeapon : MonoBehaviour
{
    private string weaponName;
    public Sprite weaponIcon;

    public WeaponType weaponType;
    public WeaponClass weaponClass;
    public BaseCharacter target;

    [SerializeField] private int DEX;
    [SerializeField] private int STR;
    [SerializeField] private int INT;

    [SerializeField] public float damage;

    [SerializeField] private bool isDoubleHanded = false;

    // Nueva variable para probabilidad de crítico
    [SerializeField] private float critChance = 0.1f; // 10% de probabilidad por defecto

    // Función para determinar si es un golpe crítico
    private bool IsCriticalHit()
    {
        float randomValue = Random.Range(0f, 1f); // Genera un número aleatorio entre 0 y 1
        return randomValue <= critChance;
    }

    public bool HasRequiredAttributes(BaseHero hero)
    {
        switch (weaponType)
        {
            case WeaponType.Dexterity:
                return hero.DEX >= DEX;

            case WeaponType.Strength:
                return hero.STR >= STR;

            case WeaponType.Intelect:
                return hero.INT >= INT;

            default:
                return false;
        }
    }

    public void BasicAttack(BaseHero hero)
    {
        if (target != null)
        {
            // Verificar si el héroe cumple con los requisitos del arma
            bool meetsRequirements = HasRequiredAttributes(hero);
            float adjustedDamage = meetsRequirements ? damage : damage / 2;

            // Verificamos si es un golpe crítico
            bool isCritical = IsCriticalHit();
            if (isCritical)
            {
                Debug.Log("¡Golpe crítico!");
                switch (weaponType)
                {
                    case WeaponType.Dexterity:
                        HandleDexterityCrit(adjustedDamage);
                        break;

                    case WeaponType.Strength:
                        HandleStrengthCrit(adjustedDamage);
                        break;

                    default:
                        Debug.LogWarning("El tipo de arma no tiene manejo crítico específico.");
                        break;
                }

                return; // Termina la ejecución después de manejar el crítico
            }

            // Si no es un crítico, ejecutamos el ataque normal
            switch (weaponType)
            {
                case WeaponType.Dexterity:
                    HandleDexterityAttack(adjustedDamage);
                    break;

                case WeaponType.Strength:
                    HandleStrengthAttack(adjustedDamage);
                    break;

                case WeaponType.Intelect:
                    HandleIntellectAttack(adjustedDamage);
                    break;

                default:
                    Debug.LogWarning("Tipo de arma desconocido");
                    break;
            }
        }
        else
        {
            Debug.Log("No hay objetivo seleccionado para BasicAttack");
        }
    }

    // Ataque de Dexterity
    private void HandleDexterityAttack(float adjustedDamage)
    {
        if (adjustedDamage > target.armorRating)
        {
            float overflowDamage = adjustedDamage - target.armorRating;
            target.TakeDamage(overflowDamage);  // Daño sobrante a la vida
            target.TakeArmorDamage(adjustedDamage);     // Daño total a la durabilidad de armadura
            Debug.Log($"Dexterity: Inflige {overflowDamage} de daño a la vida y reduce {adjustedDamage} de la durabilidad de armadura.");
        }
        else
        {
            target.TakeArmorDamage(adjustedDamage);     // Solo afecta a la durabilidad de la armadura
            Debug.Log($"Dexterity: Daño insuficiente para pasar la armadura. Se reduce {adjustedDamage} de la durabilidad de armadura.");
        }
    }

    private void HandleStrengthAttack(float adjustedDamage)
    {
        if (adjustedDamage > target.armorRating)
        {
            float overflowDamage = adjustedDamage - target.armorRating;
            target.TakeDamage(overflowDamage);  // Daño sobrante a la vida
            target.TakeArmorDamage(adjustedDamage);     // Daño total a la durabilidad de armadura
            Debug.Log($"Strength: Inflige {overflowDamage} de daño a la vida y reduce {adjustedDamage} de la durabilidad de armadura.");
        }
        else
        {
            target.TakeArmorDamage(adjustedDamage * 2); // Daña el armorDurability al doble si no pasa la armadura
            Debug.Log($"Strength: Daño insuficiente para pasar la armadura. Se reduce {adjustedDamage * 2} de la durabilidad de armadura.");
        }
    }

    private void HandleIntellectAttack(float adjustedDamage)
    {
        target.TakeDamage(adjustedDamage);  // Ignora completamente la armadura
        Debug.Log($"Intelect: Ignora la armadura y hace {adjustedDamage} de daño directo a la vida.");
    }

    // Funciones de manejo de ataques críticos
    private void HandleDexterityCrit(float adjustedDamage)
    {
        target.TakeDamage(adjustedDamage);  // Ignora completamente la armadura
        Debug.Log($"Crítico de Dexterity: Ignora la armadura y hace {adjustedDamage} de daño directo a la vida.");
        HandleDexterityAttack(adjustedDamage); // Realiza un nuevo ataque
    }

    private void HandleStrengthCrit(float adjustedDamage)
    {
        target.TakeDamage(adjustedDamage);  // Ignora completamente la armadura
        target.TakeArmorDamage(adjustedDamage);  // Hace daño a la armadura igual al daño del arma
        Debug.Log($"Crítico de Strength: Ignora la armadura y hace {adjustedDamage} de daño a la vida y {adjustedDamage} de daño a la armadura.");
    }

    public abstract void Ability1();
    public abstract void Ability2();
}

