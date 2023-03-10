using Mirror;
using RTS.Infrastucture;
using RTS.Management;
using RTS.UI;
using System.Threading.Tasks;
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
        GameObject player = await CreatePlayer();
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player.gameObject);
    }

    private async Task<GameObject> CreatePlayer()
    {
        var player = await gameFactory.CreatePlayer(GetStartPosition());
        var playerCanvas = await gameFactory.CreatePlayerCanvas();
        player.GetComponent<UnitSelector>().Construct(playerCanvas.GetComponentInChildren<SelectionAreaView>());
        return player;
    }
}
