using UnityEngine;

public class NewMonoBehaviourScript1 : MonoBehaviour
{
    private float rotationSpeed = 10.0f;
    
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
        //transform.Rotate(new Vector3(1, 1, 0) * Time.deltaTime * rotationSpeed, Space.Self);
        transform.localPosition = new Vector3(startPos.x, (startPos.y + Mathf.Sin(Time.time) + 1) * 0.25f, startPos.z);
    }
}

