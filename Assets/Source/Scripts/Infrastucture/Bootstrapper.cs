using UnityEngine;
using Zenject;

namespace RTS.Infrastucture
{
    public class Bootstrapper : MonoBehaviour
    {
        private SceneLoader sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        private void Start()
        {
            sceneLoader.LoadSceneAsync(SceneLoader.GameScene);
        }
    }
}