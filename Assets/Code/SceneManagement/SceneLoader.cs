using System;
using System.Collections;
using Code.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private Action _onLoaded;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string scene, Action onLoaded)
        {
            _onLoaded = onLoaded;
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            _coroutineRunner.StartCoroutine(Loading(loadSceneAsync));
        }

        private IEnumerator Loading(AsyncOperation asyncOperation)
        {
            while (asyncOperation.isDone == false)
                yield return null;

            _onLoaded?.Invoke();
            _onLoaded = null;
        }
    }
}