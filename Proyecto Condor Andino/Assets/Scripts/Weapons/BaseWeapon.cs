using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public WeaponType weaponType;
    public WeaponClass weaponClass;
    public BaseCharacter target;

    [SerializeField] private int DEX;
    [SerializeField] private int STR;
    [SerializeField] private int INT;

    [SerializeField] private float damage;

    [SerializeField] private bool isDoubleHanded = false;


    public void BasicAttack()
    {
        if (target != null)
        {
            float finalDamage = damage;

            switch (weaponType)
            {
                case WeaponType.Dexterity:
                    if (damage > target.armorRating)
                    {
                        float overflowDamage = damage - target.armorRating;
                        target.TakeDamage(overflowDamage);  // Daño sobrante a la vida
                        target.TakeArmorDamage(damage);     // Daño total a la durabilidad de armadura
                        Debug.Log($"Dexterity: Inflige {overflowDamage} de daño a la vida y reduce {damage} de la durabilidad de armadura.");
                    }
                    else
                    {
                        target.TakeArmorDamage(damage);     // Solo afecta a la durabilidad de la armadura
                        Debug.Log($"Dexterity: Daño insuficiente para pasar la armadura. Se reduce {damage} de la durabilidad de armadura.");
                    }
                    break;

                case WeaponType.Strength:
                    if (damage > target.armorRating)
                    {
                        float overflowDamage = damage - target.armorRating;
                        target.TakeDamage(overflowDamage);  // Daño sobrante a la vida
                        target.TakeArmorDamage(damage);     // Daño total a la durabilidad de armadura
                        Debug.Log($"Strength: Inflige {overflowDamage} de daño a la vida y reduce {damage} de la durabilidad de armadura.");
                    }
                    else
                    {
                        target.TakeArmorDamage(damage * 2); // Daña el armorDurability al doble si no pasa la armadura
                        Debug.Log($"Strength: Daño insuficiente para pasar la armadura. Se reduce {damage * 2} de la durabilidad de armadura.");
                    }
                    break;

                case WeaponType.Intelect:
                    target.TakeDamage(damage);  // Ignora completamente la armadura
                    Debug.Log($"Intelect: Ignora la armadura y hace {damage} de daño directo a la vida.");
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



    public abstract void Ability1();

    public abstract void Ability2();

    protected virtual void SpecialAbility()
    {
        // HABILIDAD ESPECIAL de las armas
        // Lógica especial según el tipo de arma
    }
}
