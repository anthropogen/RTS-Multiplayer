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

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var player = gameFactory.CreatePlayer(GetStartPosition());
        Debug.Log("create player");
        // player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        // NetworkServer.AddPlayerForConnection(conn, player);
    }
}
