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
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }
    }

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            CmdSetColor();
        }
    }

    [Command]
    public void CmdSetColor()
    {
        m_NewColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );

        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }

        RpcTriggerChildrenColour(m_NewColor);
    }

    [ClientRpc]
    public void RpcTriggerChildrenColour(Color color)
    {
        m_NewColor = color;
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }
    }
}