using UnityEngine;

public class BaseHero : BaseCharacter
{
    public BaseWeapon[] equippedWeapons = new BaseWeapon[2];  // Dos slots de armas
    public HeroUIController heroUIController;

    
        
    public override void Start() {
        base.Start();
        heroUIController = FindObjectOfType<HeroUIController>();
    }

    public void EquipWeapon(BaseWeapon weaponPrefab, int slot)
    {
        if (slot < equippedWeapons.Length)
        {
            // Instanciar el prefab del arma
            BaseWeapon newWeapon = Instantiate(weaponPrefab);
            equippedWeapons[slot] = newWeapon;
        }else{
            
        }
    }
    
    public void BasicAttack(int slot)
    {
        if (CanAct() && equippedWeapons[slot] != null)
        {
            equippedWeapons[slot].BasicAttack();
            ConsumeActionPoint(); // Consumir un punto de acción
            heroUIController.UpdateButtons();
        }
    }

    public void UseAbility1(int slot)
    {
        if (CanAct() && equippedWeapons[slot] != null)
        {
            equippedWeapons[slot].Ability1();
            ConsumeActionPoint();
            heroUIController.UpdateButtons();
        }
    }

    public void UseAbility2(int slot)
    {
        if (CanAct() && equippedWeapons[slot] != null)
        {
            equippedWeapons[slot].Ability2();
            ConsumeActionPoint();
            heroUIController.UpdateButtons();
        }
    }
}
