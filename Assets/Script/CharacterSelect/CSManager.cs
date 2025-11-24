using UnityEngine;
using UnityEngine.SceneManagement;

public class CSManager : MonoBehaviour
{
    [SerializeField] ChSelect p1, p2;
    [SerializeField] int nextScene;
    [SerializeField] GameObject pers;

    private void Awake()
    {
        var p = FindAnyObjectByType<Persistent>();

        if( p != null )
            DestroyImmediate( p.gameObject );

        Instantiate( pers );
    }
    private void Update()
    {
        if(p1.selected && p2.selected)
        {
            FindFirstObjectByType<Persistent>().p1 = p1.GetCurrentModel();
            FindFirstObjectByType<Persistent>().p2 = p2.GetCurrentModel();
            SceneManager.LoadScene(nextScene);
        }

        
    }
}
