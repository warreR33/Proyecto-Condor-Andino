using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : BaseWeapon
{
    public override void Ability1()
    {
        if (target != null) // Asegúrate de que hay un objetivo seleccionado
        {
            float damage = 20f;  // Daño fijo de la habilidad 1
            target.TakeDamage(damage);  // Inflige daño al objetivo
            Debug.Log($"Ability1 inflige {damage} de daño a {target.cName}");
        }
        else
        {
            Debug.Log("No hay objetivo seleccionado para Ability1");
        }
    }

    public override void Ability2()
    {
        if (target != null)
        {
            float damage = 35f;  // Daño fijo de la habilidad 2
            target.TakeDamage(damage);  // Inflige daño al objetivo
            Debug.Log($"Ability2 inflige {damage} de daño a {target.cName}");
        }
        else
        {
            Debug.Log("No hay objetivo seleccionado para Ability2");
        }
    }
}
