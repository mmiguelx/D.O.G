using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int lastBehaviourA = 0;
    public int lastBehaviourD = 0;
    public int[] unitBehaviourA;
    public int[] unitBehaviourD;

    public int damage;

    public int maxHP;
    public int currentHP;


    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public int getActionA()
    {
        int action;

        action = lastBehaviourA;
        lastBehaviourA++;
        if (lastBehaviourA == unitBehaviourA.Length)
            lastBehaviourA = 0;
        return action;
    }

    public int getActionD()
    {
        int action;

        action = lastBehaviourD;
        lastBehaviourD++;
        if (lastBehaviourD == unitBehaviourD.Length)
            lastBehaviourD = 0;
        return action;
    }
}
