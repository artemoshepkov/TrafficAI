using UnityEditor;

namespace CodeBase.Editor.Windows
{
    [CustomEditor(typeof(ShortRoad))]
    public class ShortRoadEditor : BaseRoadEditor
    {
        protected override float GetShift()
        {
            switch (RoadType)
            {
                case RoadType.Road:
                    return 1.36f;
                case RoadType.ShortRoad:
                    return 0.68f;
                case RoadType.Crossroad:
                    return 0.74f;
                case RoadType.TurningRoad:
                    return 0.68f;
            }

            return 0f;
        }
    }
}
