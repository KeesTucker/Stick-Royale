using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour {

    public GameObject escMenu;
    public bool state = false;
    public bool esc;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            esc = true;
        }
        if (esc)
        {
            esc = false;
            escMenu.SetActive(state);
            if (transform.childCount > 1)
            {
                transform.GetChild(1).gameObject.SetActive(!state);
                GameObject.Find("LoadingPlayer").GetComponent<LoadingControl>().enabled = !state;
            }
            else
            {

                transform.GetChild(0).gameObject.SetActive(!state);
                GameObject.Find("LocalPlayer").GetComponent<PlayerControl>().enabled = state;
            }

            state = !state;
        }
	}
}
