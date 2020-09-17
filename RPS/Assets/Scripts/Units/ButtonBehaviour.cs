using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;
    public Animator animator3;
    public Animator animator4;


    //For the proyect
    public void btnRed()
    {
        animator.SetBool("red", true);
        animator2.SetBool("red", true);
        animator3.SetBool("red", true);
        animator4.SetBool("red", true);
    }
    public void btnBlue()
    {
        animator.SetBool("blue", true);
        animator2.SetBool("blue", true);
        animator3.SetBool("blue", true);
        animator4.SetBool("blue", true);
    }
    public void btnGreen()
    {
        animator.SetBool("green", true);
        animator2.SetBool("green", true);
        animator3.SetBool("green", true);
        animator4.SetBool("green", true);
    }
    public void btnAtt()
    {
        if (animator.GetBool("att"))
        {
            animator.SetBool("att", false);
            animator2.SetBool("att", false);
            animator3.SetBool("att", false);
            animator4.SetBool("att", false);
        } else
        {
            animator.SetBool("att", true);
            animator2.SetBool("att", true);
            animator3.SetBool("att", true);
            animator4.SetBool("att", true);
        }
    }
    public void btnChangePosition ()
    {
        Flip(animator);
        Flip(animator2);
        Flip(animator3);
        Flip(animator4);
    }
    private void Flip(Animator animator)
    {
        Vector3 theScale = animator.transform.localScale;
        if (animator.transform.localScale.x > 0)
        {
            theScale.x = -1;
            animator.transform.localScale = theScale;
        } else
        {
            theScale.x = 1;
            animator.transform.localScale = theScale;
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
