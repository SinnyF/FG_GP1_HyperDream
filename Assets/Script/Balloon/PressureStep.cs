using UnityEngine;

public class PressureStep : MonoBehaviour
{
    [SerializeField]private float timer = 0f;
    public GameObject Door;
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<BalloonMovement>())
        {
            timer += Time.deltaTime;
            if(timer >= 2f)
            {
                Destroy(Door);
            }
        }
        else {             
            timer = 0f;
        }
    }
}
