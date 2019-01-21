using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{

    public bool e;
    public bool a;
    public bool aLast;
    public bool s;
    public bool sLast;
    public bool d;
    public bool dLast;
    public bool space;
    public bool r;
    public bool lClick;
    public bool lClickLast;
    public bool rClick;
    public bool rClickLast;
    public bool one;
    public bool two;
    public bool three;
    public bool four;

    public ItemCheck itemCheck;
    public AimShootAI aimShoot;
    public PlayerMovementAI playerMovement;
    public ShootAI shoot;
    public RefrenceKeeperAI refrenceKeeper;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hasAuthority);
        if (Input.GetKeyDown("e"))
        {
            e = true;
        }
        if (Input.GetKeyDown("a"))
        {
            a = true;
        }
        if (Input.GetKeyUp("a"))
        {
            a = false;
        }
        if (Input.GetKeyDown("s"))
        {
            s = true;
        }
        if (Input.GetKeyUp("s"))
        {
            s = false;
        }
        if (Input.GetKeyDown("d"))
        {
            d = true;
        }
        if (Input.GetKeyUp("d"))
        {
            d = false;
        }
        if (Input.GetKeyDown("space") || Input.GetKeyDown("w"))
        {
            space = true;
        }
        if (Input.GetKeyDown("r"))
        {
            r = true;
        }
        if (Input.GetKeyDown("1"))
        {
            one = true;
        }
        if (Input.GetKeyDown("2"))
        {
            two = true;
        }
        if (Input.GetKeyDown("3"))
        {
            three = true;
        }
        if (Input.GetKeyDown("4"))
        {
            four = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            lClick = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lClick = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            rClick = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rClick = false;
        }

        if (hasAuthority)
        {
            if (e)
            {
                e = false;
                CmdSetKey("e", true);
            }
            if (a != aLast)
            {
                aLast = a;
                CmdSetKey("a", a);
            }
            if (d != dLast)
            {
                dLast = d;
                CmdSetKey("d", d);
            }
            if (s != sLast)
            {
                sLast = s;
                CmdSetKey("s", s);
            }
            if (space)
            {
                space = false;
                CmdSetKey("space", true);
            }
            if (r)
            {
                r = false;
                CmdSetKey("r", true);
            }
            if (rClick != rClickLast)
            {
                rClickLast = rClick;
                CmdSetKey("rClick", rClick);
            }
            if (lClick != lClickLast)
            {
                lClickLast = lClick;
                CmdSetKey("lClick", lClick);
            }
            if (one)
            {
                one = false;
                CmdSetKey("one", true);
            }
            if (two)
            {
                two = false;
                CmdSetKey("two", true);
            }
            if (three)
            {
                three = false;
                CmdSetKey("three", true);
            }
            if (four)
            {
                four = false;
                CmdSetKey("four", true);
            }
        }
    }

    [Command]
    public void CmdSetKey(string key, bool state)
    {
        RpcSetKey(key, state);
        if (key == "e")
        {
            itemCheck.e = state;
        }
        if (key == "a")
        {
            playerMovement.a = state;
        }
        if (key == "d")
        {
            playerMovement.d = state;
        }
        if (key == "s")
        {
            playerMovement.s = state;
        }
        if (key == "space")
        {
            playerMovement.space = state;
        }
        if (key == "r")
        {
            shoot.r = state;
        }
        if (key == "lClick")
        {
            shoot.lClick = state;
        }
        if (key == "rClick")
        {
            aimShoot.RClick = state;
        }
        if (key == "one")
        {
            refrenceKeeper.one = state;
        }
        if (key == "two")
        {
            refrenceKeeper.two = state;
        }
        if (key == "three")
        {
            refrenceKeeper.three = state;
        }
        if (key == "four")
        {
            refrenceKeeper.four = state;
        }
    }

    [ClientRpc]
    public void RpcSetKey(string key, bool state)
    {
        if (key == "e")
        {
            itemCheck.e = state;
        }
        if (key == "a")
        {
            playerMovement.a = state;
        }
        if (key == "d")
        {
            playerMovement.d = state;
        }
        if (key == "s")
        {
            playerMovement.s = state;
        }
        if (key == "space")
        {
            playerMovement.space = state;
        }
        if (key == "r")
        {
            shoot.r = state;
        }
        if (key == "lClick")
        {
            shoot.lClick = state;
        }
        if (key == "rClick")
        {
            aimShoot.RClick = state;
        }
        if (key == "one")
        {
            refrenceKeeper.one = state;
        }
        if (key == "two")
        {
            refrenceKeeper.two = state;
        }
        if (key == "three")
        {
            refrenceKeeper.three = state;
        }
        if (key == "four")
        {
            refrenceKeeper.four = state;
        }
    }
}
