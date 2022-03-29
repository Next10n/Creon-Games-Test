using UnityEngine;

namespace Code.Core
{
    public class RectMeshGenerator : MonoBehaviour
    {
        private readonly int _width;
        private readonly int _height;

        public RectMeshGenerator(int width, int height)
        {
            _width = width;
            _height = height;
        }
        
        public RectMesh CreateMesh()
        {
            return new RectMesh(_width, _height);
        }
    }
}