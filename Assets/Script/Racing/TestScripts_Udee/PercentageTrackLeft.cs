using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PercentageTrackleft : MonoBehaviour
{
    public Transform player,rival;
    public Transform[] waypoints;
    public bool p1Wins = true;
    //public TextMeshProUGUI progressText;
    public Slider playerSlider,rivalSlider;
    public FGSpline playerSpline,rivalSpline;
    [SerializeField] private float catchupMult = 1.5f;
    private float totalTrackDistance;
    public GameObject p1Position1,p1Position2,p2Position1,p2Position2,p1Trophy,p2Trophy;

    void Start()
    {
        totalTrackDistance = 0f;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            totalTrackDistance += Vector3.Distance(waypoints[i].position, waypoints[i + 1].position);
        }
    }

    void Update()
    {
        //float playerDistance = GetDistanceAlongPath(player.position);
        //float rivalDistance = GetDistanceAlongPath(rival.position);
        //Debug.Log(playerSpline.getPathProgress());
        playerSlider.value = playerSpline.getPathProgress();
        rivalSlider.value = rivalSpline.getPathProgress();
        if(((playerSpline.getPathProgress()-rivalSpline.getPathProgress()) > 0.05))
        {
            //Debug.Log("P1 Ahead");
            rival.GetComponent<MovementState>().setCatchup(catchupMult);
            //rival.GetComponent<PlayerControl>().ActivateCatchup();
        }
        else if(((rivalSpline.getPathProgress() - playerSpline.getPathProgress()) > 0.05))
        {
            //Debug.Log("P2 Ahead");
            player.GetComponent<MovementState>().setCatchup(catchupMult);
            //player.GetComponent<PlayerControl>().ActivateCatchup();
        }
        else
        {
           //Debug.Log("Catchup Inactive");
        }
        /* float percentLeft = (1f - (playerDistance / totalTrackDistance)) * 100f;
         progressText.text = $"Track left: {percentLeft:F1}%";*/
        if (((playerSpline.getPathProgress() > rivalSpline.getPathProgress())))
        {
            
            p1Position1.SetActive(true);
            p1Position2.SetActive(false);
            p1Trophy.SetActive(true);
            p2Trophy.SetActive(false);
            p1Position2.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
            p2Position2.SetActive(true);
            p2Position1.SetActive(false);
            p2Position1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

            p1Wins = true;

        }
        else 
        {
            p1Position1.SetActive(false);
            p1Position2.SetActive(true);
            p1Trophy.SetActive(false);
            p2Trophy.SetActive(true);
            p1Position1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            p2Position2.SetActive(false);
            p2Position1.SetActive(true);
            p2Position2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

             p1Wins = false;
        }
    }

  /*float GetDistanceAlongPath(Vector3 pos)
    {
        float distance = 0f;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Vector3 a = waypoints[i].position;
            Vector3 b = waypoints[i + 1].position;

            Vector3 ab = b - a;
            float t = Mathf.Clamp01(Vector3.Dot(pos - a, ab.normalized) / ab.magnitude);
            Vector3 projection = a + ab * t;

            if (Vector3.Distance(pos, projection) < 10f)
            {
                for (int j = 0; j < i; j++)
                {
                    distance += Vector3.Distance(waypoints[j].position, waypoints[j + 1].position);
                }
                distance += Vector3.Distance(a, projection);
                break;
            }
        }
        return distance;
    }*/
}