using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersLeft : MonoBehaviour {

    public PlayerManagement playerManagement;

    public int players;

    public TMPro.TMP_Text text;

	// Use this for initialization
	IEnumerator Start () {
        while (playerManagement == null)
        {
            yield return null;
        }
        players = playerManagement.totalPlayers;
        text.text = players.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerManagement)
        {
            if (players != playerManagement.totalPlayers)
            {
                players = playerManagement.totalPlayers;
                text.text = players.ToString();
            }
        }
	}
}
