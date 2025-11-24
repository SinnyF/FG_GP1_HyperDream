using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[ExecuteAlways]
public class FGSpline : MonoBehaviour
{
    [SerializeField] List<BezierPath> curves = new List<BezierPath>();
    [SerializeField] Transform point;
    [SerializeField] float totalDistance = 0f;
    public bool anchored = true;
    float constantPath = 0;

    private void Awake()
    {
        totalDistance = 0;
        if (point == null)
            point = transform;
        if (!Application.isPlaying && curves.Count == 0)
        {
            AddCurve();
            AddCurve();
            AddCurve();
        }

        foreach (var curve in curves)
        {
            curve.SetPoint(point);
            totalDistance += curve.getDistance();
        }
        
    }

    public List<BezierPath> GetCurves()
    {
        return curves;
    }

    public float getPathProgress()
    {
        return constantPath;
    }

    public Transform getPoint() {  return point; }

    public void AddCurve()
    {
        var bp = transform.GetChild(0).gameObject.AddComponent<BezierPath>();
        bp.isSpline = true;
        bp.SetPoint(point);
        bp.anchored = true;
        bp.applyRotation = true;
        curves.Add(bp);
        if (curves.Count > 1 )
        {
            curves[curves.Count - 1].Startv = curves[curves.Count - 2].End; 
            curves[curves.Count - 1].End = curves[curves.Count - 1].Startv + new Vector3(0,0,10);
            curves[curves.Count - 1].ModEnd = curves[curves.Count - 1].Startv + new Vector3(-5, 0, 10);
            curves[curves.Count - 1].ModStart = curves[curves.Count - 1].Startv + new Vector3(5, 0, 0);
        }
        else
        {
            curves[curves.Count - 1].Startv = Vector3.zero;
            curves[curves.Count - 1].End = curves[curves.Count - 1].Startv + new Vector3(0, 0, 10);
            curves[curves.Count - 1].ModEnd = curves[curves.Count - 1].Startv + new Vector3(-5, 0, 10);
            curves[curves.Count - 1].ModStart = curves[curves.Count - 1].Startv + new Vector3(5, 0, 0);
        }
    }

    public void DeleteCurve()
    {
        var bp = curves[curves.Count - 1];
        curves.RemoveAt(curves.Count - 1);
        DestroyImmediate(bp);
    }

    public void FollowSpline(float path)
    {
        if(path * curves.Count < curves.Count)
        {
            int curve = (int)(path * curves.Count);
            float div = 1f / (float)curves.Count;
            curves[curve].FollowCurve( ((path - div) * curves.Count) - curve );
        }
        
    }

    public void FollowSplineConstant(float speed)
    {
        if (constantPath * curves.Count < curves.Count)
        {
            int curve = (int)(constantPath * curves.Count);
            float div = 1f / (float)curves.Count;
            float overflow = curves[curve].ConstantSpeed(speed);
            constantPath = ( overflow / (float)curves.Count + (float)curve / (float)curves.Count);
        }
    }

}
