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

    public void SetupButtons()
    {
        if (hero != null)
        {
            // Verificar si hay un arma equipada en el slot 0
            if (hero.equippedWeapons[0] != null)
            {
                // Slot 1: Configura los botones para el arma 1
                buttonBasicAttack1.onClick.RemoveAllListeners(); // Remover listeners previos
                buttonBasicAttack1.onClick.AddListener(() => hero.BasicAttack(0));
                buttonAbility1_1.onClick.RemoveAllListeners();
                buttonAbility1_1.onClick.AddListener(() => hero.UseAbility1(0));
                buttonAbility1_2.onClick.RemoveAllListeners();
                buttonAbility1_2.onClick.AddListener(() => hero.UseAbility2(0));

                // Activar los botones y actualizar los textos
                buttonBasicAttack1.interactable = true;
                buttonAbility1_1.interactable = true;
                buttonAbility1_2.interactable = true;
                
            }
            else
            {
                // No hay arma en el slot 0, desactivar botones
                buttonBasicAttack1.interactable = false;
                buttonAbility1_1.interactable = false;
                buttonAbility1_2.interactable = false;
                
            }

            // Verificar si hay un arma equipada en el slot 1
            if (hero.equippedWeapons[1] != null)
            {
                // Slot 2: Configura los botones para el arma 2
                buttonBasicAttack2.onClick.RemoveAllListeners(); // Remover listeners previos
                buttonBasicAttack2.onClick.AddListener(() => hero.BasicAttack(1));
                buttonAbility2_1.onClick.RemoveAllListeners();
                buttonAbility2_1.onClick.AddListener(() => hero.UseAbility1(1));
                buttonAbility2_2.onClick.RemoveAllListeners();
                buttonAbility2_2.onClick.AddListener(() => hero.UseAbility2(1));

                // Activar los botones y actualizar los textos
                buttonBasicAttack2.interactable = true;
                buttonAbility2_1.interactable = true;
                buttonAbility2_2.interactable = true;
            }
            else
            {
                // No hay arma en el slot 1, desactivar botones
                buttonBasicAttack2.interactable = false;
                buttonAbility2_1.interactable = false;
                buttonAbility2_2.interactable = false;
            }
        }
    }

    public void UpdateButtons()
    {
        if (hero != null && hero.CanAct())
        {
            // Si hay un arma equipada en el slot 0 y tiene puntos de acción
            buttonBasicAttack1.interactable = hero.equippedWeapons[0] != null;
            buttonAbility1_1.interactable = hero.equippedWeapons[0] != null;
            buttonAbility1_2.interactable = hero.equippedWeapons[0] != null;

            // Si hay un arma equipada en el slot 1 y tiene puntos de acción
            buttonBasicAttack2.interactable = hero.equippedWeapons[1] != null;
            buttonAbility2_1.interactable = hero.equippedWeapons[1] != null;
            buttonAbility2_2.interactable = hero.equippedWeapons[1] != null;
        }
        else
        {
            // Si no tiene puntos de acción, desactivar los botones
            buttonBasicAttack1.interactable = false;
            buttonAbility1_1.interactable = false;
            buttonAbility1_2.interactable = false;

            buttonBasicAttack2.interactable = false;
            buttonAbility2_1.interactable = false;
            buttonAbility2_2.interactable = false;
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

        buttonBasicAttack2.interactable = false;
        buttonAbility2_1.interactable = false;
        buttonAbility2_2.interactable = false;
    }


    
}
