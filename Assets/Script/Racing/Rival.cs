using UnityEngine;

public class Rival : MonoBehaviour
{
//TEST
    float speed = 1f, speedMax = 10f;
    int count = 0;

    void FixedUpdate()
    {
        speed -= speed * 1f * Time.fixedDeltaTime;
        count++;
        if (count > 3 )
        {
            if (Random.Range(0, 100) > 50)
            {
                speed += Mathf.Log10(speedMax);
            }
            count = 0;
        }
        
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }
}
