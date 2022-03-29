using System.Collections.Generic;
using UnityEngine;

namespace Code.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, Object> _loadedAssets = new Dictionary<string, Object>();

        public T Load<T>(string path) where T : Object
        {
            if(_loadedAssets.TryGetValue(path, out Object asset))
                return asset as T;

            T loadedAsset = Resources.Load<T>(path);
            _loadedAssets.Add(path, loadedAsset);
            return loadedAsset;

        }
    }
}