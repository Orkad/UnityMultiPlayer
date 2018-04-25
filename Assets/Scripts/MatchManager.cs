using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;


public class MatchManager : NetworkBehaviour
{
    [Header("Sync data")]
    [SyncVar] public MatchState State;
    [SyncVar] public float CurrentStartTimer;

    [Header("Configuration")]
    public float TimerWhenReady = 3.5f;

    [Header("Asset references")]
    public Character CharacterPrefab;

    void Awake()
    {
        CurrentStartTimer = TimerWhenReady;
        ClientScene.RegisterPrefab(CharacterPrefab.gameObject);
    }

    void Update()
    {
        if (isServer)
        {
            var players = FindObjectsOfType<Player>();
            if(players.Length == 0)
                return;
            if (State == MatchState.NotStarted)
            {
                
                if (players.All(p => p.State == PlayerState.Ready))
                {
                    State = MatchState.Starting;
                }
            }
            else if (State == MatchState.Starting)
            {
                if (players.All(p => p.State == PlayerState.Ready))
                {
                    CurrentStartTimer -= Time.deltaTime;
                    if (CurrentStartTimer < 0f)
                    {
                        State = MatchState.Started;
                        foreach (var player in players)
                        {
                            player.State = PlayerState.InGame;
                        }
                        SpawnPlayerCharacters();
                    }
                }
                else
                {
                    State = MatchState.NotStarted;
                    CurrentStartTimer = TimerWhenReady;
                }
            }
        }
    }

    public void SpawnPlayerCharacters()
    {
        var players = FindObjectsOfType<Player>();
        foreach (var player in players)
        {
            var character = Instantiate(CharacterPrefab.gameObject);
            NetworkServer.SpawnWithClientAuthority(character, player.gameObject);
        }
    }
}

public enum MatchState
{
    NotStarted,
    Starting,
    Started,
    Finished
}
