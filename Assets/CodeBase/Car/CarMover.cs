using System;
using UnityEngine;

namespace CodeBase.Car
{
    public class CarMover : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Rigidbody _carRB;
        [SerializeField] private Transform _transform;
        [SerializeField] private OnGroundChecker _onGroundChecker;

        [Header("Movement settings")] 
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private Transform _movePoint;
        
        [Header("Info")]
        [SerializeField] private float _currentSpeed;

        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
                PressGas();
            if (Input.GetKey(KeyCode.S))
                PressBrake();
        }

        public void PressGas() => TryMove(_acceleration, _transform.forward);
        public void PressBrake() => TryMove(_deceleration, -_transform.forward);

        private void TryMove(float acceleration, Vector3 dir)
        {
            if (!_onGroundChecker.Check())
                return;
            _carRB.AddForceAtPosition(acceleration * dir, _movePoint.position, ForceMode.Acceleration);
            _currentSpeed = _transform.InverseTransformDirection(_carRB.velocity).z;
            if (Math.Abs(_currentSpeed) > _maxSpeed)
            {
                _currentSpeed = _maxSpeed;
                _carRB.velocity = new Vector3(_carRB.velocity.x, _carRB.velocity.y, _maxSpeed);
            }
        }
    }
}