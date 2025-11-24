using System.Collections;
using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    //Coroutine coroutine;
    [SerializeField] private float timer = 1F;
    public HTPAnim htpAnim;
    public PercentageTrackleft ptl;
    [SerializeField] TMP_Text winner;
    private void Start()
    {
        htpAnim = FindFirstObjectByType<HTPAnim>();
        ptl = FindFirstObjectByType<PercentageTrackleft>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winner.text = "WINNER";
            Time.timeScale = 0.1f;
        }
        else
        {
            winner.text = "LOSER";
            Time.timeScale = 0.1f;
        }
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
       htpAnim.finishImg.SetActive(true);
        yield return new WaitForSecondsRealtime(timer);
        Time.timeScale = 1f;
        htpAnim.InitFinishUI(ptl.p1Wins);
    }

    

}
