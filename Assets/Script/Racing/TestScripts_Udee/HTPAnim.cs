using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Windows;

public class HTPAnim : MonoBehaviour
    
{  
    public static HTPAnim instance;
    SaveSystem _saveSystem = new SaveSystem();
    private PlayerInput playerInput;
    [SerializeField]private GameObject P1, P2;
    private float delay = 0.5f;
    public GameObject buttonA, buttonB,p1Pos,p2Pos,timerParent,finishImg;
    public GameObject FTUEParent,FinishUI, dreamModeP1, dreamModeP2,dreamModeGameplayP1,dreamModeGameplayP2;
    [SerializeField]private float timer = 1f, inputTimer;
    public float playerTime = 0f, _highscore;
    [SerializeField]bool ftueActive, finishActive = false, inputPressed = false;
    public bool startTimer = true;
    InputAction inputA, inputB, inputC, inputD,inputE,inputF;
    public TextMeshProUGUI PlayerTimer;
    private Countdown countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sd
        countdown = GetComponent<Countdown>();
        ftueActive = true;
        Time.timeScale = 0f;
        playerInput = FindFirstObjectByType<PlayerInput>();
        inputA = playerInput.actions.FindAction("B1");
        inputB = playerInput.actions.FindAction("B2");
        inputC = playerInput.actions.FindAction("P2B1");
        inputD = playerInput.actions.FindAction("P2B2");
        inputE = playerInput.actions.FindAction("P1Up");
        inputF = playerInput.actions.FindAction("P2Up");
        P1.GetComponent<PlayerControl>().isDisabled = true;
        P2.GetComponent<PlayerControl>().isDisabled = true;

    }
    public void Update()
    {
       /* if (swap)
        {
            StartCoroutine(ColorSwap());
        }*/
        if (((inputA.IsPressed() && inputB.IsPressed()) || (inputC.IsPressed() && inputD.IsPressed())) && ftueActive)
        {
            inputTimer += Time.unscaledDeltaTime;
            if (inputTimer > delay)
            {
                Time.timeScale = 1f;
                FTUEParent.SetActive(false);
                ftueActive = false;
                countdown.StartCoroutine(countdown.CountdownCoroutine());
                timer = 0;
                dreamModeP1.SetActive(false);
                dreamModeP2.SetActive(false);
                // P1.GetComponent<PlayerControl>().isDisabled = false;
                //P2.GetComponent<PlayerControl>().isDisabled = false;

            }
        }
          // if ((inputA.IsPressed() /*|| inputB.IsPressed() */|| inputC.IsPressed()) /*|| inputD.IsPressed()*/ && ftueActive)
       /* {
            Time.timeScale = 1f;
            FTUEParent.SetActive(false);
            ftueActive = false;
            countdown.StartCoroutine(countdown.CountdownCoroutine());
        }*/
        if((inputE.IsPressed() ||Input.GetKeyDown(KeyCode.A)) && !inputPressed && ftueActive)
        {
            StartCoroutine(inputDelay());
            if (!P1.GetComponent<MovementState>().easyMode)
            {
                P1.GetComponent<MovementState>().easyMode = true;
                dreamModeP1.SetActive(true);
                dreamModeGameplayP1.SetActive(true);
            }
            else
            {                 
                P1.GetComponent<MovementState>().easyMode = false;
                dreamModeP1.SetActive(false);
                dreamModeGameplayP1.SetActive(false);
            }
        }
        if ((inputF.IsPressed() || Input.GetKeyDown(KeyCode.D)) && !inputPressed && ftueActive)
        {
            StartCoroutine(inputDelay());
            if (!P2.GetComponent<MovementState>().easyMode)
            {
                P2.GetComponent<MovementState>().easyMode = true;
                dreamModeP2.SetActive(true);
                dreamModeGameplayP2.SetActive(true);
            }
            else
            {
                P2.GetComponent<MovementState>().easyMode = false;
                dreamModeP2.SetActive(false);
                dreamModeGameplayP2.SetActive(false);
            }
        }
        if (startTimer)
        {
            playerTime += Time.deltaTime;
        }
       timerParent.GetComponentInChildren<TextMeshProUGUI>().text = playerTime.ToString("F2") + "s";

        if (((inputA.IsPressed() && inputB.IsPressed()) || (inputC.IsPressed() && inputD.IsPressed())) && finishActive)
        {
            inputTimer += Time.unscaledDeltaTime;
            if (inputTimer > delay)
            {
                Time.timeScale = 1f;
                var players = FindObjectsByType<PlayerInput>(FindObjectsSortMode.None);
                foreach (var p in players)
                    Destroy(p);
                Time.timeScale = 1f;
                SceneManager.LoadScene(0);
            }
        }
    }
    public IEnumerator ColorSwap()
    {
       //swap = false;
        buttonA.GetComponent<Image>().color = new Color(1,1,1,0.3f);
        buttonB.GetComponent<Image>().color = Color.white;
        yield return new WaitForSecondsRealtime(timer);
        buttonA.GetComponent<Image>().color = Color.white;
        buttonB.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSecondsRealtime(timer);
       // swap = true;
    }
   /* public IEnumerator WaitAndShowFinishUI()
    {
        yield return new WaitForSeconds(1f);
        InitFinishUI();
    }*/
    public void InitFinishUI(bool p1winner)
    {
       /* float hiScore = 35f;
        IFormatter formatter = new BinaryFormatter();
        if (File.Exists("HiScore.bin"))
        {
            Stream stream = new FileStream("HiScore.bin", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            hiScore = (float)formatter.Deserialize(stream);
            stream.Close();
        }
        if (hiScore > playerTime)
        {
            Stream stream = new FileStream("HiScore.bin", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            formatter.Serialize(stream, playerTime);
            stream.Close();
        }*/
        PlayerTimer.text = "Fastest Time:\n" + _highscore.ToString("F2") + "s!\nYour Time:" + playerTime.ToString("F2") + "s!";
        if (playerTime > _highscore)
        {
            _saveSystem.Save();
        }
        StartCoroutine(finishImgDelay());
        startTimer = false;
        p1Pos.SetActive(false);
        p2Pos.SetActive(false);
        //Time.timeScale = 1f; 
        timerParent.SetActive(false);
        P1.GetComponent<PlayerControl>().isDisabled = true;
        P2.GetComponent<PlayerControl>().isDisabled = true;

        if(p1winner)
        {
            P1.GetComponentInChildren<Animator>().SetBool("Win", true);
            P2.GetComponentInChildren<Animator>().SetBool("Lose", true);
        }
        else
        {
            P1.GetComponentInChildren<Animator>().SetBool("Lose", true);
            P2.GetComponentInChildren<Animator>().SetBool("Win", true);
        }
    }
    public IEnumerator inputDelay()
    {
        inputPressed = true;
        yield return new WaitForSecondsRealtime(0.5f);
        inputPressed = false;
    }
    public IEnumerator finishImgDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        FinishUI.SetActive(true);
        finishActive = true;
        finishImg.SetActive(false);

    }
}
