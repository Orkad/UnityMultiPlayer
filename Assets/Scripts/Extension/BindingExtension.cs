using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class BindingExtension {

    public static void BindToPlayerPrefs(this InputField inputField, string key, string defaultValue = "")
    {
        inputField.text = PlayerPrefs.GetString(key, defaultValue);
        inputField.onEndEdit.AddListener(value => PlayerPrefs.SetString(key, value));
    }

    public static void Bind(this InputField inputField, Action<string> bindingAction)
    {
        inputField.onEndEdit.AddListener(value => bindingAction(value));
    }
}
