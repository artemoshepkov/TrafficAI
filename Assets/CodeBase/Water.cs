using System.Collections.Generic;
using UnityEngine;

namespace CodeBase
{
    public class Water : MonoBehaviour
    {
        private Dictionary<Transform, Rigidbody> _rigidbodies = new();
        
        [SerializeField] private Transform _transform;
        [SerializeField] private float _waterDensity;

        private void FixedUpdate()
        {
            // float upPointOfWater = _transform.position.y + _transform.localScale.y;
            foreach (var pair in _rigidbodies)
            {
                float divePercent = -pair.Key.position.y + pair.Key.localScale.x * 0.5f;
                divePercent = Mathf.Clamp(divePercent, 0f, 1f);

                float pushingForce = _waterDensity * divePercent;
                pair.Value.AddForce(pushingForce * Vector3.up);
            }
        }

        private void OnTriggerEnter(Collider other) => _rigidbodies[other.gameObject.GetComponent<Transform>()] = other.gameObject.GetComponent<Rigidbody>();
        private void OnTriggerExit(Collider other) => _rigidbodies.Remove(other.gameObject.GetComponent<Transform>());
    }
}