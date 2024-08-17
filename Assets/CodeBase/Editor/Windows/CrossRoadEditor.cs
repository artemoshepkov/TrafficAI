using UnityEditor;

namespace CodeBase.Editor.Windows
{
    [CustomEditor(typeof(CrossRoad))]
    public class CrossRoadEditor : BaseRoadEditor
    {
        protected override float GetShift()
        {
            switch (RoadType)
            {
                case RoadType.Road:
                    return 1.42f;
                case RoadType.ShortRoad:
                    return 0.74f;
                case RoadType.Crossroad:
                    return 0.8f;
                case RoadType.TurningRoad:
                    return 0.74f;
            }

            return 0f;
        }
    }
}
