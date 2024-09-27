using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUIController : MonoBehaviour
{
    public BaseHero hero;  // Referencia al héroe actual
    public Button buttonBasicAttack1;
    public Button buttonAbility1_1;
    public Button buttonAbility1_2;

    public Button buttonBasicAttack2;
    public Button buttonAbility2_1;
    public Button buttonAbility2_2;

    // Referencias para los íconos de las armas
    public Image weaponIcon1;  // Ícono del arma en el primer slot
    public Image weaponIcon2;  // Ícono del arma en el segundo slot

    public void SetupButtons()
    {
        if (hero != null)
        {
            // Configurar botones y actualizar íconos según el arma equipada en el slot 0
            if (hero.equippedWeapons[0] != null)
            {
                BaseWeapon weapon1 = hero.equippedWeapons[0];
                
                // Asignar el ícono del arma al Image UI correspondiente
                weaponIcon1.sprite = weapon1.weaponIcon;

                // Configurar los botones para el arma 1
                buttonBasicAttack1.onClick.RemoveAllListeners(); // Remover listeners previos
                buttonBasicAttack1.onClick.AddListener(() => hero.BasicAttack(0));
                buttonAbility1_1.onClick.RemoveAllListeners();
                buttonAbility1_1.onClick.AddListener(() => hero.UseAbility1(0));
                buttonAbility1_2.onClick.RemoveAllListeners();
                buttonAbility1_2.onClick.AddListener(() => hero.UseAbility2(0));

                buttonBasicAttack1.interactable = true;
                buttonAbility1_1.interactable = true;
                buttonAbility1_2.interactable = true;
            }
            else
            {
                // Si no hay arma en el slot 0, desactivar los botones e íconos
                buttonBasicAttack1.interactable = false;
                buttonAbility1_1.interactable = false;
                buttonAbility1_2.interactable = false;
                weaponIcon1.sprite = null;  // Quitar ícono
            }

            // Configurar botones y actualizar íconos según el arma equipada en el slot 1
            if (hero.equippedWeapons[1] != null)
            {
                BaseWeapon weapon2 = hero.equippedWeapons[1];
                
                // Asignar el ícono del arma al Image UI correspondiente
                weaponIcon2.sprite = weapon2.weaponIcon;

                // Configurar los botones para el arma 2
                buttonBasicAttack2.onClick.RemoveAllListeners(); // Remover listeners previos
                buttonBasicAttack2.onClick.AddListener(() => hero.BasicAttack(1));
                buttonAbility2_1.onClick.RemoveAllListeners();
                buttonAbility2_1.onClick.AddListener(() => hero.UseAbility1(1));
                buttonAbility2_2.onClick.RemoveAllListeners();
                buttonAbility2_2.onClick.AddListener(() => hero.UseAbility2(1));

                buttonBasicAttack2.interactable = true;
                buttonAbility2_1.interactable = true;
                buttonAbility2_2.interactable = true;
            }
            else
            {
                // Si no hay arma en el slot 1, desactivar los botones e íconos
                buttonBasicAttack2.interactable = false;
                buttonAbility2_1.interactable = false;
                buttonAbility2_2.interactable = false;
                weaponIcon2.sprite = null;  // Quitar ícono
            }
        }
    }

    public void UpdateButtons()
    {
        if (hero != null && hero.CanAct())
        {
            // Si el héroe puede actuar, actualizar el estado de los botones e íconos
            buttonBasicAttack1.interactable = hero.equippedWeapons[0] != null;
            buttonAbility1_1.interactable = hero.equippedWeapons[0] != null;
            buttonAbility1_2.interactable = hero.equippedWeapons[0] != null;
            weaponIcon1.sprite = hero.equippedWeapons[0] != null ? hero.equippedWeapons[0].weaponIcon : null;

            buttonBasicAttack2.interactable = hero.equippedWeapons[1] != null;
            buttonAbility2_1.interactable = hero.equippedWeapons[1] != null;
            buttonAbility2_2.interactable = hero.equippedWeapons[1] != null;
            weaponIcon2.sprite = hero.equippedWeapons[1] != null ? hero.equippedWeapons[1].weaponIcon : null;
        }
        else
        {
            // Si no puede actuar, desactivar los botones e íconos
            buttonBasicAttack1.interactable = false;
            buttonAbility1_1.interactable = false;
            buttonAbility1_2.interactable = false;
            weaponIcon1.sprite = null;

            buttonBasicAttack2.interactable = false;
            buttonAbility2_1.interactable = false;
            buttonAbility2_2.interactable = false;
            weaponIcon2.sprite = null;
        }
    }

    public void ClearButtons()
    {
        // Desactivar o limpiar los botones cuando no es turno de un héroe
        buttonBasicAttack1.onClick.RemoveAllListeners();
        buttonAbility1_1.onClick.RemoveAllListeners();
        buttonAbility1_2.onClick.RemoveAllListeners();

        buttonBasicAttack2.onClick.RemoveAllListeners();
        buttonAbility2_1.onClick.RemoveAllListeners();
        buttonAbility2_2.onClick.RemoveAllListeners();

        buttonBasicAttack1.interactable = false;
        buttonAbility1_1.interactable = false;
        buttonAbility1_2.interactable = false;
        weaponIcon1.sprite = null;  // Quitar ícono

        buttonBasicAttack2.interactable = false;
        buttonAbility2_1.interactable = false;
        buttonAbility2_2.interactable = false;
        weaponIcon2.sprite = null;  // Quitar ícono
    }
}
