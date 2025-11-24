
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using static PlayerControl;

public class ChSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> models;
    [SerializeField] GameObject pivot, empty;
    List<GameObject> showpiece;

    public enum player
    {
        ONE,
        TWO
    }
    [SerializeField] player enumplayer;
    InputAction leftButton, rightButton;
    PlayerInput pInput;
    public bool selected = false;
    int index = 0;
    float targetRot = 0, curRot = 0;
    [SerializeField] GameObject tick;

    public GameObject GetCurrentModel()
    {
        return models[index];
    }

    private void Start()
    {
        pInput = GetComponent<PlayerInput>();
        showpiece = new List<GameObject>();
        foreach (GameObject model in models)
        {
            showpiece.Add(Instantiate(empty, pivot.transform));
            showpiece[showpiece.Count - 1].transform.SetPositionAndRotation(pivot.transform.position + new Vector3(0,0,3), Quaternion.identity);
            Instantiate(model, showpiece[showpiece.Count - 1].transform);
            pivot.transform.SetPositionAndRotation(pivot.transform.position, Quaternion.Euler(0, (360f /models.Count) * showpiece.Count, 0));
        }
        if (Gamepad.all.Count < 1)
        {
            if (enumplayer == player.ONE)
            {
                leftButton = pInput.actions.FindAction("B1");
                rightButton = pInput.actions.FindAction("B2");
            }
            else
            {
                leftButton = pInput.actions.FindAction("P2B1");
                rightButton = pInput.actions.FindAction("P2B2");
            }
        }
        else if (Gamepad.all.Count == 1)
        {
            if (enumplayer == player.ONE)
            {
                pInput.user.UnpairDevices();
                pInput.actions.devices = new InputDevice[] { Gamepad.all[0] };
                leftButton = pInput.actions.FindAction("B1");
                rightButton = pInput.actions.FindAction("B2");
            }
            else
            {
                leftButton = pInput.actions.FindAction("P2B1");
                rightButton = pInput.actions.FindAction("P2B2");
            }
        }
        else if (Gamepad.all.Count > 1)
        {
            if (enumplayer == player.ONE)
            {
                pInput.user.UnpairDevices();
                pInput.actions.devices = new InputDevice[] { Gamepad.all[0] };
                leftButton = pInput.actions.FindAction("B1");
                rightButton = pInput.actions.FindAction("B2");
            }
            else
            {
                pInput.user.UnpairDevices();
                pInput.actions.devices = new InputDevice[] { Gamepad.all[1] };
                leftButton = pInput.actions.FindAction("B1");
                rightButton = pInput.actions.FindAction("B2");
            }
        }
    }

    private void Update()
    {
        
        tick.SetActive(selected);

        if (rightButton.triggered && !selected)
        {
            index++;
            if (index > showpiece.Count - 1)
                index = 0;
        }

        if (leftButton.triggered)
        {
            selected = !selected;
        }

        targetRot = (360f / showpiece.Count) * index;
        //Debug.Log(index + "||" + targetRot + "||" + curRot);
        if (curRot < targetRot || (targetRot == 0 && curRot > 3))
        {
            curRot += 180f * Time.deltaTime;
            pivot.transform.rotation = Quaternion.Euler(0, curRot, 0);
        }
        else
        {
            pivot.transform.rotation = Quaternion.Euler(0,targetRot,0);
        }
            
        if(curRot > 360) 
            curRot = 0;

        foreach(GameObject model in showpiece)
        {
            model.transform.rotation = Quaternion.identity;
        }

        
    }
}
