using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.VersionControl;

public enum CombatPhase
{
    Start,
    StartTurn,
    SelectTarget,
    EndTurn
}

public class TurnSystem : MonoBehaviour
{
    public CombatPhase currentPhase = CombatPhase.Start;

    public HeroUIController heroUIController;

    public LogDisplay turnOrderUI;
    public GameObject turnButtom;
    
    public List<BaseCharacter> sortedCharacters;

    private int currentTurnIndex = 0;


    void Start()
    {
        turnButtom.SetActive(false);

        StartCombat();

    }

    void StartCombat()
    {
        BaseCharacter[] characters = FindObjectsOfType<BaseCharacter>();
        sortedCharacters = characters.OrderByDescending(character => character.speed).ToList();

        if (turnOrderUI != null)
        {
            turnOrderUI.UpdateTurnOrder(sortedCharacters);
        }

        currentPhase = CombatPhase.StartTurn;
        StartTurn();
    }

    public void StartTurn()
    {
        
        if (turnOrderUI != null)
        {
            turnOrderUI.ClearCombatLog(); 
        }


        if (currentPhase == CombatPhase.StartTurn)
        {
            BaseCharacter currentCharacter = sortedCharacters[currentTurnIndex];
            string characterType = "";

            currentCharacter.ResetActionPoints(currentCharacter.actionPoints);
            // Detecta si el personaje es un héroe o un enemigo
            if (currentCharacter is BaseHero)
            {
                turnButtom.SetActive(true);
                characterType = "Hero";

                BaseHero hero = currentCharacter as BaseHero;

                // Actualizar la UI para el héroe actual
                if (heroUIController != null && hero != null)
                {
                    heroUIController.hero = hero;
                    heroUIController.SetupButtons();  
                }
            }
            else if (currentCharacter is BaseEnemy)
            {
                turnButtom.SetActive(false);
                characterType = "Enemy";

                if (heroUIController != null)
                {
                    heroUIController.ClearButtons(); // Desactivar los botones de habilidades
                }
            }

            string message = $"Comienza el turno de: {currentCharacter.cName} ({characterType})";
            turnOrderUI.UpdateCombatLog(message);

            if( characterType == "Enemy"){
                
                StartCoroutine(EnemyTurnRoutine(currentCharacter));
            }
        }

    }
    
    
    public void EndTurn()
    {
        if (currentPhase == CombatPhase.StartTurn)
        {

            string message = $"Termina el turno de: {sortedCharacters[currentTurnIndex].name}";
            turnOrderUI.UpdateCombatLog(message);
            currentTurnIndex++;

            // Si llegamos al final de la lista, volver al primer personaje
            if (currentTurnIndex >= sortedCharacters.Count)
            {
                currentTurnIndex = 0;
            }

            // Cambiar de fase para iniciar el turno del siguiente personaje
            currentPhase = CombatPhase.StartTurn;
            StartTurn();
        }
    }


    private IEnumerator EnemyTurnRoutine(BaseCharacter enemy)
    {
        enemy.PerformAction();
        // Espera unos segundos antes de realizar la acción
        yield return new WaitForSeconds(2f); // Cambia el valor según lo que necesites

        // Realiza la acción del enemigo
        EndTurn(); // Finaliza el turno
    }

}
