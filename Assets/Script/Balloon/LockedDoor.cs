using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BalloonMovement>().hasKey)
        {
            Destroy(gameObject);
        }
        else
        {
           // other.gameObject.GetComponent<BalloonMovement>().DoorLocked();
            Debug.Log("Door is locked. Find the key!");
        }
    }
}
