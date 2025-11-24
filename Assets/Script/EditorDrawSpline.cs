using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FGSpline))]
public class EditorDrawSpline : Editor
{
    private void OnSceneGUI()
    {
        FGSpline spline = target as FGSpline;
        var curves = spline.GetCurves();

        if (!spline.anchored)
        {
            Vector3 anchor = Vector3.zero;
            for (int count = 0; count < curves.Count; count++)
            {
                if (count == 0)
                {
                    anchor = Handles.PositionHandle(curves[count].Startv, Quaternion.identity);
                }

                var sdif = anchor - curves[count].Startv;
                var edif = anchor - curves[count].End;
                var msdif = anchor - curves[count].ModStart;
                var medif = anchor - curves[count].ModEnd;

                curves[count].Startv = anchor + sdif;
                curves[count].End = anchor + edif;
                curves[count].ModStart = anchor + msdif;
                curves[count].ModEnd = anchor + medif;

                Handles.color = Color.white;
                Handles.DrawBezier(curves[count].Startv, curves[count].End, curves[count].ModStart, curves[count].ModEnd, Color.white, null, 2f);
            }
            
        }
        else
        {
            for (int count = 0; count < curves.Count; count++)
            {
                if (count == 0)
                {
                    curves[count].Startv = Handles.PositionHandle(curves[count].Startv, Quaternion.identity);
                    Handles.color = Color.green;
                    Handles.CubeHandleCap(0, curves[count].Startv, Quaternion.identity, 0.7f, EventType.Repaint);
                }
                else
                {
                    curves[count].Startv = Handles.PositionHandle(curves[count].Startv, Quaternion.identity);
                    Handles.color = Color.blue;
                    Handles.CubeHandleCap(0, curves[count].Startv, Quaternion.identity, 0.7f, EventType.Repaint);
                }

                if (count < curves.Count - 1)
                {
                    curves[count].End = curves[count + 1].Startv;
                }
                else
                {
                    curves[count].End = Handles.PositionHandle(curves[count].End, Quaternion.identity);
                    Handles.color = Color.red;
                    Handles.CubeHandleCap(0, curves[count].End, Quaternion.identity, 0.7f, EventType.Repaint);
                }

                if (!curves[count].isLine)
                {
                    curves[count].ModStart = Handles.PositionHandle(curves[count].ModStart, Quaternion.identity);
                    Handles.color = Color.green;
                    Handles.CubeHandleCap(0, curves[count].ModStart, Quaternion.identity, 0.5f, EventType.Repaint);

                    curves[count].ModEnd = Handles.PositionHandle(curves[count].ModEnd, Quaternion.identity);
                    Handles.color = Color.red;
                    Handles.CubeHandleCap(0, curves[count].ModEnd, Quaternion.identity, 0.5f, EventType.Repaint);
                }
                Handles.color = Color.white;
                Handles.DrawBezier(curves[count].Startv, curves[count].End, curves[count].ModStart, curves[count].ModEnd, Color.white, null, 2f);
            }
            Handles.BeginGUI();

            if (GUI.Button(new Rect(10, 200, 100, 50), "Add Curve"))
            {
                spline.AddCurve();
            }

            if (GUI.Button(new Rect(10, 250, 100, 50), "Remove Curve"))
            {
                spline.DeleteCurve();
            }

            Handles.EndGUI();
        }
    }
}
