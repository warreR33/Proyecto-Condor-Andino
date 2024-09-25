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
public abstract class BaseWeapon : MonoBehaviour
{
    private string weaponName;

    public WeaponType weaponType;
    public WeaponClass weaponClass;

    private int DEX;
    private int STR;
    private int INT;

    [SerializeField] private float damage;

    [SerializeField] private bool isDoubleHanded = false;


    public void BasicAttack()
    {
        Debug.Log("Ataque basico");
    }

    public abstract void Ability1();

    public abstract void Ability2();
}
