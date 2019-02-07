using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingControl : MonoBehaviour
{
    public bool a;
    public bool aLast;
    public bool s;
    public bool sLast;
    public bool d;
    public bool dLast;
    public bool space;
    public bool shift;
    public bool shiftLast;
    public bool lClick;
    public bool lClickLast;
    public bool rClick;
    public bool rClickLast;

    public AimShootAI aimShoot;
    public PlayerMovementAI playerMovement;
    public ShootAI shoot;

    void Start()
    {
        shift = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (a != aLast)
        {
            aLast = a;
            SetKey("a", a);
        }
        if (d != dLast)
        {
            dLast = d;
            SetKey("d", d);
        }
        if (s != sLast)
        {
            sLast = s;
            SetKey("s", s);
        }
        if (space)
        {
            space = false;
            SetKey("space", true);
        }
        if (shift != shiftLast)
        {
            shiftLast = shift;
            SetKey("shift", shift);
        }
        if (rClick != rClickLast)
        {
            rClickLast = rClick;
            SetKey("rClick", rClick);
        }
        if (lClick != lClickLast)
        {
            lClickLast = lClick;
            SetKey("lClick", lClick);
        }
    }

    public void SetKey(string key, bool state)
    {
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
        if (key == "shift")
        {
            playerMovement.shift = state;
        }
        if (key == "lClick")
        {
            shoot.lClick = state;
        }
        if (key == "rClick")
        {
            aimShoot.RClick = state;
        }
    }
}
