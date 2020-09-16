using UnityEngine;

public class megamanMovement : MonoBehaviour
{
    public Animator animator;
     void Start()
    {
        //animator.SetBool("spawn", false);
    }
    void Update()
    {

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("MegaSpawn_loop"))
        {
            animator.SetBool("spawn", false);
        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("MegaJump_loop"))
        {
            animator.SetBool("air", false);
        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("MegaRun"))
        {
            animator.SetFloat("speed", 0f);
        }

    }
}
