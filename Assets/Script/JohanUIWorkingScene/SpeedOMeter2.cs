using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedOMeter2 : MonoBehaviour
{
    public PlayerControl playerControl1, playerControl2;

    [SerializeField]
    private Image imageNeedle1, imageNeedle2;

    // Variables for controlling speed
    float currentSpeedL = 0f, currentSpeedR = 0f;

    // speed of target
    float targetSpeedL = 0f, targetSpeedR = 0f;

    //speed of needle
    float needleSpeedL = 100.0f, needleSpeedR = 100;

    // maxspeed of player
    float maxSpeedL, maxSpeedR;

    void Update()
    {
        UpdateSpeed1();
        UpdateSpeed2();

        SetMaxSpeed1();
        SetNeedle1();

        SetMaxSpeed2();
        SetNeedle2();
    }

    void UpdateSpeed1()
    {
        // min speed
        if (targetSpeedL > currentSpeedL)
        {
            currentSpeedL += Time.deltaTime * needleSpeedL;
            currentSpeedL = Mathf.Clamp(currentSpeedL, 0.0f, targetSpeedL);
        }
        // max speed
        else if (targetSpeedL < needleSpeedL)
        {
            currentSpeedL -= Time.deltaTime * needleSpeedL;
            currentSpeedL = Mathf.Clamp(currentSpeedL, targetSpeedL, maxSpeedL);
        }        
    }
    void UpdateSpeed2()
    {
        // min speed
        if (targetSpeedR > currentSpeedR)
        {
            currentSpeedR += Time.deltaTime * needleSpeedR;
            currentSpeedR = Mathf.Clamp(currentSpeedR, 0.0f, targetSpeedR);
        }
        // max speed
        else if (targetSpeedR < needleSpeedR)
        {
            currentSpeedR -= Time.deltaTime * needleSpeedR;
            currentSpeedR = Mathf.Clamp(currentSpeedR, targetSpeedR, maxSpeedR);
        }
    }
    
    void SetNeedle1()
    {
        //speedometer needle
        imageNeedle1.transform.localEulerAngles = new Vector3(0, 0, (currentSpeedL / maxSpeedL) * 170 - 95);
    }
    
    void SetNeedle2()
    {
        //speedometer needle
        imageNeedle2.transform.localEulerAngles = new Vector3(0, 0, (currentSpeedR / maxSpeedR) * -170 - 95);
    }
    
    void SetMaxSpeed1()
    {
        if (maxSpeedL == 0)
        {
            maxSpeedL = playerControl1.getState().getspeedMax();
        }
        targetSpeedL = playerControl1.getState().getmoveSpeed();        
    }
    void SetMaxSpeed2()
    {
        if (maxSpeedR == 0)
        {
            maxSpeedR = playerControl2.getState().getspeedMax();
        }
        targetSpeedR = playerControl2.getState().getmoveSpeed();
    }
}