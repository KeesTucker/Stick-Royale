using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOptions : MonoBehaviour {

    public TMPro.TMP_InputField inputField;

    public void SetName()
    {
        SyncData.name = inputField.text;
        GameObject.Find("Nametag(Clone)").GetComponent<SyncName>().UpdateName();
    }
}
