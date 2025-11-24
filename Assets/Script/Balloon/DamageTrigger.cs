using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] float damage = 0.5f, timeout = 1f;
    private void OnTriggerEnter(Collider other)
    {
            other.GetComponent<MovementState>().Damage(damage, timeout);

    }
}
