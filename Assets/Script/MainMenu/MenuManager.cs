using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum Language {Swedish, English}
public enum Text {Aesthetic, Readable}
public class MenuManager : MonoBehaviour
{
    SaveSystem _saveSystem = new SaveSystem();
    public Animator animator;
    public Language m_Language;
    public Text m_Text;
    bool started = false;
    CameraBrain cBrain;
    public int menuValue = 1, highestMenu = 0, lowestMenu = 0, optionsValue;
    [SerializeField] CameraPoint[] points;
    GameObject[] TextOptions;
    Vector2 stickValue;
    bool pausedMove = false;
    [SerializeField] TextMeshPro[] textOptions;
    public TextMeshPro currentSelectedText;
    List<RectTransform> arrowPoints = new List<RectTransform>();
    public GameObject[] checkMarks;
    RectTransform arrowTransform;
    public UnityEngine.UI.Slider volumeSlider;
    Canvas optionsCanvas;
    public static MenuManager instance;

   [Serializable] public struct CameraPoint
    {
        public float rotateDuration, moveDuration;
        public GameObject target;
        public UnityEvent  startMoveAction, finishedMoveAction, startRotateAction,  finishedRotateAction, selectAction, cancelAction;
    }
    /*[Serializable] public struct ListOfPoints
    {
        public CameraPoint[] cPoints;
    }*/

    private void OnEnable()
    {
        cBrain.OnFinishedMove += CamFinishedMove;
        cBrain.OnFinishedRotate += CamFinishedRotate;
        cBrain.OnStartRotate += CamStartRotate;
        cBrain.OnStartMove += CamStartMove;
    }

    //CameraBrain.OnFinishedMove += CamFinishedMove();
    private void Awake()
    {
        instance = this;
        _saveSystem.Load();
        cBrain = Camera.main.GetComponent<CameraBrain>();

        TextOptions = GameObject.FindGameObjectsWithTag("Text");
        foreach (GameObject text in TextOptions)
        {
            text.SetActive(false);
        }

        GameObject[] arrowPointsOBJ = GameObject.FindGameObjectsWithTag("ArrowPoints");
        foreach (GameObject rect in arrowPointsOBJ)
        {
            arrowPoints.Add(rect.GetComponent<RectTransform>());
        }

        checkMarks = GameObject.FindGameObjectsWithTag("CheckMark");

        volumeSlider = GetComponent<UnityEngine.UI.Slider>();

        optionsCanvas = FindFirstObjectByType<Canvas>();

        arrowTransform = GameObject.Find("Arrow").GetComponent<RectTransform>();
    }

    private void Start()
    {
        optionsCanvas.gameObject.SetActive(false);
    }

    public void OnButton1()
    {
        if (!started)
        {
            started = true;
            MoveToMainMenu();
        }
        else if(menuValue < 5)
        {
            points?[menuValue].selectAction.Invoke();
        }
        else if(menuValue == 5)
        {
            switch (optionsValue)
            {
                case 3:
                    checkMarks[0].SetActive(true);
                    checkMarks[1].SetActive(false);
                    m_Language = Language.English;
                    break;
                case 2:
                    checkMarks[0].SetActive(false);
                    checkMarks[1].SetActive(true);
                    m_Language = Language.Swedish;
                    break;
                case 1:
                    checkMarks[2].SetActive(true);
                    checkMarks[3].SetActive(false);
                    m_Text = Text.Aesthetic;
                    break;
                case 0:
                    checkMarks[2].SetActive(false);
                    checkMarks[3].SetActive(true);
                    m_Text = Text.Readable;
                    break;
            }
        }
        else
        {

        }
    }

    public void OnButton2()
    {
        if (!started)
        {
            started = true;
            MoveToMainMenu();
        }
        else
        {
            points?[menuValue].cancelAction.Invoke();
        }
    }

    public void OnMenuMove(InputValue value)
    {
        /*if (value.started)
        {
            Debug.Log("Started");
        }
        if (value.performed)
        {*/
        stickValue = value.Get<Vector2>();

        if(menuValue < 4)
        {
            if (menuValue != highestMenu && (stickValue.x > 0.8 || stickValue.y > 0.8))
            {
            
                menuValue++;
                pausedMove = true;

                Menu1RotateCam();
            }
            else if (menuValue != lowestMenu && (stickValue.x < -0.8 || stickValue.y < -0.8))
            {
                menuValue--;
                pausedMove = true;

                Menu1RotateCam();
            }
            else pausedMove = false;
        }
        else if(menuValue == 5)
        {

            if (stickValue.y > 0.8 && optionsValue != arrowPoints.Count - 1)
            {
                 optionsValue++;
                 arrowTransform.position = arrowPoints[optionsValue].position;
            }
            else if (stickValue.y < -0.8 && optionsValue != 0)
            {
                optionsValue--;
                arrowTransform.position = arrowPoints[optionsValue].position;
            }
            
            if(optionsValue == 4)
            {
                if(stickValue.x > 0.8)
                {
                    volumeSlider.value++;
                }
                else if (stickValue.x < -0.8)
                {
                    volumeSlider.value--;
                }
            }
        }



        /*}
        if (value.canceled)
        {

        }*/
    }

    public void Menu1RotateCam()
    {
        dimText(currentSelectedText);
        highlightText(textOptions[menuValue]);

        cBrain.LerpCamera(points[menuValue].target.transform.rotation, points[menuValue].rotateDuration);
    }

    public void CamFinishedMove(Vector3 sentPosition)
    { 
        foreach (CameraPoint point in points)
        {
            if (point.target.transform.position == sentPosition)
            {
                point.finishedMoveAction.Invoke();
            }
        }
    }

    public void CamFinishedRotate(Quaternion sentRotation)
    {
        foreach (CameraPoint point in points)
        {
            if (point.target.transform.rotation == sentRotation)
            {
                point.finishedMoveAction.Invoke();
            }
        }
    }

    public void CamStartRotate()
    {
        if (menuValue < 0)
        {
            Debug.LogError("MenuValue outside of bounds of points index.");
        }
        else points?[menuValue].startRotateAction.Invoke();
    }

    public void CamStartMove()
    {
        if (menuValue < 0)
        {
            Debug.LogError("MenuValue outside of bounds of points index.");
        }
        else
        {
            points[menuValue].startMoveAction.Invoke();
        }
        //This is GPT code because I'm losing my mind
    }

    public void DisplayText(GameObject textOBJ)
    {
        textOBJ.SetActive(true);
    }

    public void HideText(GameObject textOBJ)
    {
        textOBJ.SetActive(false);
    }

    public void ActivateMenu(MenuParamaters menuParams)
    {
        //Debug.LogError($"Setting menu value to: {menuParams.name}");
        menuValue = menuParams.menuValue;
        highestMenu = menuParams.highestValue;
        lowestMenu = menuParams.lowestValue;
    }

    public void highlightText(TextMeshPro text)
    {
        text.color = Color.white;

        currentSelectedText = text;
    }

    public void dimText(TextMeshPro text)
    {
        text.color = Color.black;
    }

    public void LoadScene(int sceneIndex)
    {
        Debug.Log("Loading Scene by index: " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
        Destroy(GetComponent<PlayerInput>());
    }

    public void QuitGame()
    {
        //UnityEngine.Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    public void MoveToGameSelectScreen()
    {
        cBrain.LerpCamera(points[4].target.transform.position, points[4].moveDuration, points[4].target.transform.rotation, points[4].rotateDuration);
        animator.SetBool("Sleeping", true); //Sleep
    }

    public void MoveToMainMenu()
    {
        cBrain.LerpCamera(points[2].target.transform.position, 1f, points[2].target.transform.rotation, 1f);
        animator.SetBool("Sleeping", false); //No Sleep
    }

    public void MoveToSettings()
    {
        cBrain.LerpCamera(points[5].target.transform.position, points[5].moveDuration, points[5].target.transform.rotation, points[5].rotateDuration);
    }

    public void ActivateOptionsMenu()
    {
        optionsCanvas.gameObject.SetActive(true);

        if (m_Language == Language.English)
        {
            checkMarks[0].SetActive(true);
            checkMarks[1].SetActive(false);
        }
        else
        {
            checkMarks[0].SetActive(false);
            checkMarks[1].SetActive(true);
        }
        if (m_Text == Text.Aesthetic)
        {
            checkMarks[2].SetActive(true);
            checkMarks[3].SetActive(false);
        }
        else
        {
            checkMarks[2].SetActive(false);
            checkMarks[3].SetActive(true);
        }
    }

    public void DeactivateOptionsMenu()
    {
        optionsCanvas.gameObject.SetActive(false);
    }

    public void SaveSettings()
    {
        GameManager.instance._saveData._languageSettings = m_Language;
        GameManager.instance._saveData._textSettings = m_Text;
        GameManager.instance._saveData._volumeSettings = volumeSlider.value;

        _saveSystem.Save();
    }
}
