using TMPro;
using UnityEngine;

public class AnimParameters : MonoBehaviour
{
    [SerializeField]private PlayerControl control;
    [SerializeField]private Animator animator;
    //public TextMeshProUGUI text1;
    private void Start()
    {
        control = GetComponent<PlayerControl>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (animator != null)
        {
            bool swim = false;
            animator.SetFloat("Speed",control.getState().getmoveSpeed()/5);
            if (control.enumstate == PlayerControl.moveState.SWIM) swim = true;
            animator.SetBool("Swim", swim);
            animator.SetBool("isGrounded", !control.getState().isJumping());
        }
    }
}
