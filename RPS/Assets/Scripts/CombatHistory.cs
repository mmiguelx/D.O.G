using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHistory : MonoBehaviour
{
    #region Singleton

    public static CombatHistory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of combathistory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public List<int> combatHistory = new List<int>();

    public void Add(int action)
    {
        combatHistory.Add(action);
    }

    public void Remove(int action)
    {
        combatHistory.Remove(action);
    }

    public void Clear()
    {
        combatHistory.Clear();
    }

    public List<int> Get()
    {
        return combatHistory;
    }
}