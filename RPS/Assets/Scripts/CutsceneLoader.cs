using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLoader : MonoBehaviour
{
    #region Singleton

    public static CutsceneLoader instance;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance != null)
            return;
        instance = this;
    }

    #endregion

    public Cutscene cutscene;

    public Cutscene GetCutscene()
    {
        return cutscene;
    }

    public void SetCutscene(Cutscene cutscene)
    {
        this.cutscene = cutscene;
    }
}
