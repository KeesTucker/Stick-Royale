using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class SetIP : MonoBehaviour {

    public NetworkManager manager;

    public InputField mainInputField;

    public void ValueChanged()
    {
        manager.networkAddress = mainInputField.text;
    }
}
