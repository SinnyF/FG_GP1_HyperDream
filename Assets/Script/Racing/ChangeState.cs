using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public PlayerControl.moveState moveState;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerControl>().changeState(moveState);
    }
}
