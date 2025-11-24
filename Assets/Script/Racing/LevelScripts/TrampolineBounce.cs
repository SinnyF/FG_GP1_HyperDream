using UnityEngine;
using System.Collections;

public class TrampolineBounce : MonoBehaviour
{
    public float bounceHeight = 3f;  
    public float bounceSpeed = 5f;   
    [SerializeField]private bool isBouncing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isBouncing)
        {
            isBouncing = true;
            StartCoroutine(Bounce(other.transform));
        }
    }

    IEnumerator Bounce(Transform player)
    {
        float startY = player.position.y;
        float targetY = startY + bounceHeight;


        while (player.position.y < targetY)
        {
            player.position += Vector3.up * bounceSpeed * Time.deltaTime;
            yield return null;
        }


       while (player.position.y > startY)
        {
            player.position -= Vector3.up * bounceSpeed * Time.deltaTime;
            yield return null;
        }

    
        player.position = new Vector3(player.position.x, startY, player.position.z);
        isBouncing = false;
    }
}