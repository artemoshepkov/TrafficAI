using System;
using System.Collections.Generic;
using CodeBase.Roads;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Car
{
    public class OnRoadChecker : MonoBehaviour
    {
        public event Action<List<Vector3>> OnAnotherRoad;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BezierCurve>(out BezierCurve curve))
            {
                OnAnotherRoad?.Invoke(curve.splinePoints);
            }
            if (other.TryGetComponent<CrossRoadBezierCurves>(out CrossRoadBezierCurves curves))
            {
                int randomIndex = (int)Random.Range(0, 3);
                OnAnotherRoad?.Invoke(curves.Curves[randomIndex].splinePoints);
            }
        }
    }
}
