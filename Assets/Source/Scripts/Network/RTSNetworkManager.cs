using Mirror;
using RTS.Infrastucture;
using UnityEngine;
using Zenject;

public class RTSNetworkManager : NetworkManager
{
    private IGameFactory gameFactory;

    [Inject]
    public void Construct(IGameFactory gameFactory)
    {
        this.gameFactory = gameFactory;
    }

    public override async void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var player = await gameFactory.CreatePlayer(GetStartPosition());
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
