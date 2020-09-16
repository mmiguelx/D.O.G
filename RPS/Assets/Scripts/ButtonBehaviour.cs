using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public Animator animator;
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
