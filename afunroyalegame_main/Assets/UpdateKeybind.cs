using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateKeybind : MonoBehaviour {

    public TMPro.TMP_InputField text;
    public TMPro.TMP_Text placeHolder;
    KeyCode keycode;
    Event e;
    private bool updating;

    void Start()
    {
        updating = false;
        if (placeHolder.text == "A")
        {
            text.text = SyncData.a.ToString();
        }
        else if (placeHolder.text == "D")
        {
            text.text = SyncData.d.ToString();
        }
        else if (placeHolder.text == "SPACE")
        {
            text.text = SyncData.space.ToString();
        }
        else if (placeHolder.text == "F")
        {
            text.text = SyncData.f.ToString();
        }
        else if (placeHolder.text == "R")
        {
            text.text = SyncData.r.ToString();
        }
        else if (placeHolder.text == "I")
        {
            text.text = SyncData.i.ToString();
        }
    }

    void OnGUI()
    {
        e = Event.current;
        if (e.isKey)
        {
            keycode = e.keyCode;
            updating = false;
        }
    }

    public void WrapperA()
    {
        StartCoroutine("UpdateA");
    }

	IEnumerator UpdateA()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.a = keycode;
                Debug.Log(keycode.ToString());
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.a.ToString().ToUpper();
        }
    }
    public void WrapperS()
    {
        StartCoroutine("UpdateS");
    }

    IEnumerator UpdateS()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.s = keycode;
                Debug.Log(keycode.ToString());
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.s.ToString().ToUpper();
        }
    }
    public void WrapperD()
    {
        StartCoroutine("UpdateD");
    }

    IEnumerator UpdateD()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.d = keycode;
                Debug.Log(keycode.ToString());
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.d.ToString().ToUpper();
        }
    }
    public void WrapperSpace()
    {
        StartCoroutine("UpdateSpace");
    }

    IEnumerator UpdateSpace()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.space = keycode;
                Debug.Log(keycode.ToString());
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.space.ToString().ToUpper();
        }
    }
    public void WrapperR()
    {
        StartCoroutine("UpdateR");
    }

    IEnumerator UpdateR()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.r = keycode;
                Debug.Log(keycode.ToString());
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.r.ToString().ToUpper();
        }
    }
    public void WrapperI()
    {
        StartCoroutine("UpdateI");
    }

    IEnumerator UpdateI()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.i = keycode;
                Debug.Log(keycode.ToString());
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.i.ToString().ToUpper();
        }
    }
    public void WrapperF()
    {
        StartCoroutine("UpdateF");
    }

    IEnumerator UpdateF()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.f = keycode;
                Debug.Log(keycode.ToString());
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.f.ToString().ToUpper();
        }
    }

    public void Clear()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
