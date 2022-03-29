using Code.AssetManagement;

namespace Code.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public GameStaticData Data { get; private set; }

        private readonly IAssetProvider _assetProvider;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Load() =>
            Data = _assetProvider.Load<GameStaticData>(AssetPaths.StaticDataPath);
    }
}