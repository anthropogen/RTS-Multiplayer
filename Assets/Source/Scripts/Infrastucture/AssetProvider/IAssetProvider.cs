using System.Threading.Tasks;
using UnityEngine;

namespace RTS.Infrastucture
{
    public interface IAssetProvider
    {
        Task<GameObject> LoadAsset(string path);
        void CleanUp();
    }
}