using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHUD : MonoBehaviour
{
    public Text battleLog;
    public SpriteRenderer coin;
    public Sprite attack;
    public Sprite defend;
    public bool coinState = true;

    public void changeState()
    {
        if (coinState)
            coin.sprite = defend;
        else
            coin.sprite = attack;
        coinState = !coinState;
    }

    public void writeLog(string txt)
    {
        battleLog.text += txt;
    }

    public void eraseLog()
    {
        battleLog.text = "";
    }
}
