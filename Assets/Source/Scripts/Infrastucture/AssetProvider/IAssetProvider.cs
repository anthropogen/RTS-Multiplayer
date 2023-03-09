using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RTS.Infrastucture
{
    public interface IAssetProvider
    {
        Task<GameObject> LoadAsset(string path);
        Task<GameObject> LoadAsset(AssetReference asset);
        void CleanUp();
    }
}