using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfoBridge : MonoBehaviour
{
    #region Singleton

    public static BattleInfoBridge instance;
    public GameObject playerGO;
    public Unit player;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance != null)
            return;
        instance = this;
        player = playerGO.GetComponent<Unit>();

        player.maxHP = 50;
        player.currentHP = player.maxHP;
        player.unitLevel = 1;
    }

    #endregion

    public Enemy enemy;

    public Enemy GetEnemy()
    {
        return enemy;
    }

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public Unit GetPlayer()
    {
        return player;
    }

    public void SetPlayer(Unit player)
    {
        this.player = player;
    }
}
