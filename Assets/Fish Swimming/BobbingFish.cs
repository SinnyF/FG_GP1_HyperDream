using UnityEngine;

public class BobbingFish : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;

    }

    private void Update()
    {

        TransformObject();

    }

    public void TransformObject()
    {
        transform.localPosition = new Vector3(startPos.x, (startPos.y + Mathf.Sin(Time.time) + 1), startPos.z);

    }
}

