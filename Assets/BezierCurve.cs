using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public List<Transform> points = new();
    private float t = 0;
    public float tStep = 0.1f;
    
    public List<Vector3> splinePoints = new();

    private void Start()
    {
        CalculateSpline();
    }

    public void CalculateSpline()
    {
        if (points.Count < 3)
            return;
        
        splinePoints.Clear();
        t = 0f;
        while (t <= 1)
        {
            var p1 = Vector3.Lerp(points[0].position, points[1].position, t);
            var p2 = Vector3.Lerp(points[1].position, points[2].position, t);
        
            var p11 = Vector3.Lerp(p1, p2, t);
        
            splinePoints.Add(p11);
            t += tStep;
        }
    }

    private void OnDrawGizmos()
    {
        if (splinePoints.Count < 1)
            return;
        Gizmos.color = Color.red;
        for (int i = 0; i < splinePoints.Count - 1; i++)
        {
            Gizmos.DrawLine(splinePoints[i], splinePoints[i + 1]);
        }

        DrawWith3Points();
    }

    private void DrawWith3Points()
    {
        if (points.Count < 3)
            return;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(points[0].position, points[1].position);
        Gizmos.DrawLine(points[1].position, points[2].position);

        var p1 = Vector3.Lerp(points[0].position, points[1].position, t);
        var p2 = Vector3.Lerp(points[1].position, points[2].position, t);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(p1, p2);

        var p11 = Vector3.Lerp(p1, p2, t);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(p11, 0.01f);
    }
    private void DrawWith4Points()
    {
        if (points.Count < 4)
            return;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(points[0].position, points[1].position);
        Gizmos.DrawLine(points[1].position, points[2].position);
        Gizmos.DrawLine(points[2].position, points[3].position);

        var p1 = Vector3.Lerp(points[0].position, points[1].position, t);
        var p2 = Vector3.Lerp(points[1].position, points[2].position, t);
        var p3 = Vector3.Lerp(points[2].position, points[3].position, t);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);

        var p11 = Vector3.Lerp(p1, p2, t);
        var p22 = Vector3.Lerp(p2, p3, t);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(p11, p22);

        var p111 = Vector3.Lerp(p11, p22, t);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(p111, 0.01f);
    }
}
