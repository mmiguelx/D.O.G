using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoader : MonoBehaviour
{
    static int[] access = new int[] {1, 0, 0, 0, 0};
    public int battleId;
    public FadeLoader FadeLoader;

    public void loadBattle(Enemy eventEnemy)
    {
        if (access[battleId] == 1)
        {
            BattleInfoBridge.instance.SetEnemy(eventEnemy);
            if (battleId != access.Length - 1)
                access[battleId + 1] = 1;
            access[battleId] = 0;
            FadeLoader.fadeExit(3);
        }
    }
}
