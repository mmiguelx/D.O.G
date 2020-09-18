using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;
    public Animator animator3;
    public Animator animator4;
    public Animator animatorBoss;
    public Animator animatorMC;


    //For the proyect
    public void btnRed()
    {
        animator.SetTrigger("red");
        animator2.SetTrigger("red");
        animator3.SetTrigger("red");
        animator4.SetTrigger("red");
        animatorBoss.SetTrigger("red");
        animatorMC.SetTrigger("red");
    }
    public void btnBlue()
    {
        animator.SetTrigger("blue");
        animator2.SetTrigger("blue");
        animator3.SetTrigger("blue");
        animator4.SetTrigger("blue");
        animatorBoss.SetTrigger("blue");
        animatorMC.SetTrigger("blue");
    }
    public void btnGreen()
    {
        animator.SetTrigger("green");
        animator2.SetTrigger("green");
        animator3.SetTrigger("green");
        animator4.SetTrigger("green");
        animatorBoss.SetTrigger("green");
        animatorMC.SetTrigger("green");
    }
    public void btnAtt()
    {
        if (animator.GetBool("att") || animatorBoss.GetBool("att"))
        {
            animator.SetBool("att", false);
            animator2.SetBool("att", false);
            animator3.SetBool("att", false);
            animator4.SetBool("att", false);
            animatorBoss.SetBool("att", false);
            animatorMC.SetBool("att", false);
        } else
        {
            animator.SetBool("att", true);
            animator2.SetBool("att", true);
            animator3.SetBool("att", true);
            animator4.SetBool("att", true);
            animatorBoss.SetBool("att", true);
            animatorMC.SetBool("att", true);
        }
    }
    public void btnPhase()
    {
        if (animatorBoss.GetBool("phase"))
        {
            animatorBoss.SetBool("phase", false);
        }
        else
        {
            animatorBoss.SetBool("phase", true);
        }
    }
    public void btnChangePosition ()
    {
        Flip(animator);
        Flip(animator2);
        Flip(animator3);
        Flip(animator4);
        Flip(animatorBoss);
        Flip(animatorMC);
    }

    public void btnSwitch()
    {
        SwitchActive(animator);
        SwitchActive(animator2);
        SwitchActive(animator3);
        SwitchActive(animator4);
        SwitchActive(animatorBoss);
        SwitchActive(animatorMC);
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
    private void SwitchActive(Animator animator)
    {
        if (animator.gameObject.activeSelf)
        {
            animator.gameObject.SetActive(false);
        }
        else
        {
            animator.gameObject.SetActive(true);
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
