using System.Threading.Tasks;
using UnityEngine;

namespace RTS.Infrastucture
{
    public interface IGameFactory
    {
        Task<GameObject> CreatePlayer(Transform spawnPoint);
    }
}