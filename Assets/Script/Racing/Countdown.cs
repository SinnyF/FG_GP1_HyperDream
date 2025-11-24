using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float timer = 0;
    //[SerializeField] float speedMod = 1f;
    [SerializeField] TMP_Text countdownL, countdownR;
    [SerializeField] PlayerControl p1, p2;
    public GameObject image3,image2, image1, imageGo, P1Pos,P2Pos;
    private bool fadeOutImage3, fadeOutImage2, fadeOutImage1, fadeOutImageGo, p1PosfadeIn,p2PosFadeIn;
    private void Start()
    {
      // StartCoroutine(CountdownCoroutine());
    }
    private void FixedUpdate()
    {
        if (timer == 0)
        {
            //timer += Time.fixedDeltaTime;
        }
        timer += Time.fixedDeltaTime;
        /*else if (timer < 3)
        {
            timer += Time.fixedDeltaTime;
            int t = 3 - (int)timer;
            countdownL.text = t.ToString();
            countdownR.text = t.ToString();
            p1.isDisabled = true;
            p2.isDisabled = true;
        }
        else if(timer >= 3 && timer < 5)
        {
            timer += Time.fixedDeltaTime;
            countdownL.text = "GO!";
            countdownR.text = "GO!";
            p1.isDisabled = false;
            p2.isDisabled = false;
        }
        else
        {
            countdownL.text = string.Empty;
            countdownR.text = string.Empty;
        }*/
        if (fadeOutImage3)
        {
            foreach (var image in image3.GetComponentsInChildren<Image>())
            {
                image.color -= new Color(0, 0, 0, 0.02f);
            }
        }
        if (fadeOutImage2)
        {
            foreach (var image in image2.GetComponentsInChildren<Image>())
            {
                image.color -= new Color(0, 0, 0, 0.02f);
            }
        }
        if (fadeOutImage1)
        {
            foreach (var image in image1.GetComponentsInChildren<Image>())
            {
                image.color -= new Color(0, 0, 0, 0.02f);
            }
        }
        if(fadeOutImageGo)
        {
            foreach( var image in imageGo.GetComponentsInChildren<Image>())
            {
                image.color -= new Color(0, 0, 0, 0.01f);
            }
        }
        if (p1PosfadeIn)
        {
            foreach(var image in P1Pos.GetComponentsInChildren<Image>())
            {
                image.color += new Color(0, 0, 0, 0.05f);
            }
        }
        if (p2PosFadeIn)
        {
            foreach (var image in P2Pos.GetComponentsInChildren<Image>())
            {
                image.color += new Color(0, 0, 0, 0.05f);
            }
        }
    }
    public IEnumerator CountdownCoroutine()
    {
        p1.isDisabled = true;
        p2.isDisabled = true;
        image3.SetActive(true);
        fadeOutImage3 = true;
        yield return new WaitForSeconds(1f);
        image3.SetActive(false);
        image2.SetActive(true);
        fadeOutImage2 = true;
        yield return new WaitForSeconds(1f);
        image2.SetActive(false);
        image1.SetActive(true);
        fadeOutImage1 = true;
        yield return new WaitForSeconds(1f);
        image1.SetActive(false);
        imageGo.SetActive(true);
        fadeOutImageGo = true;
        p1.isDisabled = false;
        p2.isDisabled = false;
        GetComponent<HTPAnim>().startTimer = true;
        StartCoroutine(PosDelay());
        //imageGo.SetActive(false);
    }
    public IEnumerator PosDelay()
    {
        yield return new WaitForSeconds(1f);
        P1Pos.SetActive(true);
        p1PosfadeIn = true;
        P2Pos.SetActive(true);
        p2PosFadeIn = true;
    }
}
