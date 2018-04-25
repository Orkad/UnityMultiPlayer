using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlotUI : MonoBehaviour
{
    [Header("References")]
    public Image PlayerColorImageRef;
    public Text PlayerNameTextRef;
    public Text PlayerPingTextRef;

    [Header("Configuration")]
    [SerializeField] private Color _notReadyPlayerColor = Color.grey;
    [SerializeField] private Color _readyPlayerColor = Color.cyan;
    [SerializeField] private Color _inGamePlayerColor = Color.green;

    [Header("In Game")]
    public Player Player;
    


    private Color _defaultSlotColor;

    void Start()
    {
        _defaultSlotColor = PlayerColorImageRef.color;
    }

    void Update()
    {
        if (Player != null)
        {
            PlayerNameTextRef.text = Player.PlayerName + (Player.isLocalPlayer ? " (you)" : "");
            PlayerColorImageRef.color = Player.State == PlayerState.Ready ? _readyPlayerColor : 
                Player.State == PlayerState.NotReady ?_notReadyPlayerColor : _inGamePlayerColor;
            PlayerPingTextRef.text = Player.Ping.ToString() + " ms";
        }
        else
        {
            PlayerNameTextRef.text = "(Empty)";
            PlayerColorImageRef.color = _defaultSlotColor;
            PlayerPingTextRef.text = "";
        }
    }
}
