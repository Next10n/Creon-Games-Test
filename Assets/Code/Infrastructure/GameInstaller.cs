using Code.Core;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Rect Mesh")]
        public int MeshWidth;
        public int MeshHeight;
        public int CutBorderDistance;

        [Header("Cloth")]
        public Material Material;
        public float MinConstraint;
        public float MaxConstraint;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<IRectMeshGenerator>().FromInstance(new RectMeshGenerator(MeshWidth, MeshHeight)).AsSingle();
            Container.Bind<IMeshCutter>().FromInstance(new MeshCutter(CutBorderDistance)).AsSingle();
            Container.Bind<IClothFactory>().FromInstance(new ClothFactory(Material)).AsSingle();
            Container.Bind<IClothConstraintService>().FromInstance(new ClothConstraintService(MinConstraint, MaxConstraint)).AsSingle();
        }
    }
}
