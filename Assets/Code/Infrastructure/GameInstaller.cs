using Code.AssetManagement;
using Code.Core;
using Code.StaticData;
using Zenject;

namespace Code.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<IRectMeshGenerator>().To<RectMeshGenerator>().AsSingle();
            Container.Bind<IMeshCutter>().To<MeshCutter>().AsSingle();
            Container.Bind<IClothFactory>().To<ClothFactory>().AsSingle();
            Container.Bind<IClothConstraintService>().To<ClothConstraintService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }
    }
}
