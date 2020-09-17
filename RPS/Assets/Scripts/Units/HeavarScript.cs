using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavarScript : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("HeaIddle"))
        {
            animator.SetBool("red", false);
            animator.SetBool("blue", false);
            animator.SetBool("green", false);
        }
    }
}
