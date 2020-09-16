using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public BattleLoader bl;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(executeIntro());
    }

    IEnumerator executeIntro()
    {
        yield return new WaitForSeconds(3f);
        bl.fadeExit(1);
    }
}
