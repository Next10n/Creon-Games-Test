using UnityEngine;
using UnityEngine.Serialization;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "StaticData")]
    public class GameStaticData : ScriptableObject
    {
        [Header("Rect Mesh")]
        public int Width;
        public int Height;
        public int CutBorderDistance;

        [Header("Cloth")]
        public Material Material;
        public float Spread;
        public float MinConstraint;
        public float MaxConstraint;
    }
}