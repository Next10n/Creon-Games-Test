using Code.Core;
using UnityEngine;

namespace Code.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly IRectMeshGenerator _rectMeshGenerator;
        private readonly IMeshCutter _meshCutter;
        private readonly IClothFactory _clothFactory;
        private readonly IClothConstraintService _clothConstraintService;


        public BootstrapState(IRectMeshGenerator rectMeshGenerator, IMeshCutter meshCutter, IClothFactory clothFactory, IClothConstraintService clothConstraintService)
        {
            _rectMeshGenerator = rectMeshGenerator;
            _meshCutter = meshCutter;
            _clothFactory = clothFactory;
            _clothConstraintService = clothConstraintService;
        }

        public void Enter()
        {

            RectMesh rectMesh = _rectMeshGenerator.CreateMesh();
            
            _meshCutter.Cut(rectMesh, out Mesh leftMesh, out Mesh rightMesh, out BrokenLine brokenLine);

            Cloth leftCloth = _clothFactory.Create("Left Cloth", leftMesh, brokenLine);
            Cloth rightCloth = _clothFactory.Create("Right Cloth", rightMesh, brokenLine);

            _clothConstraintService.ApplyConstraints(leftCloth, rightCloth, brokenLine);

        }
        
    }
}