using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(startPos, transform.position) > 5f)
        {
            moveSpeed = -moveSpeed;
        }
    }
}
