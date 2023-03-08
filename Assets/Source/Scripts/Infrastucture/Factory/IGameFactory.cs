using UnityEngine;

namespace RTS.Infrastucture
{
    public interface IGameFactory
    {
        GameObject CreatePlayer(Transform spawnPoint);
    }
}