using Code.AssetManagement;
using UnityEngine;

namespace Code.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public GameStaticData Data { get; private set; }

        public void Load()
        {
            Data = Resources.Load<GameStaticData>(AssetPaths.StaticData);
        }
    }
}