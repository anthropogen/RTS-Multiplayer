using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace RTS.Infrastucture
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer diContainer;
        private readonly IAssetProvider assetProvider;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
        }

        public async Task<GameObject> CreatePlayer(Transform spawnPoint)
        {
            var prefab = await assetProvider.LoadAsset(AssetPath.Player);
            return diContainer.InstantiatePrefab(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        }
    }
}