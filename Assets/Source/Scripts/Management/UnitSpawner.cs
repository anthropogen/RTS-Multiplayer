using Mirror;
using RTS.Configs;
using RTS.Infrastucture;
using System.Runtime.InteropServices;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Unit = RTS.Units.Unit;


namespace RTS.Management
{
    public class UnitSpawner : NetworkBehaviour
    {
        [SerializeField] private Key spawnKey = Key.S;
        [SerializeField] private Unit unitTemplate;
        private IGameFactory gameFactory;
        public ReactiveCommand<Unit> UnitSpawned = new ReactiveCommand<Unit>();

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        [ClientCallback]
        private void Update()
        {
            if (Keyboard.current[spawnKey].wasPressedThisFrame)
            {
                var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out var hit, 100))
                {
                    if (isServer)
                        CreateUnit(UnitType.Little, hit.point);
                    else
                        CmdCreateUnit(UnitType.Little, hit.point);
                }
            }
        }

        [Server]
        public async void CreateUnit(UnitType type, Vector3 position)
        {
            var instance = await gameFactory.CreateUnit(type, position, transform);
            NetworkServer.Spawn(instance.gameObject, connectionToClient);
            UnitSpawned.Execute(instance.GetComponent<Unit>());
        }

        #region Server



        #endregion

        #region Client

        [Command]
        public void CmdCreateUnit(UnitType type, Vector3 position)
        {
            CreateUnit(type, position);
        }

        #endregion
    }
}