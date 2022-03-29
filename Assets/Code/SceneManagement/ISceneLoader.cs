using System;

namespace Code.SceneManagement
{
    public interface ISceneLoader
    {
        void Load(string scene, Action onLoaded = null);
    }
}