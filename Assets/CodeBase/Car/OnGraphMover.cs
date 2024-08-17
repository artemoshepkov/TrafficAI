using System.Collections;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Car
{
    public class OnGraphMover : MonoBehaviour
    {
        private Transform _transform;
        private Node _currentNode;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        private void Awake() => _transform = transform;
        private void Start() => StartCoroutine(Move());

        public void Init(Node startNode) => _currentNode = startNode;

        private IEnumerator Move()
        {
            Vector3 startPoint;
            Vector3 endPoint;

            float moveProgress = 0;
            float rotationProgress = 0;

            while (_currentNode != null)
            {
                _currentNode = _currentNode.GetRandomConnectedNode();

                startPoint = _transform.position;
                endPoint = new Vector3(_currentNode.Position.x, _transform.position.y, _currentNode.Position.z);
                
                var directionToPoint = _currentNode.Position - _transform.position;
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
                
                _transform.position = new Vector3(_currentNode.Position.x, _transform.position.y, _currentNode.Position.z);
                moveProgress = 0;
                rotationProgress = 0;
                
                yield return null;
            }
        }
    }
}