using UnityEngine;
using Zenject;

namespace RTS.Infrastucture
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner coroutineRunner;
        public override void InstallBindings()
        {
            Container.Bind<CoroutineRunner>().
                FromComponentInHierarchy().
                AsSingle();

            Container.Bind<SceneLoader>().
                AsSingle();
        }
    }
}