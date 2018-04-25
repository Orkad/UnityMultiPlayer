using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [Header("Sync data")]
    [SyncVar] [SerializeField] private string _playerName;
    [SyncVar] [SerializeField] private PlayerState _state;
    [SyncVar] [SerializeField] private int _number;
    [SyncVar] [SerializeField] private int _ping;


    public static Player Me;
    private static List<Player> PlayerList = new List<Player>();
    public static Player[] All { get { return PlayerList.ToArray(); } }

    public string PlayerName // client
    {
        get { return _playerName; }
        set { CmdChangePlayerName(value); }
    }
    public PlayerState State // client
    {
        get { return _state; }
        set
        {
            CmdChangePlayerState(value);
        }
    }
    public int Number // server
    {
        get { return _number; }
        set { _number = value; }
    }
    public int Ping //server
    {
        get { return _ping; }
        set { _ping = value; }
    }

    void Start()
    {
        PlayerList.Add(this);
        if (isServer)
        {
            Number = PlayerList.Count;
        }
        if (isLocalPlayer)
        {
            Me = this;
            CmdChangePlayerName(PlayerPrefs.GetString("PLAYER_NAME"));
        }
    }

    void Update()
    {
        if (isServer)
        {
            Ping = NetworkClient.allClients[0].GetRTT();
        }
    }

    void OnDestroy()
    {
        PlayerList.Remove(this);
    }

    public void ToogleReady()
    {
        switch (State)
        {
            case PlayerState.Ready:
                State = PlayerState.NotReady;
                break;
            case PlayerState.NotReady:
                State = PlayerState.Ready;
                break;
        }
    }

    [Command]
    void CmdChangePlayerState(PlayerState playerState)
    {
        _state = playerState;
    }

    [Command]
    void CmdChangePlayerName(string playerName)
    {
        name = _playerName = playerName;
    }
}

public enum PlayerState
{
    NotReady,
    Ready,
    InGame
}
