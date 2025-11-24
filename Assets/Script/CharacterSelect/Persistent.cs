using UnityEngine;

public class Persistent : MonoBehaviour
{
    public GameObject p1, p2;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
