using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        //meter HUD
        Debug.Log("starting...");

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public int actionResolver(int action, int enemyAction)
    {
        //INFO------
        if (action == 0)
            Debug.Log("Has sacado piedra");
        if (action == 1)
            Debug.Log("Has sacado papel");
        if (action == 2)
            Debug.Log("Has sacado tijera");

        if (enemyAction == 0)
            Debug.Log("El enemigo ha sacado piedra");
        if (enemyAction == 1)
            Debug.Log("El enemigo ha sacado papel");
        if (enemyAction == 2)
            Debug.Log("El enemigo ha sacado tijera");
        //--------

        //devolvera 0 si el jugador falla, 1 si empata y 2 si acierta
        if (enemyAction == action)
            return 1;
        else if ((action == 0 && enemyAction == 2) || ((action - 1) == enemyAction))
            return 2;
        else
            return 0;
    }

    IEnumerator PlayerAttack(int action)
    {
        //---
        //comprobar quien gana y ejecutar acción ofensiva según ganar empatar o perder
        int enemyAction = Random.Range(0, 3); //acción enemiga random simple

        int resolve = actionResolver(action, enemyAction);
        bool isDead = enemyUnit.TakeDamage(resolve * playerUnit.damage);
        Debug.Log("Se han hecho " + resolve * playerUnit.damage + " puntos de daño");
        //actualizar HUD
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            //StartCoroutine(EnemyTurn());
            EnemyTurn();
        }
        //---
        yield return new WaitForSeconds(2f);

        // comprobar si el enemigo esta muerto y cambiar estado según lo que ha pasado
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Debug.Log("Has ganado");
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("Has perdido");
        }
    }

    IEnumerator EnemyAttack(int action)
    {
        //---
        //comprobar quien gana y ejecutar acción defensiva según ganar empatar o perder
        int enemyAction = Random.Range(0, 3); //acción enemiga random simple

        int resolve = actionResolver(action, enemyAction);
        if (resolve != 1)
            resolve += 2;
        if (resolve > 2)
            resolve = 0;
        bool isDead = playerUnit.TakeDamage(resolve * enemyUnit.damage);
        Debug.Log("Se han recibido " + resolve * enemyUnit.damage + " puntos de daño");
        //actualizar HUD
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            //StartCoroutine(PlayerTurn());
            PlayerTurn();
        }
        //---
        yield return new WaitForSeconds(2f);

        // comprobar si has muerto y cambiar estado según lo que ha pasado
    }

    void PlayerTurn()
    {
        Debug.Log("Player turn");
        //actualizar HUD para enseñar que hay que elegir una acción de ataque

    }

    void EnemyTurn()
    {
        Debug.Log("Enemy turn");
        //actualizar HUD para enseñar que hay que elegir una acción de defensa

    }

    public void OnActionButton(int action)
    {
        if (state == BattleState.PLAYERTURN)
        {
            StartCoroutine(PlayerAttack(action));
        }
        else if (state == BattleState.ENEMYTURN)
        {
            StartCoroutine(EnemyAttack(action));
        }
    }
}
