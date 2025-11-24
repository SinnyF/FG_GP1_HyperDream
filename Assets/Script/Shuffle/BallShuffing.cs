using UnityEngine;
using UnityEngine.InputSystem;

public class BallShuffing : MonoBehaviour
{
    // Properties 
    int selector = 0;
    int ballSelector = 0;
    int losses;
    int wins;
    int numberOfCups;

    // Arrayed Game Objects
    [Header("Cups")]
    public GameObject[] Cups;

    // Non Arrayed Game Objects
    [Header("Balls")]
    public GameObject Ball0;
    public GameObject Ball1;
    public GameObject Ball2;

    // Single Game Objects
    [Header("Attached Game Objects")]
    public GameObject highlighter;
    public GameObject winMenu;
    public GameObject loseMenu;

    // Controll buttons
    InputAction leftButton;
    InputAction rightButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Refactoring
        numberOfCups = Cups.Length;
        // selector = Random.Range(0, numberOfCups);

        // Making the menus hidden
        winMenu.SetActive(false);
        loseMenu.SetActive(false);

        // putting the Highlight gameobject over the selector at the start of the game
        Highlight();

        // Putting the ball inside of a random cup at the start of the game
        Ball0.transform.position = Cups[Random.Range(0, numberOfCups)].transform.position;
        Ball1.transform.position = Cups[Random.Range(0, numberOfCups)].transform.position;
        Ball2.transform.position = Cups[Random.Range(0, numberOfCups)].transform.position;

        // Input Assingment
        leftButton = InputSystem.actions.FindAction("Button1");
        rightButton = InputSystem.actions.FindAction("Button2");

        /*// Balls going in to the corrrect slots
        for (int i = 0; i < numberOfCups; i++)
        {
            Balls[i] = GameObject.Find("Sphere " + i);
        }
        */
        // Cups going in to the corrrect slots
        // Making the menus visible
        for (int i = 0; i < numberOfCups; i++)
        {
            Cups[i] = GameObject.Find("Cylinder " + i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Iterating Selection/Highlight Position
        if (leftButton.triggered)
        {
            selector++;
            if (selector == Cups.Length)
            {
                selector = 0;
            }
            Highlight();
        }

        // Selecting the cup & Hiding the ball/balls under a random cup when selecting
        // Making wins/losses iterate for every won/loss
        // Winning/Losing Menus Visible
        if (rightButton.triggered)
        {            
            if (selector == ballSelector)
            {
                Debug.Log("You Won");
                Debug.Log(wins+1);
                wins++;
                
            }
            else if (selector != ballSelector)
            {
                Debug.Log("You Lost");
                Debug.Log(losses+1);
                losses++;
            }
            ballSelector = Random.Range(0, numberOfCups);
            Ball0.transform.position = Cups[Random.Range(0, numberOfCups)].transform.position;
            Ball1.transform.position = Cups[Random.Range(0, numberOfCups)].transform.position;
            Ball2.transform.position = Cups[Random.Range(0, numberOfCups)].transform.position;
            if (losses == 3)
            {
                loseMenu.SetActive(true);
            }
            else if (wins == 3)
            {
                winMenu.SetActive(true);
            }
        }
    }


    // Highlighting The Selection positionP
    void Highlight()
    {
        Vector3 highlightPosition = Cups[selector].transform.position;
        highlightPosition.y += 1.8f;
        highlighter.transform.position = highlightPosition;
    }    
}
