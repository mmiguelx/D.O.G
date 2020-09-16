using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoader : MonoBehaviour
{
    //static int[] access = new int[] {1, 0, 0, 0, 0};
    //public int battleId;
    public Animator animator;
    public int scene;

    public void loadBattle(Enemy eventEnemy)
    {
        /*if (access[battleId] == 1)
        {
            BattleInfoBridge.instance.SetEnemy(eventEnemy);
            if (battleId != access.Length)
                access[battleId + 1] = 1;
            access[battleId] = 0;
            fadeExit();
        }*/
        BattleInfoBridge.instance.SetEnemy(eventEnemy);
        fadeExit();
    }

    public void fadeExit()
    {
        animator.SetTrigger("FadeOut");
    }

    public void changeScene()
    {
        SceneManager.LoadScene(scene);
    }
}

