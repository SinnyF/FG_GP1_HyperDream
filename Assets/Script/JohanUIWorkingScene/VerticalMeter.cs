using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VerticalMeter : MonoBehaviour
{
    public PlayerControl playerControl1, playerControl2;

    public Slider slider,slider2;
    public Gradient player1gradient1,player1gradient2/*, player2gradient1, player2gradient2*/;
    public Image fill1,fill2;
    //public Transform runPosL,swimPosL,runPosR, swimPosR;
    public Image indicatorBarL, indicatorBarR;
    public GameObject arrowL, arrowR;
    //public float animSpeed;
    //[SerializeField]private bool runToSwimL = false,swimToRunL=false,runToSwimR = false, swimToRunR = false;

   /* [SerializeField] private TMP_ColorGradient colorGradientRun;
    [SerializeField] private TMP_ColorGradient colorGradientSwim;*/

    float avgPresses1, avgPresses2 = 0;
    //float maxPresses;
    //public float testVar = 0;

    private void Start()
    {
        //playerControl = FindFirstObjectByType<PlayerControl>();
    }
    private void Update()
    {
        avgPresses1 = playerControl1.getState().getAveragePresses();
        SetSpeedVerticalMeter1(avgPresses1);
        avgPresses2 = playerControl2.getState().getAveragePresses();
        SetSpeedVerticalMeter2(avgPresses2);
      /* if(runToSwimR)
        {
            arrowR.transform.position = Vector3.Lerp(runPosR.position, swimPosR.position, Time.deltaTime * animSpeed);
        }
        else
        {
            arrowR.transform.position = Vector3.Lerp(swimPosR.position, runPosR.position, Time.deltaTime * animSpeed);
        }*/
    }
    public void SetMaxSpeed(float speed)
    {
        slider.maxValue = speed;        
    }

    public void SetSpeedVerticalMeter1(float speed/*, float maxSpeed*/)
    {
       // avgPresses = slider.value;
        slider.value = speed;

        /*
        slider.maxValue = avgPresses;
        slider.maxValue = maxSpeed;
        */

        if (playerControl1.enumstate == PlayerControl.moveState.RUN)
        {
            fill1.color = player1gradient1.Evaluate(slider.normalizedValue);
            indicatorBarL.gameObject.SetActive(false);
            arrowL.gameObject.SetActive(true);
        }
        else
        {
            fill1.color = player1gradient2.Evaluate(slider.normalizedValue);
            indicatorBarL.gameObject.SetActive(true);
            arrowL.gameObject.SetActive(false);
        }
    }
    public void SetSpeedVerticalMeter2(float speed/*, float maxSpeed*/)
    {
        // avgPresses = slider.value;
        slider2.value = speed;

        /*
        slider.maxValue = avgPresses;
        slider.maxValue = maxSpeed;
        */

        if (playerControl2.enumstate == PlayerControl.moveState.RUN)
        {
            fill2.color = player1gradient1.Evaluate(slider2.normalizedValue);
            indicatorBarR.gameObject.SetActive(false);
           // swimToRunR = true;
            //runToSwimR = false;
            arrowR.gameObject.SetActive(true);
        }
        else
        {
            fill2.color = player1gradient2.Evaluate(slider2.normalizedValue);
            indicatorBarR.gameObject.SetActive(true);
           /// runToSwimR = true;
           // swimToRunR = false;
            arrowR.gameObject.SetActive(false);
        }
    }

}
