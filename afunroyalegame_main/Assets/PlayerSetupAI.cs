using UnityEngine;
using Mirror;

public class PlayerSetupAI : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    Behaviour[] componentsToEnableOnLocal;
    [SerializeField]
    Behaviour[] componentsToDisableOnLocal;
    [SerializeField]
    Behaviour[] componentsToEnable;

    Camera sceneCamera;

    [SyncVar]
    public Color m_NewColor;

    [SyncVar]
    public bool jetOn;

    [SyncVar]
    public bool dropped = false;


    [SyncVar]
    public GameObject parent;

    public GameObject ragdollPlaceholder;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            for (int i = 0; i < componentsToEnable.Length; i++)
            {
                componentsToEnable[i].enabled = true;
            }
            ragdollPlaceholder.SetActive(true);
        }
        else
        {
            for (int i = 0; i < componentsToEnableOnLocal.Length; i++)
            {
                componentsToEnableOnLocal[i].enabled = true;
            }
            for (int i = 0; i < componentsToDisableOnLocal.Length; i++)
            {
                componentsToDisableOnLocal[i].enabled = true;
            }
            ragdollPlaceholder.gameObject.SetActive(false);
            m_NewColor = parent.GetComponent<ColourSetter>().m_NewColor;
            parent.GetComponentInChildren<SwitchWeaponAI>().Setup(gameObject);
            parent.GetComponentInChildren<ShootAI>().Setup(gameObject);
            parent.GetComponentInChildren<AimShootAI>().Setup(gameObject);
            /*sceneCamera = Camera.main;
            gameObject.name = "Local";
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            charCam.gameObject.SetActive(true);*/
        }
    }

    /*void Update()
    {
        if (isLocalPlayer)
        {
            CmdSyncJet(GameObject.Find("Local").transform.Find("Physics Animator").GetComponent<PlayerMovement>().jetOn);
        }
    }

    [Command]
    void CmdSyncJet(bool jetState)
    {
        jetOn = jetState;
    }
    */
    /*void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }*/

}
