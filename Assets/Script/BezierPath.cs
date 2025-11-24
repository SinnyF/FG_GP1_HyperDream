using UnityEngine;


[ExecuteAlways]
public class BezierPath : MonoBehaviour
{
    [SerializeField] Transform point;
    [HideInInspector] public Vector3 Startv = new Vector3(0,0,0), End = new Vector3(0,0,10), ModStart = new Vector3(5,0,0), ModEnd = new Vector3(-5,0,10);
    [SerializeField] float totalDistance = 0, constantPath = 0;
    public bool isLine = false, applyRotation = true, anchored = true;
    public bool isSpline = false;
    [SerializeField] int sampleCount = 10;
    [SerializeField] Vector3[] samples;
    [SerializeField] float[] distances;
    Vector3 lastPos;
    Quaternion direction;
    int hack = 0;
    private void Start()
    {
        
        sampleCount = 10;
        if (!Application.isPlaying && point == null && !isSpline)
        {
            point = transform;
            Startv += point.position;
            End += point.position;
            if(isLine)
            {
                ModEnd = End;
                ModStart = Startv;
            }
            else
            {
                ModEnd += point.position;
                ModStart += point.position;
            }
        }
        if (isLine)
            sampleCount = 1;
        point.rotation = Quaternion.identity;
        samples = new Vector3[sampleCount];
        distances = new float[sampleCount];
        Sample();
    }



    private void Update()
    {
        if (!Application.isPlaying && isLine)
        {
            ModEnd = End;
            ModStart = Startv;
        }
        
    }

    public float getDistance() { return totalDistance; }

    public Quaternion getDirectionR()
    {
        return direction;
    }

    public void SetPoint(Transform point)
    {
        this.point = point;
    }
    void Sample()
    {
        totalDistance = 0;
        for(int i = 0; i<samples.Length; i++)
        {
            samples[i] = FollowCurve((float)(i+1)/(float)sampleCount);
            if (i > 0)
            {
                //Debug.Log(i +"||"+ (float)(i + 1) / 10f);
                distances [i] = Vector3.Distance(samples[i], samples[i - 1]);
                totalDistance += distances[i];
            }
            else
            {
                //Debug.Log(i + "||" + (float)(i + 1) / 10f);
                distances[i] = Vector3.Distance(samples[i], Startv);
                totalDistance += distances[i];
            }
        }
    }

    public Vector3 FollowCurve(float path)
    {
        
        path = Mathf.Clamp01(path);
        if (isLine)
        {
            point.position = Vector3.Lerp(Startv, End, path);
        }
        else
        {
            point.position = Vector3.Lerp(
                Vector3.Lerp(Vector3.Lerp(Startv, ModStart, path), Vector3.Lerp(ModStart, ModEnd, path), path),
                Vector3.Lerp(Vector3.Lerp(ModStart, ModEnd, path), Vector3.Lerp(ModEnd, End, path), path), path);
        }
        
        var towards = (point.position - lastPos).normalized;
        if ((point.position - lastPos).magnitude > 0.001f)
        {
            direction = Quaternion.LookRotation(towards);
        }
        if (path == 0)
        {
            direction = Quaternion.identity;
        }

            lastPos = point.position;

        if(applyRotation)
            point.rotation = direction;
        return point.position;
    }

    public float ConstantSpeed(float speed)
    {
        if(constantPath < 1)
        {
            constantPath += (speed / distances[(int)(constantPath * sampleCount)]) / sampleCount;
            FollowCurve(constantPath);
        }
        return constantPath;
    }
}

