using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfoBridge : MonoBehaviour
{
    #region Singleton

    public static BattleInfoBridge instance;

    void Awake()
    {
        //DontDestroyOnLoad(this);
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of BattleInfoBridge found!");
            return;
        }
        instance = this;
    }

    #endregion

    public Enemy enemy;
    public GameObject playerGO;
    public Unit player;

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
        if (!player)
        {
            player = playerGO.GetComponent<Unit>();
        }
        return player;
    }

    public void SetPlayer(Unit player)
    {
        this.player = player;
    }
}
