using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncName : NetworkBehaviour {

    public TMPro.TMP_Text text;

    [SyncVar]
    public string name;

    public GameObject parent;

    [SyncVar]
    public GameObject serverParent;

	// Use this for initialization
	IEnumerator Start () {
        if (GameObject.Find("LoadingPlayer"))
        {
            parent = GameObject.Find("LoadingPlayer");
        }
        else
        {
            yield return new WaitForSeconds(1f);
            if (!hasAuthority)
            {
                parent = serverParent;
                text.text = name;
            }
            text.color = parent.GetComponent<ColourSetterAI>().m_NewColor;
        }
    }

    // Update is called once per frame
    void LateUpdate() {
        if (parent)
        {
            if (parent.GetComponent<PlayerSetup>())
            {
                transform.position = parent.transform.position + new Vector3(0, 10, -10);
            }
            else
            {
                transform.position = parent.transform.position + new Vector3(0, 10, 10);
            }
        }
	}

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            CmdUpdateName(SyncData.name);
            UpdateName();
        }
    }

    public void UpdateName()
    {
        name = SyncData.name;
        text.text = name;
    }

    [Command]
    public void CmdUpdateName(string nameS)
    {
        name = nameS;
        RpcUpdateTxt();
        text.text = name;
    }

    [Command]
    public void CmdUpdateParent(GameObject parentObject)
    {
        parent = parentObject;
        serverParent = parent;
        RpcUpdateParent(parent);
    }

    [ClientRpc]
    public void RpcUpdateParent(GameObject parentObject)
    {
        parent = parentObject;
    }

    [ClientRpc]
    public void RpcUpdateTxt()
    {
        text.text = name;
    }
}
