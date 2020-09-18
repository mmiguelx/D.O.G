using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public AudioSource audioSource;

    public BattleState state;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public ScreenHUD screenHUD;

    private bool onAction = false;

    public FadeLoader animator;
    public BattleLoader bl;

    public Cutscene Mid;
    public Cutscene Ending;
    public Cutscene Defeat1;
    public Cutscene Defeat2;

    public Animator playerAnimator;
    public Animator enemyAnimator;

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
        //playerUnit = playerGO.GetComponent<Unit>();
        playerUnit = BattleInfoBridge.instance.GetPlayer();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        enemyUnit.initEUnit();
        playerAnimator.runtimeAnimatorController = playerUnit.animc;
        enemyAnimator.runtimeAnimatorController = enemyUnit.animc;
        //audioSource = BattleInfoBridge.instance.GetEnemy().audioSource;
        audioSource.clip = BattleInfoBridge.instance.GetEnemy().audioSource.clip;
        audioSource.outputAudioMixerGroup = BattleInfoBridge.instance.GetEnemy().audioSource.outputAudioMixerGroup;
        audioSource.Play();
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
            {
                screenHUD.writeLog("You choose red\n\n");
                if (playerAnimator.GetBool("att"))
                    enemyUnit.reproductor.PlayOneShot(playerUnit.attackR);
                playerAnimator.SetBool("red", true);
            }
            if (action == 1)
            {
                screenHUD.writeLog("You choose blue\n\n");
                if (playerAnimator.GetBool("att"))
                    enemyUnit.reproductor.PlayOneShot(playerUnit.attackB);
                playerAnimator.SetBool("blue", true);
            }
            if (action == 2)
            {
                screenHUD.writeLog("You choose green\n\n");
                if (playerAnimator.GetBool("att"))
                    enemyUnit.reproductor.PlayOneShot(playerUnit.attackG);
                playerAnimator.SetBool("green", true);
            }
        }
        else
        {
            if (action == 0)
            {
                screenHUD.writeLog(enemyUnit.unitName + " choose red\n\n");
                if (enemyAnimator.GetBool("att"))
                    enemyUnit.reproductor.PlayOneShot(enemyUnit.attackR);
                enemyAnimator.SetBool("red", true);
            }
            if (action == 1)
            {
                screenHUD.writeLog(enemyUnit.unitName + " choose blue\n\n");
                if (enemyAnimator.GetBool("att"))
                    enemyUnit.reproductor.PlayOneShot(enemyUnit.attackB);
                enemyAnimator.SetBool("blue", true);
            }
            if (action == 2)
            {
                screenHUD.writeLog(enemyUnit.unitName + " choose green\n\n");
                if (enemyAnimator.GetBool("att"))
                    enemyUnit.reproductor.PlayOneShot(enemyUnit.attackG);
                enemyAnimator.SetBool("green", true);
            }
    }
        CombatHistory.instance.Add(action);
    }

    IEnumerator PlayerAttack(int action)
    {
        enemyAnimator.SetBool("att", false);
        playerAnimator.SetBool("att", true);
        //comprobar quien gana y ejecutar acción ofensiva según ganar empatar o perder
        CombatLog(action, true);
        yield return new WaitForSeconds(1f);

        int enemyAction = enemyUnit.getActionD(); //acción enemiga simple
        CombatLog(enemyAction, false);
        enemyUnit.reproductor.PlayOneShot(enemyUnit.defend);
        yield return new WaitForSeconds(1f);

        int resolve = actionResolver(action, enemyAction);
        bool isDead = enemyUnit.TakeDamage(resolve * playerUnit.damage);
        if (resolve == 2)
        {
            screenHUD.writeLog("Critical strike!\n");
            yield return new WaitForSeconds(1f);
        }
        screenHUD.writeLog("You did " + resolve * playerUnit.damage + " points of damage\n");
        enemyHUD.SetHP(enemyUnit.currentHP);
        if (isDead)
        {
            state = BattleState.WON;
            BattleInfoBridge.instance.SetPlayer(playerUnit);
            StartCoroutine(EndBattle());
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

        enemyAnimator.SetBool("att", true);
        playerAnimator.SetBool("att", false);

        int enemyAction = enemyUnit.getActionA(); //acción enemiga simple
        CombatLog(enemyAction, false);
        yield return new WaitForSeconds(1f);

        CombatLog(action, true);
        enemyUnit.reproductor.PlayOneShot(playerUnit.defend);
        yield return new WaitForSeconds(1f);

        int resolve = actionResolver(action, enemyAction);
        if (resolve != 1)
            resolve += 2;
        if (resolve > 2)
            resolve = 0;
        if (resolve == 2)
        {
            screenHUD.writeLog("Critical strike!\n");
            yield return new WaitForSeconds(1f);
        }
        bool isDead = playerUnit.TakeDamage(resolve * enemyUnit.damage);
        screenHUD.writeLog("You take " + resolve * enemyUnit.damage + " points of damage\n");
        if (enemyUnit.unitName == "SS08" && resolve == 2) //cosas de boss implementadas de forma chorra
        {
            enemyUnit.damage++;
            screenHUD.writeLog("SS08 gained strength!");
        }
        playerHUD.SetHP(playerUnit.currentHP);
        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
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

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            screenHUD.writeLog("You win\n");
            yield return new WaitForSeconds(1f);
            screenHUD.writeLog("You leveled up!\n");
            playerUnit.unitLevel++;
            if (playerUnit.unitLevel % 2 == 0)
            {
                screenHUD.writeLog("You gain 1 more damage\n");
                playerUnit.damage++;
            }
            else
            {
                screenHUD.writeLog("You gain 5 more health\n");
                playerUnit.maxHP += 5;
                playerUnit.currentHP += 5;
            }
            yield return new WaitForSeconds(2f);
            if (enemyUnit.unitName == "Circlo")
            {
                 animator.reproduceCutscene(Mid);
                 animator.fadeExit(4);
            }
            else if (enemyUnit.unitName == "SS08"){
                animator.reproduceCutscene(Ending);
                bl.resetAccess();
                animator.fadeExit(4);
            }
            else {
                animator.fadeExit(2);
            }
        }
        else if (state == BattleState.LOST)
        {
            screenHUD.writeLog("You lose");
            yield return new WaitForSeconds(2f);
            if (enemyUnit.unitName == "Rectanguler" || enemyUnit.unitName == "Circlo")
            {
                animator.reproduceCutscene(Defeat1);
                animator.fadeExit(4);
            }
            else
            {
                animator.reproduceCutscene(Defeat2);
                animator.fadeExit(4);
            }
            bl.resetAccess();
            //SceneManager.LoadScene(0);
        }
        CombatHistory.instance.Clear();
    }

    void PlayerTurn()
    {
        screenHUD.writeLog("Player turn\n\n");
        onAction = false;
    }

    void EnemyTurn()
    {
        screenHUD.writeLog("Enemy turn\n\n");
        onAction = false;
    }

    public void OnActionButton(int action)
    {
        if (onAction)
            return;
        else
            onAction = true;
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerAttack(action));
        else if (state == BattleState.ENEMYTURN)
            StartCoroutine(EnemyAttack(action));
    }
}
