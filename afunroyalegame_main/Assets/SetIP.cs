using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class SetIP : MonoBehaviour {

    public NetworkManager manager;

    public TMPro.TMP_InputField mainInputField;

    int result;

    public void ValueChanged()
    {
        manager.networkAddress = mainInputField.text;
    }

    public void PlayerNumValueChanged()
    {
        try
        {
            int number = int.Parse(mainInputField.text);
            result = number;
        }
        catch (FormatException)
        {
            result = 5;
            mainInputField.text = "Thats not a number -_-";
        }
        catch (OverflowException)
        {
            result = 5;
            mainInputField.text = "Thats not a number -_-";
        }
        SyncData.numPlayers = result;
    }
}
