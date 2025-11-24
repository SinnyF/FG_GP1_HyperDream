using System.Collections;
using TMPro;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    public float speed = 2f,pumpSpeed=5f,deflateSpeed=5f;
    [SerializeField] private int health = 2;
    [SerializeField]private float air = 100f;
    public TextMeshProUGUI airText,inflateState,healthText;
    [SerializeField] private bool isInflating = false;
    [SerializeField] private bool canInflate = true;
    [SerializeField] private bool inflateMode = false;
    public bool hasKey = false;
    [SerializeField] private bool canMove = true;
    public GameObject winUI;

    void Update()
    {
        //Movement Controls 
       // Debug.Log("!");
        if (canMove && Input.GetKey(KeyCode.W) && air>0)
        {
            transform.Translate(Vector3.up* speed * Time.deltaTime);
            air -= deflateSpeed * Time.deltaTime;
        }
        if (canMove && Input.GetKey(KeyCode.S) && air > 0)
        {           
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            air -= deflateSpeed * Time.deltaTime;
        }
        if (canMove && Input.GetKey(KeyCode.D) && air > 0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            air -= deflateSpeed * Time.deltaTime;
        }
        if (canMove && Input.GetKey(KeyCode.A) && air > 0)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            air -= deflateSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Space) && air > 0 && !inflateMode && canInflate)
        {
            canMove = false;
            canInflate = false;
            inflateMode = true;
            StartCoroutine(InflateModeCD());
        }
        if(inflateMode && Input.GetKeyDown(KeyCode.Space) && !isInflating)
        { 
            isInflating = true;
            transform.Translate(Vector3.up * pumpSpeed * Time.deltaTime);
            air += 5f;
            StartCoroutine(InflateCD());
        }
        if(canInflate)
        {
            inflateState.text = "Inflate: Ready";
        }
        else if(inflateMode)
        {
            inflateState.text = "Inflate: Mode Active";
        }
        else
        {
            inflateState.text = "Inflate: On CoolDown";
        }
        transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
        air -= 2f * Time.deltaTime;
        airText.text = "Air: " + Mathf.RoundToInt(air).ToString();
        air = Mathf.Clamp(air, 0f, 100f);
        healthText.text = "Health: " + health.ToString();
    }

    public IEnumerator InflateCD()
    {
        yield return new WaitForSeconds(0.2f);
        isInflating = false;
    }
    public IEnumerator InflateModeCD() 
    {
        yield return new WaitForSeconds(2f);
        canMove = true;
        inflateMode = false;
        yield return new WaitForSeconds(3f);
        canInflate = true;
    }
    public void TakeDamage()
    {
        health -= 1;
        if(health <= 0)
        {
            healthText.text = "Health: 0";
            gameObject.SetActive(false);
        }
    }
    public void KeyCollected()
    {
        hasKey = true;
    }
    public void DoorOpen()
    {
        if(hasKey)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
    public void WinGame()
    {
        canMove = false;
        winUI.SetActive(true);
    }
}
