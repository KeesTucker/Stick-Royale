using UnityEngine;
using Mirror;

public class ColourSetterAI : NetworkBehaviour
{
    [SyncVar]
    public Color m_NewColor;

    public Material grappleMat;
    //These are the values that the Color Sliders return
    float m_Red, m_Blue, m_Green;

    public bool local = false;

    void Start()
    {
        local = GetComponent<AISetup>().local;
        if (!local)
        {
            TriggerChildrenColour();
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetColor();
        TriggerChildrenColour();
    }

    [Command]
    public void CmdSetColor()
    {
        m_NewColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );
    }

    public void TriggerChildrenColour()
    {
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }
    }
}