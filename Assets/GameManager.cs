using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkManager
{
    private static GameManager _singleton;
    public new static GameManager singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindObjectOfType<GameManager>();
                if (_singleton == null)
                {
                    _singleton = Instantiate(new GameManager());
                }
            }
            return _singleton;
        }
    }

    public MatchManager MatchManagerPrefab;

    void Start()
    {
        ClientScene.RegisterPrefab(MatchManagerPrefab.gameObject);
    }

    public void HostGame()
    {
        StartHost();
    }

    public void JoinGame()
    {
        StartClient();
    }

    public void LeaveGame()
    {
        StopHost();
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName == onlineScene)
        {
            var obj = Instantiate(MatchManagerPrefab.gameObject);
            obj.name = "Match Manager";
            NetworkServer.Spawn(obj);
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        
        base.OnServerConnect(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    public override void OnStartHost()
    {
        base.OnStartServer();
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
    }
}
