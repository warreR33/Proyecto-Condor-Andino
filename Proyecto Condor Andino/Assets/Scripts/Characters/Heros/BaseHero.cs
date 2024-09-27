using UnityEngine;

public class BaseHero : BaseCharacter
{
    public BaseWeapon[] equippedWeapons = new BaseWeapon[2];  // Dos slots de armas
    public HeroUIController heroUIController;

    [SerializeField] public int DEX;
    [SerializeField] public int STR;
    [SerializeField] public int INT;

    
        
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
            BaseWeapon weapon = equippedWeapons[slot];
            if (!weapon.HasRequiredAttributes(this))
            {
                Debug.Log("No cumples con los requisitos para usar esta arma. El daño será reducido a la mitad.");
                weapon.damage *= 0.5f;  // Reduce el daño a la mitad si no cumple con los requisitos
            }

            // Iniciar la selección de objetivo para el ataque básico
            CharacterSelector characterSelector = FindObjectOfType<CharacterSelector>();
            characterSelector.StartSelectingTarget(this, slot, "BasicAttack");
        }
    }

    public void UseAbility1(int slot)
    {
        if (CanAct() && equippedWeapons[slot] != null)
        {
            // Iniciar la selección de objetivo para la habilidad 1
            CharacterSelector characterSelector = FindObjectOfType<CharacterSelector>();
            characterSelector.StartSelectingTarget(this, slot, "Ability1");
        }
    }

    public void UseAbility2(int slot)
    {
        if (CanAct() && equippedWeapons[slot] != null)
        {
            // Iniciar la selección de objetivo para la habilidad 2
            CharacterSelector characterSelector = FindObjectOfType<CharacterSelector>();
            characterSelector.StartSelectingTarget(this, slot, "Ability2");
        }
    }

    

}
