using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<BalloonMovement>().hasKey = true;
        Destroy(gameObject);
    }
}
