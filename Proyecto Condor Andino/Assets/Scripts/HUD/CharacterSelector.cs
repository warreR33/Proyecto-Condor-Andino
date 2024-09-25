using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private BaseCharacter selectedCharacter;

    void Update()
    {
        // Detectar clic izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Intentar obtener el componente BaseCharacter
                BaseCharacter clickedCharacter = hit.collider.GetComponent<BaseCharacter>();

                if (clickedCharacter != null)
                {
                    // Deseleccionar el personaje anterior, si existe
                    if (selectedCharacter != null && selectedCharacter != clickedCharacter)
                    {
                        selectedCharacter.DeselectCharacter();
                    }

                    // Seleccionar el nuevo personaje
                    clickedCharacter.SelectCharacter();
                    selectedCharacter = clickedCharacter;
                }
            }
        }
    }
}
