using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLoader : MonoBehaviour
{
    static int[] access = new int[] {1, 0, 1, 0, 0};
    public int battleId;
    public FadeLoader FadeLoader;
    public GameObject EventButton;
    public GameObject Sprite;

    void Start() {
        Button b;
        SpriteRenderer a;

        if (EventButton == true && access[battleId] == 0)
        {
            b = EventButton.GetComponent<Button>();
            b.interactable = false;
            a = Sprite.GetComponent<SpriteRenderer>();
            a.color = new Color(1f,1f,1f,.5f);
        }
    }

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

    public void resetAccess()
    {
        access[0] = 1;
        for (int i = 1; i < access.Length; i++)
        {
            access[i] = 0;
        }
        Unit player = BattleInfoBridge.instance.GetPlayer();
        player.maxHP = 30;
        player.currentHP = player.maxHP;
        player.unitLevel = 1;
        player.damage = 2;
    }
}
