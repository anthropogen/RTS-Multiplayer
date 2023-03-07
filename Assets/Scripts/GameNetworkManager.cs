using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class GameNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("client connected");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Debug.Log(numPlayers);
    }
}
