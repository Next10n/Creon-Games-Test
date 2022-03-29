using Code.Core;
using UnityEngine;

namespace Code.Infrastructure
{
    public class ClothFactory : IClothFactory
    {
        private readonly Material _material;

        public ClothFactory(Material material)
        {
            _material = material;
        }


        public Cloth Create(string name, Mesh mesh, BrokenLine brokenLine)
        {
            GameObject gameObject = new GameObject(name);
            // gameObject.AddComponent<MeshFilter>().mesh = mesh;
            SkinnedMeshRenderer skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material = _material;
            skinnedMeshRenderer.sharedMesh = mesh;
            Cloth cloth = gameObject.AddComponent<Cloth>();

            return cloth;
        }
    }
}