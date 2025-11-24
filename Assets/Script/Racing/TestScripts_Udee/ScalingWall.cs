using System.Collections;
using System.Threading;
using UnityEngine;

public class ScalingWall : MonoBehaviour
{
    [SerializeField]private bool moveWall,startMove, initObstacle;
    [SerializeField] private float scaleSpeed = 2f, wallCD = 2f, startWait = 1f;
    void Start()
    {
      startMove = true;
      StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (initObstacle)
        {
            if (startMove)
            {
                StartCoroutine(WallSwap());
            }
            if (moveWall)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(gameObject.transform.localScale.x, 3f, gameObject.transform.localScale.z), scaleSpeed * Time.deltaTime);
            }
            else if (!moveWall)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(gameObject.transform.localScale.x, 1f, gameObject.transform.localScale.z), scaleSpeed * Time.deltaTime);
            }
        }
    }
    IEnumerator WallSwap()
    {
        moveWall = !moveWall;
        startMove = false;
        yield return new WaitForSeconds(wallCD);
        startMove = true;
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startWait);
        initObstacle = true;
    }
}
