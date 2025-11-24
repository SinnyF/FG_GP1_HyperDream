using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour
{
    public PlayerControl playerControl;
    public Image imageLeft, imageRight;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetBottunGlowp1();
    }

    public void SetBottunGlowp1()
    {
        if (playerControl.getState().getSide())
        {
            var tempColor = imageRight.color;
            var tempColor2 = imageLeft.color;
            tempColor2.a = 1f;
            tempColor.a = 0.3f;
            imageRight.color = tempColor;
            imageLeft.color = tempColor2;
        }
        else 
        {
            var tempColor = imageLeft.color;
            var tempColor2 = imageLeft.color;
            tempColor.a = 0.3f;
            tempColor2.a = 1f;
            imageLeft.color = tempColor;
            imageRight.color = tempColor2;
        }
        
    }
    /*public void SetBottunGlowp2()
    {
        if (playerControl.getState().getSide())
        {
            var tempColor = imageRight.color;
            tempColor.a = 0f;
            imageRight.color = tempColor;
        }
        else
        {
            var tempColor = imageLeft.color;
            tempColor.a = 0f;
            imageLeft.color = tempColor;
        }
    }*/
}
