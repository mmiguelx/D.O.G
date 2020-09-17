using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;
  

    //For the proyect
    public void btnRed()
    {
        animator.SetBool("red", true);
        animator2.SetBool("red", true);
    }
    public void btnBlue()
    {
        animator.SetBool("blue", true);
        animator2.SetBool("blue", true);
    }
    public void btnGreen()
    {
        animator.SetBool("green", true);
        animator2.SetBool("green", true);
    }
    public void btnAtt()
    {
        if (animator.GetBool("att"))
        {
            animator.SetBool("att", false);
            animator2.SetBool("att", false);
        } else
        {
            animator.SetBool("att", true);
            animator2.SetBool("att", true);
        }
    }




    //exclusive for Megaman
    public void btnSpawnPress()
    {
        animator.SetBool("spawn", true);
    }
    public void btnJumpPress()
    {
        animator.SetBool("air", true);
    }
    public void btnRunPress()
    {
        animator.SetFloat("speed", 1f);
    }
}
