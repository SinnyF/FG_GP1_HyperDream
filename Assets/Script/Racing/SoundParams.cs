using UnityEngine;

public class SoundParams : MonoBehaviour
{
    [SerializeField]AudioSource stepSound;
    [SerializeField]AudioSource jumpSound;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void StepSound()
    {
        float speed = animator.GetFloat("Speed");
        if (speed >= 0.1)
        {
            stepSound.Play();
        }
    }

    public void JumpSound()
    {
        jumpSound.Play();
    }
}
