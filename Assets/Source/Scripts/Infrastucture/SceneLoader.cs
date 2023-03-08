using System.Collections;
using UnityEngine.SceneManagement;

namespace RTS.Infrastucture
{
    public class SceneLoader
    {
        public const string GameScene = "Game";
        private readonly CoroutineRunner coroutineRunner;

        public SceneLoader(CoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public void LoadSceneAsync(string name)
            => coroutineRunner.StartCoroutine(LoadSceneRoutine(name));

        private IEnumerator LoadSceneRoutine(string name)
        {
            var routine = SceneManager.LoadSceneAsync(name);
            routine.allowSceneActivation = false;
            while (routine.isDone!)
            {
                yield return null;
            }
            routine.allowSceneActivation = true;
        }
    }
}