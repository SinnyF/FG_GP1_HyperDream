using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestControls1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0, speedMax = 10, jumpHeight = 10;
    float buttonNumber = 0;
    InputAction leftButton, rightButton;
    private bool leftPressed = false, rightPressed = false;
    [SerializeField] private bool side = false, canJump = true;
    public TextMeshProUGUI speedText, buttonNumberText;

    void Start()
    {
        leftButton = InputSystem.actions.FindAction("Button1");
        rightButton = InputSystem.actions.FindAction("Button2");

        if (leftButton == null) Debug.LogWarning("Button1 action not found");
        if (rightButton == null) Debug.LogWarning("Button2 action not found");

        // Track pressed state (for simultaneous detection)
        if (leftButton != null)
        {
            leftButton.started += ctx => leftPressed = true;
            leftButton.canceled += ctx => leftPressed = false;
            // Use performed to handle the "single button" gameplay logic (fires once per press)
            leftButton.performed += OnLeftPerformed;
            leftButton.Enable();
        }

        if (rightButton != null)
        {
            rightButton.started += ctx => rightPressed = true;
            rightButton.canceled += ctx => rightPressed = false;
            rightButton.performed += OnRightPerformed;
            rightButton.Enable();
        }
    }

    void OnDisable()
    {
        if (leftButton != null)
        {
            leftButton.started -= ctx => leftPressed = true;
            leftButton.canceled -= ctx => leftPressed = false;
            leftButton.performed -= OnLeftPerformed;
            leftButton.Disable();
        }

        if (rightButton != null)
        {
            rightButton.started -= ctx => rightPressed = true;
            rightButton.canceled -= ctx => rightPressed = false;
            rightButton.performed -= OnRightPerformed;
            rightButton.Disable();
        }
    }

    void Update()
    {
        buttonNumber = Mathf.Clamp(buttonNumber, 0, 10);
        moveSpeed = Mathf.Clamp(moveSpeed, 0, speedMax);

        speedText.text = "Speed: " + Mathf.RoundToInt(moveSpeed).ToString();
        buttonNumberText.text = "Button Presses: " + Mathf.RoundToInt(buttonNumber).ToString();

        moveSpeed -= moveSpeed * 1f * Time.deltaTime;
        buttonNumber -= buttonNumber * 1f * Time.deltaTime;

        // Reliable simultaneous detection: use pressed state tracked by started/canceled callbacks
        if (leftPressed && rightPressed && canJump)
        {
            Jump();
        }

        if (buttonNumber > 4 && buttonNumber < 6)
        {
            moveSpeed += speedMax;
        }
        if (buttonNumber > 6)
        {
            moveSpeed -= moveSpeed * 2f * Time.deltaTime;
        }
        transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
    }

    // called once per press from the InputAction.performed callback
    private void OnLeftPerformed(InputAction.CallbackContext ctx)
    {
        // original logic: if (side && leftButton.triggered) { ... }
        if (side)
        {
            moveSpeed += Mathf.Log10(speedMax);
            buttonNumber++;
            side = !side;
        }
    }

    // called once per press from the InputAction.performed callback
    private void OnRightPerformed(InputAction.CallbackContext ctx)
    {
        // original logic: if (!side && rightButton.triggered) { ... }
        if (!side)
        {
            moveSpeed += Mathf.Log10(speedMax);
            buttonNumber++;
            side = !side;
        }
    }

    public void Jump()
    {
        canJump = false;
        transform.Translate(new Vector3(0, jumpHeight * Time.deltaTime, 0));
        StartCoroutine(JumpCD());
    }

    IEnumerator JumpCD()
    {
        yield return new WaitForSeconds(1f);
        canJump = true;
    }

    public void TakeDamage()
    {
        moveSpeed /= 2;
    }
}
