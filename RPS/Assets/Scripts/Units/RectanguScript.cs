using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectanguScript : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("RectIddle"))
        {
            animator.SetBool("red", false);
            animator.SetBool("blue", false);
            animator.SetBool("green", false);
        }
    }
}
