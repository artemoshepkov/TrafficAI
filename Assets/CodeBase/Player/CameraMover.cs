using UnityEngine;

namespace CodeBase.Player
{
    public class CameraMover : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _folowing;

        [Header("Settings")]
        [SerializeField] private Vector3 _positionShift;
        [SerializeField] private Vector3 _rotationShift;

        public void Init(Transform folowing) => _folowing = folowing;

        private void Update()  
        {
            if (_folowing == null)
                return;

            // var dir = _folowing.position - _cameraTransform.position;

            // _cameraTransform.forward = dir.normalized;
            
            // var quaternion = new Quaternion();
            // quaternion.SetLookRotation(dir + _rotationShift, Vector3.up);
            // _cameraTransform.rotation = quaternion;

            _cameraTransform.position = _folowing.position + _positionShift;
        }
    }
}