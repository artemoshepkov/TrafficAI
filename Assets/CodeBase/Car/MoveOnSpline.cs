using System.Collections.Generic;
using CodeBase.Car;
using UnityEngine;

public class MoveOnSpline : MonoBehaviour
{
    public OnRoadChecker OnRoadChecker;
    
    public List<Vector3> dirSpline = null;
    
    [SerializeField] private float _moveSpeed;

    public int currentIndex = 0;
    public Transform Transform;
    private void Start()
    {
        OnRoadChecker.OnAnotherRoad += UpdateDirSpline;
    }

    private void Update()
    {
        if (dirSpline != null)
            Move();
    }

    private float t = 0f;
    
    private void Move()
    {
        if (currentIndex >= dirSpline.Count)
            return;
        
        t += Time.fixedDeltaTime;
        Transform.position = Vector3.Lerp(Transform.position, dirSpline[currentIndex], t * _moveSpeed);

        if (t >= 1f)
        {
            t = 0f;
            currentIndex++;
            Transform.forward =  dirSpline[currentIndex] - Transform.position;
        }
    }

    private void UpdateDirSpline(List<Vector3> obj)
    {
        dirSpline = obj;
        currentIndex = 0;
    }
}
