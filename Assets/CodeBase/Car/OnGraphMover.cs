using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Roads;
using UnityEngine;

namespace CodeBase.Car
{
    public class OnGraphMover : MonoBehaviour
    {
        private Transform _transform;
        private RoadNode _currentRoadNode;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        private void Awake() => _transform = transform;
        private void Start() => StartCoroutine(Move());

        public void Init(RoadNode startRoadNode) => _currentRoadNode = startRoadNode;

        private IEnumerator Move()
        {
            Vector3 startPoint;
            Vector3 endPoint;

            float moveProgress = 0;
            float rotationProgress = 0;

            while (_currentRoadNode != null)
            {
                _currentRoadNode = _currentRoadNode.GetRandomConnectedNode();

                startPoint = _transform.position;
                endPoint = new Vector3(_currentRoadNode.Position.x, _transform.position.y, _currentRoadNode.Position.z);
                
                var directionToPoint = _currentRoadNode.Position - _transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(directionToPoint);
                targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f);
                
                while (moveProgress < 1)
                {
                    moveProgress += Time.fixedDeltaTime * _moveSpeed;
                    _transform.position =  Vector3.Lerp(startPoint, endPoint, moveProgress);

                    rotationProgress += Time.fixedDeltaTime * _rotationSpeed;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationProgress);
                    
                    yield return null;
                }
                
                _transform.position = new Vector3(_currentRoadNode.Position.x, _transform.position.y, _currentRoadNode.Position.z);
                moveProgress = 0;
                rotationProgress = 0;
                
                yield return null;
            }
        }
    }
}