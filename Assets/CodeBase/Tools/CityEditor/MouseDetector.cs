using System;
using UnityEngine;

namespace CodeBase.Tools.CityEditor
{
    public class MouseDetector : MonoBehaviour
    {
        public event Action<Vector3Int> OnMouseDown;
        public event Action<Vector3Int> OnMouseHold;

        public LayerMask MapLayerMask;

        private void Update()
        {
            CheckClickDown();
            CheckClickHold();
        }

        private Vector3Int? RayCastOnMap()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, MapLayerMask))
            {
                var position = Vector3Int.RoundToInt(hit.point);
                return position;
            }

            return null;
        }
        private void CheckClickDown()
        {
            if (Input.GetMouseButtonDown(0)) //  && !EventSystem.current.IsPointerOverGameObject()
            {
                Vector3Int? position = RayCastOnMap();
                if (position != null) 
                    OnMouseDown?.Invoke(position.Value);
            }
        }
        private void CheckClickHold()
        {
            if (Input.GetMouseButton(0)) // && !EventSystem.current.IsPointerOverGameObject()
            {
                Vector3Int? position = RayCastOnMap();
                if (position != null) 
                    OnMouseHold?.Invoke(position.Value);
            }
        }
    }
}