using UnityEngine;

namespace Code.AssetManagement
{
    public interface IAssetProvider
    {
        T Load<T>(string path) where T : Object;
    }
}