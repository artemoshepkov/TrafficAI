using System.Linq;
using CodeBase.Infrastructure.Services;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor.Windows
{
    public enum ObjectType
    {
        Road,
        House,
    }
    public enum TurningRoadType
    {
        Left,
        Right
    }
    
    public abstract class BaseRoadEditor : UnityEditor.Editor
    {
        private RoadFactory _roadFactory = ServiceLocator.GetService<RoadFactory>();
        private AssetProvider _assetProvider = ServiceLocator.GetService<AssetProvider>();
        
        public RoadType RoadType;
        public TurningRoadType TurningRoadType;
        public ObjectType ObjectType;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Component road = (Component)target;
            
            RoadType = (RoadType)EditorGUILayout.EnumPopup("Road Type", RoadType);
            if (RoadType == RoadType.TurningRoad)
            {
                TurningRoadType = (TurningRoadType)EditorGUILayout.EnumPopup("Turning Road Type", TurningRoadType);
            }
            ObjectType = (ObjectType)EditorGUILayout.EnumPopup("Object Type", ObjectType);
            
            var position = road.GetComponent<Transform>().position;

            float shift = GetShift();

            Quaternion rotation = Quaternion.identity;

            if (GUILayout.Button("Add object to left"))
            {
                if (RoadType == RoadType.TurningRoad)
                {
                    if (TurningRoadType == TurningRoadType.Left)
                    {
                        rotation.eulerAngles = new Vector3(0f, -90f, 0f);
                        position.x += 0.04f;
                    }
                    if (TurningRoadType == TurningRoadType.Right)
                        rotation.eulerAngles = new Vector3(0f, 180f, 0f);
                }
                HandleInterface(new Vector3(position.x, position.y, position.z + -shift), rotation);
            }
            if (GUILayout.Button("Add object to right"))
            {
                if (RoadType == RoadType.TurningRoad)
                {
                    if (TurningRoadType == TurningRoadType.Left)
                    {
                        rotation.eulerAngles = new Vector3(0f, 90f, 0f);
                        position.x += -0.04f;
                    }
                    if (TurningRoadType == TurningRoadType.Right)
                        rotation.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                HandleInterface(new Vector3(position.x, position.y, position.z + shift), rotation);
            }
            if (GUILayout.Button("Add object to top"))
            {
                if (RoadType != RoadType.TurningRoad)
                {
                    rotation.eulerAngles = new Vector3(0f, 90f, 0f);
                }
                else
                {
                    if (TurningRoadType == TurningRoadType.Left)
                    {                        
                        rotation.eulerAngles = new Vector3(0f, 180f, 0f);
                        position.z += 0.04f;
                    }
                        rotation.eulerAngles = new Vector3(0f, 180f, 0f);
                    if (TurningRoadType == TurningRoadType.Right)
                        rotation.eulerAngles = new Vector3(0f, 90f, 0f);
                }
                HandleInterface(new Vector3(position.x + shift, position.y, position.z), rotation);
            }
            if (GUILayout.Button("Add object to bottom"))
            {
                if (RoadType != RoadType.TurningRoad)
                {
                    rotation.eulerAngles = new Vector3(0f, 90f, 0f);
                }
                else
                {
                    if (TurningRoadType == TurningRoadType.Left)
                    {
                        rotation.eulerAngles = new Vector3(0f, 0f, 0f);
                        position.z += -0.04f;
                    }
                    if (TurningRoadType == TurningRoadType.Right)
                        rotation.eulerAngles = new Vector3(0f, 270f, 0f);
                }
                HandleInterface(new Vector3(position.x + -shift, position.y, position.z), rotation);
            }
        }

        private void HandleInterface(Vector3 objectPos, Quaternion quaternion)
        {
            switch (ObjectType)
            {
                case ObjectType.Road:
                    // GameObject road = _roadFactory.InstantiateRoad(RoadType, objectPos, quaternion);
                    break;
                case ObjectType.House:
                    _assetProvider.Instantiate(GameConfig.HousePath.First(), objectPos + new Vector3(0f, -0.235f, 0f));
                    break;
            }
        }
        
        private void HandleInterface(Vector3 objectPos) => HandleInterface(objectPos, Quaternion.identity);

        protected abstract float GetShift();
    }
}
