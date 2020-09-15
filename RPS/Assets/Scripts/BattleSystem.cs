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

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public ScreenHUD screenHUD;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        screenHUD.eraseLog();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        screenHUD.writeLog("Starting...");

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        screenHUD.eraseLog();

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public int actionResolver(int action, int enemyAction)
    {
        //devolvera 0 si el jugador falla, 1 si empata y 2 si acierta
        if (enemyAction == action)
            return 1;
        else if ((action == 0 && enemyAction == 2) || ((action - 1) == enemyAction))
            return 2;
        else
            return 0;
    }

    void CombatLog(int action, bool type)
    {
        if (type)
        {
            if (action == 0)
                screenHUD.writeLog("Has sacado piedra\n");
            if (action == 1)
                screenHUD.writeLog("Has sacado papel\n");
            if (action == 2)
                screenHUD.writeLog("Has sacado tijera\n");
        }
        else
        {
            if (action == 0)
                screenHUD.writeLog("El enemigo ha sacado piedra\n");
            if (action == 1)
                screenHUD.writeLog("El enemigo ha sacado papel\n");
            if (action == 2)
                screenHUD.writeLog("El enemigo ha sacado tijera\n");
        }
    }

    IEnumerator PlayerAttack(int action)
    {
        //comprobar quien gana y ejecutar acción ofensiva según ganar empatar o perder
        int enemyAction = enemyUnit.getActionD(); //acción enemiga simple

        CombatLog(action, true);
        yield return new WaitForSeconds(1f);
        CombatLog(enemyAction, false);
        yield return new WaitForSeconds(1f);

        int resolve = actionResolver(action, enemyAction);
        bool isDead = enemyUnit.TakeDamage(resolve * playerUnit.damage);
        screenHUD.writeLog("Se han hecho " + resolve * playerUnit.damage + " puntos de daño\n");
        enemyHUD.SetHP(enemyUnit.currentHP);
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
            screenHUD.eraseLog();
            state = BattleState.ENEMYTURN;
            EnemyTurn();
            screenHUD.changeState();
        }
    }

    IEnumerator EnemyAttack(int action)
    {
        //comprobar quien gana y ejecutar acción defensiva según ganar empatar o perder
        int enemyAction = enemyUnit.getActionA(); //acción enemiga simple

        CombatLog(action, true);
        yield return new WaitForSeconds(1f);
        CombatLog(enemyAction, false);
        yield return new WaitForSeconds(1f);

        int resolve = actionResolver(action, enemyAction);
        if (resolve != 1)
            resolve += 2;
        if (resolve > 2)
            resolve = 0;
        bool isDead = playerUnit.TakeDamage(resolve * enemyUnit.damage);
        screenHUD.writeLog("Se han recibido " + resolve * enemyUnit.damage + " puntos de daño\n");
        playerHUD.SetHP(playerUnit.currentHP);
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
            screenHUD.eraseLog();
            state = BattleState.PLAYERTURN;
            PlayerTurn();
            screenHUD.changeState();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            screenHUD.writeLog("Has ganado");
        }
        else if (state == BattleState.LOST)
        {
            screenHUD.writeLog("Has perdido");
        }
    }
    void PlayerTurn()
    {
        screenHUD.writeLog("Player turn\n");
    }

    void EnemyTurn()
    {
        screenHUD.writeLog("Enemy turn\n");
    }

    public void OnActionButton(int action)
    {
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerAttack(action));
        else if (state == BattleState.ENEMYTURN)
            StartCoroutine(EnemyAttack(action));
    }
}
