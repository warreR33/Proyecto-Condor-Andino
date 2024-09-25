using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType{
    Dexterity,
    Strength,
    Intelect
}

public enum WeaponClass{
    Longsword,
    Claymore,
    Rapier,
    Dagger,
    Staff,
    Quarterstaff,
    Mase,
    Axe,
    Hammer,
    Bow,
    Crossbow,
}
public class BaseWeapon : MonoBehaviour
{
    private string weaponName;

    private int DEX;
    private int STR;
    private int INT;

    private float damage;

    private bool isDoubleHanded = false;


    public void BasicAttack(BaseCharacter target)
    {
        // Lógica para realizar el ataque básico
        Debug.Log($"{weaponName} realiza un ataque básico e inflige {damage} de daño a {target.cName}");

        // Aplica el daño al objetivo
        target.TakeDamage(damage);
    }
}
