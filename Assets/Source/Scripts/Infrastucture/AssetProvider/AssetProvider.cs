using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RTS.Infrastucture
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> cashe = new Dictionary<string, AsyncOperationHandle>();

        ~AssetProvider()
        {
            CleanUp();
        }

        public async Task<GameObject> LoadAsset(string path)
        {
            if (cashe.TryGetValue(path, out var handle))
                return handle.Result as GameObject;
            return await LoadWithCache(path);
        }

        public async Task<GameObject> LoadAsset(AssetReference asset)
        {
            if (cashe.TryGetValue(asset.AssetGUID, out var handle))
                return handle.Result as GameObject;
            return await LoadWithCache(asset.AssetGUID);
        }

        public void CleanUp()
        {
            foreach (var handle in cashe.Values)
                Addressables.Release(handle);
        }

        private async Task<GameObject> LoadWithCache(string path)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(path);
            handle.Completed += h =>
            {
                cashe[path] = h;
            };
            return await handle.Task;
        }
    }
}