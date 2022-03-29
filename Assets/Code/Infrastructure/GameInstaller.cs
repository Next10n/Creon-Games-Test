using Code.AssetManagement;
using Code.Core;
using Code.Infrastructure.StateMachine;
using Code.SceneManagement;
using Code.StaticData;
using Zenject;

namespace Code.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public CoroutineRunner CoroutineRunner;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<IRectMeshGenerator>().To<RectMeshGenerator>().AsSingle();
            Container.Bind<IMeshCutter>().To<MeshCutter>().AsSingle();
            Container.Bind<IClothFactory>().To<ClothFactory>().AsSingle();
            Container.Bind<IClothConstraintService>().To<ClothConstraintService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IPlayerControlService>().To<PlayerControlService>().AsSingle();
            Container.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(CoroutineRunner).AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}
