using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSetSliders : MonoBehaviour {

    public Transform r;
    public Transform g;
    public Transform b;

    public ColourSetterLoad colourSetter;

    public TMPro.TMP_Text nameTag;

    void Start()
    {
        nameTag = GameObject.Find("Nametag(Clone)").transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>();
        UpdateColor();
    }

    public void UpdateColor()
    {
        Color color = new Color(1 + r.localPosition.x, 1 + g.localPosition.x, 1 + b.localPosition.x, 1);
        colourSetter.SetColor(color);
        SyncData.color = color;
        nameTag.color = color;
    }
}
