using RTS.Configs;
using System.Threading.Tasks;
using UnityEngine;

namespace RTS.Infrastucture
{
    public interface IGameFactory
    {
        Task<GameObject> CreatePlayer(Transform spawnPoint);

        Task<GameObject> CreateUnit(UnitType type, Vector3 spawnPoint, Transform parent);
    }
}