using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MainMenuUI : MonoBehaviour
{
    public Button HostButton;
    public Button JoinButton;
    public InputField PlayerNameInputField;
    public InputField IpAdressInputField;
    public InputField PortInputField;

    void Start()
    {
        if (HostButton != null)
        {
            HostButton.onClick.AddListener(() => GameManager.singleton.HostGame());
        }

        if (JoinButton != null)
        {
            JoinButton.onClick.AddListener(() => GameManager.singleton.JoinGame());
        }

        if (PlayerNameInputField != null)
        {
            PlayerNameInputField.BindToPlayerPrefs("PLAYER_NAME", "Unknown");
        }

        if (IpAdressInputField != null)
        {
            IpAdressInputField.BindToPlayerPrefs("IP_ADRESS", "127.0.0.1");
            IpAdressInputField.Bind(s => GameManager.singleton.networkAddress = s);
        }

        if (PortInputField != null)
        {
            PortInputField.BindToPlayerPrefs("PORT", "7777");
            PortInputField.Bind(s => GameManager.singleton.networkPort = int.Parse(s));
        }
    }
}
