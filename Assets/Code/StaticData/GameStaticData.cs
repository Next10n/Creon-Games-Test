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
        public float Acceleration;

        [Header("Cut")]
        public int MinCutDistance;
        public int MaxCutDistance;

        [Header("Player")]
        public float MoveSpeed;
        public float RotateSpeed;

        [Header("Game")]
        public float ReplayDelay;

        [Header("Input")]
        public float MinSwipe;
        public float SwipeTime;
        [Range(0.5f, 1f)]
        public float SwipePercentage;
    }
}