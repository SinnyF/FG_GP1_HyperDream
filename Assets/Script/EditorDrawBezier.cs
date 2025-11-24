using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierPath))]
public class EditorDrawBezier : Editor
{
    private void OnSceneGUI()
    {
        BezierPath path = target as BezierPath;
        if (!path.anchored)
        {
            Vector3 anchor = Handles.PositionHandle(path.Startv, Quaternion.identity);

            var sdif = anchor - path.Startv;
            var edif = anchor - path.End;
            var msdif = anchor - path.ModStart;
            var medif = anchor - path.ModEnd;

            path.Startv = anchor + sdif;
            path.End = anchor + edif;
            path.ModStart = anchor + msdif;
            path.ModEnd = anchor + medif;
        }
        else if (!path.isSpline)
        {
            path.Startv = Handles.PositionHandle(path.Startv, Quaternion.identity);
            Handles.color = Color.green;
            Handles.CubeHandleCap(0, path.Startv, Quaternion.identity, 0.7f, EventType.Repaint);

            
            path.End = Handles.PositionHandle(path.End, Quaternion.identity);
            Handles.color = Color.red;
            Handles.CubeHandleCap(0, path.End, Quaternion.identity, 0.7f, EventType.Repaint);

            

            if (!path.isLine)
            {
                path.ModStart = Handles.PositionHandle(path.ModStart, Quaternion.identity);
                Handles.color = Color.green;
                Handles.CubeHandleCap(0, path.ModStart, Quaternion.identity, 0.5f, EventType.Repaint);

                path.ModEnd = Handles.PositionHandle(path.ModEnd, Quaternion.identity);
                Handles.color = Color.red;
                Handles.CubeHandleCap(0, path.ModEnd, Quaternion.identity, 0.5f, EventType.Repaint);
            }
        }

        Handles.color = Color.white;
        Handles.DrawBezier(path.Startv, path.End, path.ModStart, path.ModEnd, Color.white, null, 2f);
    }
}
