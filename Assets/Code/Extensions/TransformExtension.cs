using UnityEngine;

namespace Code.Extensions
{
    public static class TransformExtension
    {
        public static void AddX(this Transform transform, float x)
        {
            transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
        }
    }
}