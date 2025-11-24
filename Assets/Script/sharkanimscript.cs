using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public Animator animator;
    public string triggerName = "Play";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            animator.SetTrigger(triggerName);
    }
}