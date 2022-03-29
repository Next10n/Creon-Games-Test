using Code.Core;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure
{
    public class ClothFactory : IClothFactory
    {
        private readonly IStaticDataService _staticDataService;

        public ClothFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public Cloth Create(string name, Mesh mesh, BrokenLine brokenLine)
        {
            GameObject gameObject = new GameObject(name);
            SkinnedMeshRenderer skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material = _staticDataService.Data.Material;
            skinnedMeshRenderer.sharedMesh = mesh;
            Cloth cloth = gameObject.AddComponent<Cloth>();

            return cloth;
        }
    }
}