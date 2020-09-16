using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoader : MonoBehaviour
{
    public void loadBattle(Enemy eventEnemy)
    {
        BattleInfoBridge.instance.SetEnemy(eventEnemy);
        SceneManager.LoadScene("MyCombat", LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MyCombat"));
        SceneManager.UnloadSceneAsync("Map");
    }
}
