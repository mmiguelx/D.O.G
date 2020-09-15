using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int behaviourA; //el tipo decidirá que behaviour va a tener
    public int behaviourD;
    public int[] unitBehaviourA;
    public int[] unitBehaviourD;

    public int damage;

    public int maxHP;
    public int currentHP;

    public void initEUnit(Enemy enemy)
    {
        this.unitName = enemy.name;
        this.unitLevel = enemy.level;
        this.damage = enemy.damage;
        this.maxHP = enemy.hp;
        this.currentHP = enemy.hp;
        this.behaviourA = enemy.ABehaviourType;
        this.behaviourD = enemy.DBehaviourType;
        this.unitBehaviourA = new int[enemy.ABehaviour.Length];
        this.unitBehaviourD = new int[enemy.DBehaviour.Length];
        for (int i = 0; i < enemy.ABehaviour.Length; i++)
        {
            this.unitBehaviourA[i] = enemy.ABehaviour[i];
        }
        for (int i = 0; i < enemy.DBehaviour.Length; i++)
        {
            this.unitBehaviourD[i] = enemy.DBehaviour[i];
        }
    }


    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    //behaviour 1 = primer int del array es el contador y el resto es el patrón fijo que loopea
    //behaviour 2 = 3 ints que representan la probabilidad de cada uno
    //behaviour 3 = toma la última acción generada segun el valor proporcionado en la primera posicion del array
    //      1 = toma la acción actual del jugador
    //      2 = toma la última acción del enemigo (si estas atacando su última acción de defensa y viceversa)
    //      3 = toma la última acción del jugador (si estas atacando su última acción de defensa y viceversa)
    //      4 = toma la penúltima acción del enemigo (si estas atacando su última acción de ataque; lo mismo con defensa)
    //      5 = toma la penúltima acción del jugador (si estas atacando su última acción de ataque; lo mismo con defensa)
    //behaviour para el resto = random
    public int getActionA()
    {
        int action = 0;

        if (behaviourA == 1)
        {
            action = unitBehaviourA[0];
            unitBehaviourA[0]++;
            if (unitBehaviourA[0] == unitBehaviourA.Length)
                unitBehaviourA[0] = 1;
        }
        else if (behaviourA == 2)
        {
            int max;

            max = unitBehaviourA[0] + unitBehaviourA[1] + unitBehaviourA[2];
            action = Random.Range(0, max);
            if (action < unitBehaviourA[0])
                action = 0;
            else if (action < unitBehaviourA[1])
                action = 1;
            else
                action = 2;
        }
        else if (behaviourA == 3)
        {
            if ((CombatHistory.instance.Get().Count - unitBehaviourA[0]) >= 0)
                action = CombatHistory.instance.Get()[CombatHistory.instance.Get().Count - unitBehaviourA[0]];
            else
                action = Random.Range(0, 3);
        }
        else
        {
            action = Random.Range(0, 3);
        }
        return action;
    }

    public int getActionD()
    {
        int action = 0;

        if (behaviourD == 1)
        {
            action = unitBehaviourD[0];
            unitBehaviourD[0]++;
            if (unitBehaviourD[0] == unitBehaviourD.Length)
                unitBehaviourD[0] = 1;
        }
        else if (behaviourD == 2)
        {
            int max;

            max = unitBehaviourD[0] + unitBehaviourD[1] + unitBehaviourD[2];
            action = Random.Range(0, max);
            if (action < unitBehaviourD[0])
                action = 0;
            else if (action < unitBehaviourD[1])
                action = 1;
            else
                action = 2;
        }
        else if (behaviourD == 3)
        {
            if ((CombatHistory.instance.Get().Count - unitBehaviourD[0]) >= 0)
                action = CombatHistory.instance.Get()[CombatHistory.instance.Get().Count - unitBehaviourD[0]];
            else
                action = Random.Range(0, 3);
        }
        else
        {
            action = Random.Range(0, 3);
        }
        return action;
    }
}
