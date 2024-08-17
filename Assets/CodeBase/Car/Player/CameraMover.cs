using UnityEngine;

namespace CodeBase.Car
{
    public class CameraMover : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _folowing;

        [Header("Settings")]
        [SerializeField] private Vector3 _shiftFromFolowing;

        private void FixedUpdate() => _cameraTransform.position = _folowing.position + _shiftFromFolowing;
    }
}