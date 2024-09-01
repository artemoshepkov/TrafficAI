using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Car
{
    public class CarMover : MonoBehaviour
    {
        private Dictionary<Wheel.Axle, List<Wheel>> _wheelsByAxle = new();

        [Header("References")] 
        [SerializeField] private Rigidbody _carRB;
        [SerializeField] private List<Wheel> _wheels;

        [Header("Movement settings")] 
        [SerializeField] private float _maxAcceleration;
        [SerializeField] private float _maxSteerAngle;
        [SerializeField] private float _maxBrake;

        private void Start()
        {
            _wheelsByAxle[Wheel.Axle.Front] = _wheels.Where(w => w.AxleType == Wheel.Axle.Front).ToList();
            _wheelsByAxle[Wheel.Axle.Rear] = _wheels.Where(w => w.AxleType == Wheel.Axle.Rear).ToList();
        }

        private void FixedUpdate() => AnimateWheels();

        public void SetBrake(float isBrake)
        {
            var brake = isBrake * _maxBrake * Time.deltaTime;
            _wheelsByAxle[Wheel.Axle.Rear].ForEach(w => w.Collider.brakeTorque = brake);
        }

        public void Turn(float horizontalAxis)
        {
            float turning = horizontalAxis * _maxSteerAngle * 0.6f * Time.deltaTime;
            _wheelsByAxle[Wheel.Axle.Front].ForEach(w => w.Collider.steerAngle = turning);
        }

        public void Move(float verticalAxis)
        {
            float movement = verticalAxis * _maxAcceleration * Time.deltaTime;
            _wheelsByAxle[Wheel.Axle.Rear].ForEach(w => w.Collider.motorTorque = movement);
        }

        private void AnimateWheels()
        {
            foreach (Wheel wheel in _wheels)
            {
                Vector3 pos;
                Quaternion rotation;
                wheel.Collider.GetWorldPose(out pos, out rotation);
                wheel.Transform.rotation = rotation;
            }
        }
    }
}