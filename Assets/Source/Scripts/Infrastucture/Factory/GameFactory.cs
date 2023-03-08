using System;
using UnityEngine;
using Zenject;

namespace RTS.Infrastucture
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer diContainer;

        public GameFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public GameObject CreatePlayer(Transform spawnPoint)
        {
            throw new NotImplementedException();
        }
    }
}