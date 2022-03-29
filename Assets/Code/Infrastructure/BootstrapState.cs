using Code.Core;
using Code.Extensions;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly IRectMeshGenerator _rectMeshGenerator;
        private readonly IMeshCutter _meshCutter;
        private readonly IClothFactory _clothFactory;
        private readonly IClothConstraintService _clothConstraintService;
        private readonly IStaticDataService _staticDataService;

        private float _spreadingDistance = 4f;
        
        public BootstrapState(IRectMeshGenerator rectMeshGenerator, IMeshCutter meshCutter, IClothFactory clothFactory, IClothConstraintService clothConstraintService, 
            IStaticDataService staticDataService)
        {
            _rectMeshGenerator = rectMeshGenerator;
            _meshCutter = meshCutter;
            _clothFactory = clothFactory;
            _clothConstraintService = clothConstraintService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadStaticData();
            CreateCloths();
        }

        private void LoadStaticData() =>
            _staticDataService.Load();

        private void CreateCloths()
        {
            RectMesh rectMesh = _rectMeshGenerator.CreateMesh();
            _meshCutter.Cut(rectMesh, out Mesh leftMesh, out Mesh rightMesh, out BrokenLine brokenLine);
            Cloth leftCloth = _clothFactory.Create("Left Cloth", leftMesh, brokenLine);
            Cloth rightCloth = _clothFactory.Create("Right Cloth", rightMesh, brokenLine);
            Spread(leftCloth.transform, rightCloth.transform);
            _clothConstraintService.ApplyConstraints(leftCloth, rightCloth, brokenLine);
        }

        private void Spread(Transform transform1, Transform transform2)
        {
            transform1.AddX(_spreadingDistance / -2f);
            transform2.AddX(_spreadingDistance / 2f);
        }
    }
}