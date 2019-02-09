using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour{

    public NetworkManager manager;

    public bool started = false;

    public bool isHost;

    public int gamemode;

    public GameObject ip;
    public InputField input;

    public GameObject error;

    void Start()
    {
        manager = GameObject.Find("_NetworkManager").GetComponent<NetworkManager>();
    }

    public void StartHost()
    {
        if (!NetworkClient.active && !NetworkServer.active && !started)
        {
            if (isHost)
            {
                manager.StartHost();
                started = true;
            }
            else if (input.text != null || input.text != "")
            {
                //manager.StartClient();
                NetworkClient client = manager.StartClient();
                SyncData.gameMode = gamemode;
                client.RegisterHandler(MsgType.Disconnect, ConnectionError);
                started = true;
            }
        }
    }

    public void ConnectionError(NetworkMessage netMsg)
    {
        Debug.Log(netMsg);
        StartCoroutine("Error");
    }

    void Update()
    {
        if (!isHost && !ip.activeSelf)
        {
            ip.SetActive(true);
        }
        else if (isHost && ip.activeSelf)
        {
            ip.SetActive(false);
        }
    }

    IEnumerator Error()
    {
        error.SetActive(true);
        yield return new WaitForSeconds(2f);
        error.SetActive(false);
        manager.StopClient();
    }
}

