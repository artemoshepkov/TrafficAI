using CodeBase.AI.Base;
using CodeBase.Car;
using CodeBase.Roads;
using UnityEngine;

namespace CodeBase.AI.States
{
    public class OnGraphDriving : IStrategy
    {
        private CarMover _carMover;

        public OnGraphDriving(CarMover carMover) => _carMover = carMover;

        public Node.Status Process()
        {
            _carMover.Move(1f);

            return Node.Status.Running;
        }

        public void Reset()
        {
            // _carMover.SetBrake(1f);
        }
    }
    
    public class OnGraphRandomDriving : IStrategy
    {
        private CarMover _carMover;
        private RoadNode _currentNode;
        private Transform _transform;

        public OnGraphRandomDriving(CarMover carMover, RoadNode currentNode, Transform transform)
        {
            _carMover = carMover;
            _currentNode = currentNode;
            _transform = transform;
        }
        
        public Node.Status Process()
        {
            var steerVector = _transform.InverseTransformPoint(_currentNode.Position);
            var steerValue = steerVector.x / steerVector.magnitude;
            
            _carMover.Turn(steerValue);
            _carMover.Move(1f);

            var distanceToNode = Vector3.Distance(_transform.position, _currentNode.Position);
            if (distanceToNode < 0.3f)
                _currentNode = _currentNode.GetRandomConnectedNode();
                
            return Node.Status.Running;
        }

        public void Reset()
        {
            // _carMover.SetBrake(1f);
        }
    }
}