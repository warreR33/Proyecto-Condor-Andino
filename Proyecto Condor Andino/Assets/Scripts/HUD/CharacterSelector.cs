using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private BaseCharacter selectedCharacter;
    private BaseHero currentHero; // El héroe que está utilizando la habilidad
    private int activeSlot = -1;  // Slot activo para identificar el arma
    private string actionType;  // "BasicAttack", "Ability1", "Ability2"
    private bool selectingTarget = false;  // Controla si estamos en selección de objetivo

    // Materiales para el resaltado
    public Material highlightMaterial;  // Material para el resaltado
    private Material originalMaterial;  // Material original del objeto resaltado
    private GameObject highlightedObject;  // Objeto que está resaltado

    void Update()
    {
        if (selectingTarget)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                BaseCharacter clickedCharacter = hit.collider.GetComponent<BaseCharacter>();

                // Cambiar el material al pasar el mouse sobre un objeto
                if (clickedCharacter != null)
                {
                    // Resaltamos el objeto
                    if (highlightedObject != hit.collider.gameObject)
                    {
                        ResetHighlight();  // Restablecer el resaltado anterior

                        highlightedObject = hit.collider.gameObject;
                        originalMaterial = highlightedObject.GetComponent<Renderer>().material;
                        highlightedObject.GetComponent<Renderer>().material = highlightMaterial;
                    }

                    if (Input.GetMouseButtonDown(0))  // Solo permitimos la selección en este estado
                    {
                        if (selectedCharacter != null && selectedCharacter != clickedCharacter)
                        {
                            selectedCharacter.DeselectCharacter();
                        }

                        clickedCharacter.SelectCharacter();
                        selectedCharacter = clickedCharacter;

                        // Ejecutamos la acción seleccionada (ataque o habilidad) sobre el personaje
                        ExecuteAction(clickedCharacter);
                        selectedCharacter.DeselectCharacter();
                        ResetHighlight();  // Restablecer el resaltado después de la acción
                    }
                }
                else
                {
                    ResetHighlight();  // Restablecer el resaltado si el mouse no está sobre un objetivo
                }
            }
            else
            {
                ResetHighlight();  // Restablecer el resaltado si el raycast no golpea nada
            }
        }
    }

    private void ResetHighlight()
    {
        if (highlightedObject != null && originalMaterial != null)
        {
            highlightedObject.GetComponent<Renderer>().material = originalMaterial;
            highlightedObject = null;
            originalMaterial = null;
        }
    }

    private IEnumerator EnemyTurnRoutine()
    {
        selectedCharacter.DeselectCharacter();
        yield return new WaitForSeconds(2f);
    }

    public void StartSelectingTarget(BaseHero hero, int slot, string actionType)
    {
        currentHero = hero;
        activeSlot = slot;
        this.actionType = actionType;  // Guardamos el tipo de acción
        selectingTarget = true;  // Entramos en el estado de selección de objetivo
    }

    private void ExecuteAction(BaseCharacter target)
    {
        if (currentHero != null && activeSlot != -1)
        {
            // Asignamos el objetivo seleccionado al arma
            currentHero.equippedWeapons[activeSlot].target = target;

            if (actionType == "BasicAttack")
            {
                currentHero.equippedWeapons[activeSlot].BasicAttack(currentHero);  // Ejecutamos el ataque básico
            }
            else if (actionType == "Ability1")
            {
                currentHero.equippedWeapons[activeSlot].Ability1();  // Ejecutamos la habilidad 1
            }
            else if (actionType == "Ability2")
            {
                currentHero.equippedWeapons[activeSlot].Ability2();  // Ejecutamos la habilidad 2
            }

            currentHero.ConsumeActionPoint();  // Consumimos el punto de acción
            currentHero.heroUIController.UpdateButtons();  // Actualizamos los botones
            selectingTarget = false;  // Terminamos la selección de objetivo
            currentHero = null;
            activeSlot = -1;
        }
    }
}
