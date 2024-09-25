using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogDisplay : MonoBehaviour
{
    public TextMeshProUGUI turnOrderText; 
    public TextMeshProUGUI combatLogText; 

    public void UpdateTurnOrder(List<BaseCharacter> sortedCharacters)
    {
       
        string turnOrderString = "Orden de Turnos:\n";
        foreach (BaseCharacter character in sortedCharacters)
        {
            turnOrderString += character.cName + " -Vel:" + character.speed + "\n";
        }

        turnOrderText.text = turnOrderString;
    }

    public void UpdateCombatLog(string message)
    {
        combatLogText.text += message + "\n"; 
    }

    public void ClearCombatLog()
    {
        combatLogText.text = ""; // Limpia el texto del log de combate
    }
}
