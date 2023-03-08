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

            BindServices();
        }

        private void BindServices()
        {
            Container.Bind<SceneLoader>().
                AsSingle();

            Container.Bind<IConfigProvider>().
                To<ConfigProvider>().
                AsSingle();

            Container.Bind<IAssetProvider>().
                To<AssetProvider>().
                AsSingle();

            Container.Bind<IGameFactory>().
                To<GameFactory>().
                AsSingle();
        }
    }
}