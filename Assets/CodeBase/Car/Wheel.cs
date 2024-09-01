using System;
using UnityEngine;

namespace CodeBase.Car
{
    [Serializable]
    public struct Wheel
    {
        public enum Axle
        {
            Front,
            Rear,
        }
            
        public Transform Transform;
        public WheelCollider Collider;
        public Axle AxleType;
    }
}