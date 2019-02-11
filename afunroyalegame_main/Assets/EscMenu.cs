using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour {

    public GameObject escMenu;
    public bool state = false;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            escMenu.SetActive(state);
            transform.GetChild(1).gameObject.SetActive(!state);
            state = !state;
        }
	}
}
