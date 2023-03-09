using RTS.Configs;
using RTS.Units;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace RTS.Infrastucture
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer diContainer;
        private readonly IAssetProvider assetProvider;
        private readonly IConfigProvider configProvider;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider, IConfigProvider configProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
            this.configProvider = configProvider;
        }

        public async Task<GameObject> CreatePlayer(Transform spawnPoint)
        {
            var prefab = await assetProvider.LoadAsset(AssetPath.Player);
            return diContainer.InstantiatePrefab(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        }

        public async Task<GameObject> CreateUnit(UnitType type, Vector3 spawnPoint, Transform parent)
        {
            var config = configProvider.GetUnitConfig(type);
            var prefab = await assetProvider.LoadAsset(config.UnitTemplate);
            var instance = diContainer.InstantiatePrefab(prefab, spawnPoint, Quaternion.identity, parent);
            instance.GetComponent<UnitMover>().Construct(config);
            return instance;
        }
    }
}