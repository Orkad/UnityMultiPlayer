using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    private MatchManager _matchManager;
    private readonly List<PlayerSlotUI> _slots = new List<PlayerSlotUI>();
    [SerializeField] private LayoutElement PlayerSlotPrefab;

    [SerializeField] private RectTransform LobbyWindow;
    [SerializeField] private LayoutGroup PlayerContainer;
    [SerializeField] private Text InformationText;
    [SerializeField] private Button DisconnectButton;
    [SerializeField] private Button ReadyButton;

    void Start()
    {
        DisconnectButton.onClick.AddListener(NetworkManager.singleton.StopHost);
        ReadyButton.onClick.AddListener(() => Player.Me.ToogleReady());
        AddEmptySlots(NetworkManager.singleton.matchSize);
    }

    void Update()
    {
        var players = Player.All;
        var playersInSlots = _slots.Select(slot => slot.Player).Where(p => p != null).ToList();
        foreach (var newPlayer in players.Except(playersInSlots))
        {
            var playerSlot = _slots[newPlayer.Number-1];
            playerSlot.Player = newPlayer;
        }

        if (players.All(p => p.State == PlayerState.Ready))
        {
            InformationText.text = "Game will start in " + FindObjectOfType<MatchManager>().CurrentStartTimer.ToString("F0");
        }
        else
        {
            InformationText.text = "";
        }
    }

    public void AddEmptySlots(uint slots)
    {
        _slots.Clear();
        for (int i = 0; i < slots; i++)
        {
            var slot = Instantiate(PlayerSlotPrefab.gameObject, Vector3.zero, Quaternion.identity, PlayerContainer.transform);
            _slots.Add(slot.GetComponent<PlayerSlotUI>());
        }
    }
}
