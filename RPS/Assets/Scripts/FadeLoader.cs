using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeLoader : MonoBehaviour
{
    public Animator animator;
    public int scene;

    public void reproduceCutscene(Cutscene cutscene)
    {
        CutsceneLoader.instance.SetCutscene(cutscene);
    }

    public void fadeExit(int ret)
    {
        scene = ret;
        animator.SetTrigger("FadeOut");
    }

    public void changeScene()
    {
        SceneManager.LoadScene(scene);
    }
}

