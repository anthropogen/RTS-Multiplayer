using Mirror;
using RTS.Configs;
using RTS.Infrastucture;
using System.Threading.Tasks;
using UnityEngine;

public class UnitSpawner : NetworkBehaviour
{
    private IGameFactory gameFactory;

    public void Construct(IGameFactory gameFactory)
    {
        this.gameFactory = gameFactory;
    }


    #region Server

    [Server]
    public async Task<GameObject> CreateUnit(UnitType type)
    {
        var instance = await gameFactory.CreateUnit(type, transform.position, transform);
        NetworkServer.Spawn(instance, connectionToClient);
        return instance;
    }
    #endregion

    #region Client

    [Command]
    public async void CmdCreateUnit(UnitType type)
        => await CreateUnit(type);

    #endregion
}
