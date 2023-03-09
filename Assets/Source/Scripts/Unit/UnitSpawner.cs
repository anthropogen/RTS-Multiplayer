using Mirror;
using RTS.Configs;
using RTS.Infrastucture;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class UnitSpawner : NetworkBehaviour
{
    [SerializeField] private Key spawnKey = Key.S;
    private IGameFactory gameFactory;

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
                CmdCreateUnit(UnitType.Little, hit.point);
            }
        }
    }


    #region Server

    [Server]
    public async Task<GameObject> CreateUnit(UnitType type, Vector3 position)
    {
        var instance = await gameFactory.CreateUnit(type, position, transform);
        NetworkServer.Spawn(instance, connectionToClient);
        return instance;
    }
    #endregion

    #region Client

    [Command]
    public async void CmdCreateUnit(UnitType type, Vector3 position)
        => await CreateUnit(type, position);

    #endregion
}
