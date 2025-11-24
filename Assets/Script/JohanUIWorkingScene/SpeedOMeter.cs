using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedOMeter : MonoBehaviour
{
    public PlayerControl playerControl1/*, playerControl2*/;

    [SerializeField]
    private Image imageNeedle1/*, imageNeedle2*/;

    // Variables for controlling speed
    float currentSpeed = 0f;

    // speed of target
    float targetSpeed = 0f;

    //speed of needle
    float needleSpeed = 100.0f;

    // maxspeed of player
    float maxSpeed;



    private void Start()
    {
        // playerControl = FindFirstObjectByType<PlayerControl>();
    }
    
    void Update()
    {
        /*
        if (maxSpeed == 0)
         {
            maxSpeed = playerControl1.getState().getspeedMax();
            Debug.Log(maxSpeed);
         }
        targetSpeed = playerControl1.getState().getmoveSpeed();
        */

        /*
        if (maxSpeed == 0)
        {
            maxSpeed = playerControl2.getState().getspeedMax();
            Debug.Log(maxSpeed);
        }
        targetSpeed = playerControl2.getState().getmoveSpeed();
        */

        UpdateSpeed();
    }

    /* // used when calling from another Script
    
    public void SetSpeedSpeedOMeter(float amt)
    {

        // targetSpeed = playerControl.getState().getmoveSpeed();
        targetSpeed += amt;
    }
    */

    void UpdateSpeed()
    {
        // min speed
        if (targetSpeed > currentSpeed)
        {
            currentSpeed += Time.deltaTime * needleSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed, 0.0f, targetSpeed);
        }
        // max speed
        else if (targetSpeed < needleSpeed) 
        {
            currentSpeed -= Time.deltaTime * needleSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed, targetSpeed, maxSpeed);

        }
        SetMaxSpeed1();
        SetNeedle1();
        //SetNeedle2();
    }
    void SetNeedle1()
    {        
        //speedometer needle
        imageNeedle1.transform.localEulerAngles = new Vector3(0,0,(currentSpeed / maxSpeed) * -170 - 95);
    }
    /*
    void SetNeedle2()
    {
        //speedometer needle
        imageNeedle1.transform.localEulerAngles = new Vector3(0, 0, (currentSpeed / maxSpeed) * -170 - 95);
    }
    */
    void SetMaxSpeed1()
    {
        if (maxSpeed == 0)
        {
            maxSpeed = playerControl1.getState().getspeedMax();
            Debug.Log(maxSpeed);
        }
        targetSpeed = playerControl1.getState().getmoveSpeed();
    }
    /*
    void SetMaxSpeed2()
    {
        if (maxSpeed == 0)
        {
            maxSpeed = playerControl2.getState().getspeedMax();
            Debug.Log(maxSpeed);
        }
    }
    */
}