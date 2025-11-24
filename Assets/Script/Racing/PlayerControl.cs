using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    public enum moveState
    {
        RUN,
        SWIM
    }
    public enum player
    {
        ONE,
        TWO
    }
    [SerializeField] List<GameObject> fallback;
    [SerializeField] FGSpline spline;
    [SerializeField] ParticleSystem dust;
    public ParticleSystem landingDust;
    public moveState enumstate;
    [SerializeField] player enumplayer;
    [SerializeField] int particleThreshold = 5, stepCount = 0;
    [SerializeField] float speedMax = 10, speedDecay = 1f, countTime = 1f, jumpHeight = 3, jumpSpeed = 2f, speedMod = 10f, jumpDelay = 0.1f;
    public bool isDisabled = false;
    //public bool easyMode;
    MovementState state = null;
    Transform move;
    InputAction leftButton, rightButton;
    PlayerInput pInput;
    [SerializeField] GameObject playerModel;

    void Awake()
    {
        var pers = FindFirstObjectByType<Persistent>();
        if(pers != null)
        {
            if (enumplayer == player.ONE)
            {
                playerModel = pers.p1;
            }
            else
            {
                playerModel = pers.p2;
            }
        }
        if(playerModel == null)
        {
            if(enumplayer == player.ONE)
            {
                playerModel = Instantiate(fallback[0], transform);
                playerModel.transform.localPosition += new Vector3(0,-1.1f, 0);
            }
            else
            {
                playerModel = Instantiate(fallback[1], transform);
                playerModel.transform.localPosition += new Vector3(0, -1.1f, 0);
            }
        }
        else
        {
            playerModel = Instantiate(playerModel, transform);
            playerModel.transform.localPosition += new Vector3(0, -1.1f, 0);
        }
        pInput = GetComponent<PlayerInput>();
        dust = GetComponentInChildren<ParticleSystem>();
        move = this.transform;
        switch (enumstate)
        {
            case moveState.RUN:
                state = gameObject.AddComponent<Run>();
                state.SetValues(speedDecay, countTime, speedMax, jumpDelay, jumpHeight, jumpSpeed, speedMod);
                break;
            case moveState.SWIM:
                state = gameObject.AddComponent<Swim>();
                state.SetValues(speedDecay, countTime, speedMax, jumpDelay, jumpHeight, jumpSpeed, speedMod);
                break;
            default:
                break;
        }

        //Set controls depending on how many gamepads are connected and which player the script is assigned to
        if(Gamepad.all.Count < 1)
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

    public void changeState(PlayerControl.moveState mstate)
    {
        if(enumstate != mstate)
        {
            switch (mstate)
            {
                case moveState.RUN:
                    state = gameObject.AddComponent<Run>();
                    state.CopyValues(GetComponent<Swim>());
                    enumstate = mstate;
                    break;
                case moveState.SWIM:
                    state = gameObject.AddComponent<Swim>();
                    state.CopyValues(GetComponent<Run>());
                    enumstate = mstate;
                    break;
                default:
                    break;
            }
            switch (enumstate)
            {
                case moveState.RUN:
                    Destroy(GetComponent<Swim>());
                    break;
                case moveState.SWIM:
                    Destroy(GetComponent<Run>());
                    break;
                default:
                    break;
            }
        }
    }

    public MovementState getState() { return state; }
    
    void Update()
    {
        var emission = dust.emission;
        if (!isDisabled)
            state.handleInput(ref move, leftButton, rightButton, ref spline);
        if(state.getmoveSpeed() > particleThreshold) emission.enabled = true;
        else emission.enabled = false;
    }
}

public abstract class MovementState : MonoBehaviour
{
    protected float speedDecay, rythmDecay, speedMax, path = 0, height = 0, timer = 0, delay = 0.1f,
                    jumpHeight, jumpSpeed, speedMod;

    [SerializeField] protected float moveSpeed = Mathf.Epsilon;
    [SerializeField] protected bool side = false;
    protected bool jumping = false, up = true, disable = false;
    public bool easyMode = false;
    [SerializeField] protected int stepCount;
    protected float catchup = 1f;
    protected IEnumerator cr;

    public void SetValues(float speedDecay, float rythmDecay, float speedMax, float delay, float jumpHeight, float jumpSpeed, float speedMod)
    {
        this.speedDecay = speedDecay;
        this.rythmDecay = rythmDecay;
        this.speedMax = speedMax;
        this.delay = delay;
        this.jumpHeight = jumpHeight;
        this.jumpSpeed = jumpSpeed;
        this.speedMod = speedMod;
    }

    public void CopyValues(MovementState copyFrom)
    {
        this.speedDecay = copyFrom.speedDecay;
        this.rythmDecay = copyFrom.rythmDecay;
        this.speedMax = copyFrom.speedMax;
        this.delay =    copyFrom.delay;
        this.jumpHeight = copyFrom.jumpHeight;
        this.jumpSpeed = copyFrom.jumpSpeed;
        this.speedMod = copyFrom.speedMod;
        this.easyMode = copyFrom.easyMode;
    }

    public bool getSide() { return side; }
    public float getmoveSpeed() { return moveSpeed; }
    public float getspeedMax() { return speedMax; }
    public float getAveragePresses() { return stepCount; }

    public void setCatchup(float set) { catchup = set; }

    public bool isJumping() { return jumping; }
    public bool isUp() { return up; }

    public abstract void handleInput(ref Transform move, InputAction leftButton, InputAction rightButton, ref FGSpline spline);

    protected IEnumerator CountDecay(float time)
    {
        yield return new WaitForSeconds(time);
        DecreaseCount();
    }

    protected void DecreaseCount() { if(stepCount > 0) stepCount--; }

    public void Damage(float damage, float timeout) 
    {
        disable = true;
        moveSpeed = moveSpeed * damage;
        cr = enable(timeout);
        StartCoroutine(cr);
    }

    public IEnumerator enable(float time)
    {
        yield return new WaitForSeconds(time);
        disable = false;
    }
}

public class Run : MovementState
{
    Vector3 offset = Vector3.zero;

    public override void handleInput(ref Transform move, InputAction leftButton, InputAction rightButton, ref FGSpline spline)
    {
        if (disable)
        {
            moveSpeed -= moveSpeed * speedDecay * Time.deltaTime;
        }
        else if (!jumping)
        {
            offset = Vector3.zero;
            moveSpeed -= moveSpeed * speedDecay * Time.deltaTime;
            if (moveSpeed < 0.01f)
            {
                moveSpeed = 0f;
            }
            //Running and jumping conditions
            if (leftButton.IsPressed() && rightButton.IsPressed())
            {
                timer += Time.deltaTime;
                if(timer > delay && up)
                {
                    jumping = true;
                    if (moveSpeed < speedMax * 0.4f)
                        moveSpeed = speedMax * 0.4f;
                    else
                        moveSpeed += moveSpeed * 0.2f;
                    timer = 0;
                }
            }
            else
            {
                
                timer = 0;
            }

            if((side && leftButton.triggered) || (!side && rightButton.triggered))
            {
                stepCount++;
                if (!easyMode)
                {
                    cr = CountDecay(rythmDecay);
                }
                else
                {
                    cr = CountDecay(rythmDecay + 2f);
                }
                StartCoroutine(cr);
                side = !side;
                //Speed logic depending on rythm
                if (stepCount < 4 && stepCount > 0)
                {
                    moveSpeed += Mathf.Log10(speedMax / 2) * speedMod * catchup;
                }
                else if (stepCount >= 5 && stepCount <= 7)
                {
                    moveSpeed += Mathf.Log10(speedMax) * speedMod * catchup;
                }
                else if (stepCount > 7)
                {
                    if (!easyMode)
                    {
                        moveSpeed += Mathf.Log10(speedMax * 2) * speedMod * catchup;
                    }
                    else
                    {
                        moveSpeed += Mathf.Log10(speedMax * 2)* 2f * speedMod * catchup;
                    }
                }

            }

        }
        else
        {
            if (up)
            {
                path += Mathf.Sin(jumpSpeed * Time.deltaTime );
                if (path >= 1)
                    up = false;
            }
            else
            {
                path -= Mathf.Sin(jumpSpeed * Time.deltaTime);
                if (path <= 0)
                {
                    jumping = false;
                    path = 0;

                    // Trigger landing dust
                    var player = GetComponent<PlayerControl>();
                    if (player != null && player.landingDust != null)
                    {
                        player.landingDust.Play();
                    }

                    cr = cooldown(0.5f);
                    StartCoroutine(cr);
                }

            }
            height = Mathf.Lerp(0,jumpHeight,path);
            if(height < 0)
                height = 0;
            offset.y = height;
        }

        moveSpeed = Mathf.Min(moveSpeed, speedMax);

        if (disable)
        {
            GetComponentInChildren<Animator>().transform.Rotate(new Vector3(0, 720f * Time.deltaTime, 0));
            spline.FollowSplineConstant(moveSpeed * Time.deltaTime);
            move.SetPositionAndRotation(spline.getPoint().position + offset, transform.rotation);
        }
        else if(spline != null)
        {
            GetComponentInChildren<Animator>().transform.localRotation = Quaternion.identity;
            spline.FollowSplineConstant(moveSpeed * Time.deltaTime);
            move.SetPositionAndRotation(spline.getPoint().position + offset, spline.getPoint().rotation);
        }
        else
            move.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime) + offset);
    }
    public IEnumerator cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        up = true;
    }

}

public class Swim : MovementState
{
    Vector3 offset = Vector3.zero;
    
    public override void handleInput(ref Transform move, InputAction leftButton, InputAction rightButton, ref FGSpline spline)
    {
        if (disable)
        {
            moveSpeed -= moveSpeed * speedDecay * Time.deltaTime;
        }
        else if (!jumping)
        {
            offset = Vector3.zero;
            moveSpeed -= moveSpeed * speedDecay * Time.deltaTime;
            if (moveSpeed < 0.05f)
            {
                moveSpeed = 0f;
            }
            //Running and jumping conditions
            if (leftButton.IsPressed() && rightButton.IsPressed())
            {
                timer += Time.deltaTime;
                if (timer > delay && up)
                {
                    jumping = true;
                    if (moveSpeed < speedMax * 0.4f)
                        moveSpeed = speedMax * 0.4f;
                    else
                        moveSpeed += moveSpeed * 0.2f;
                    timer = 0;
                }
            }
            else
            {
                timer = 0;
            }

            if ((side && leftButton.triggered) || (!side && rightButton.triggered))
            {
                stepCount++;
                if (!easyMode)
                {
                    cr = CountDecay(rythmDecay); 
                }
                else
                {
                    cr = CountDecay(rythmDecay+0.5f);
                }

                StartCoroutine(cr);
                side = !side;

                if (stepCount < 4 && stepCount > 0)
                {
                    moveSpeed += Mathf.Log10(speedMax / 2) * speedMod * catchup;
                }
                else if (stepCount >= 4 && stepCount <= 8)
                {
                    if (!easyMode)
                    {
                        moveSpeed += Mathf.Log10(speedMax * 2) * 1.75f * speedMod * catchup;
                    }
                    else
                    {
                        moveSpeed += Mathf.Log10(speedMax * 2) * 2f * speedMod * catchup;
                    }
                }
                else if (stepCount > 8)
                {
                    if (!easyMode)
                    {
                        moveSpeed += Mathf.Log10(speedMax / 8) * speedMod * catchup;
                    }
                    else
                    {
                        moveSpeed += Mathf.Log10(speedMax / 4) * speedMod * catchup;
                    }
                }
            }
        }
        else
        {
            if (up && path < 1)
            {
                path += Mathf.Sin(jumpSpeed * Time.deltaTime);
                if (path >= 1)
                {
                    cr = cooldownF(0.1f);
                    StartCoroutine(cr);
                }
            }
            else
            {
                path -= Mathf.Sin(jumpSpeed * Time.deltaTime);
                if (path <= 0)
                {
                    jumping = false;
                    path = 0;
                    cr = cooldownT(0.5f);
                    StartCoroutine(cr);
                }
            }
            height = Mathf.Lerp(0, -jumpHeight, path);
            if (height > 0)
                height = 0;
            offset.y = height;
        }
        if (disable)
        {
            GetComponentInChildren<Animator>().transform.Rotate(new Vector3(0, 720f * Time.deltaTime, 0));
            spline.FollowSplineConstant(moveSpeed * Time.deltaTime);
            move.SetPositionAndRotation(spline.getPoint().position + offset, transform.rotation);
        }
        else if (spline != null)
        {
            GetComponentInChildren<Animator>().transform.localRotation = Quaternion.identity;
            spline.FollowSplineConstant(moveSpeed * Time.deltaTime);
            move.SetPositionAndRotation(spline.getPoint().position + offset, spline.getPoint().rotation);
        }
        else
            move.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
    }
    public IEnumerator cooldownT(float time)
    {
        yield return new WaitForSeconds(time);
        up = true;
    }

    public IEnumerator cooldownF(float time)
    {
        yield return new WaitForSeconds(time);
        up = false;
    }
}