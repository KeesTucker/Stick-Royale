using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateValue : MonoBehaviour {

    public int volume = 100;
    public int volumeSFX = 100;
    public TMPro.TMP_Text text;
    public Vector2 res = new Vector2(1920, 1080);
    public bool fullScreen = true;
    public bool isRes = false;
    public TMPro.TMP_Text textFullScreen;
    public int health = 400;

    void Start()
    {
        volume = 100;
        volumeSFX = 100;
        res = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        fullScreen = Screen.fullScreen;
        if (text.text == "400")
        {
            text.text = SyncData.health.ToString();
        }
        if (isRes)
        {
            //text.text = res.x.ToString() + "X" + res.y.ToString();
            textFullScreen.text = fullScreen.ToString();
        }
    }

	public void VolumeDown()
    {
        volume -= 10;
        volume = Mathf.Clamp(volume, 0, 100);
        text.text = volume.ToString();
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void VolumeUp()
    {
        volume += 10;
        volume = Mathf.Clamp(volume, 0, 100);
        text.text = volume.ToString();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void VolumeDownSFX()
    {
        volumeSFX -= 10;
        volumeSFX = Mathf.Clamp(volumeSFX, 0, 100);
        text.text = volumeSFX.ToString();
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void VolumeUpSFX()
    {
        volumeSFX += 10;
        volumeSFX = Mathf.Clamp(volumeSFX, 0, 100);
        text.text = volumeSFX.ToString();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResUp()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.currentResolution.width == Screen.resolutions[i].width && Screen.currentResolution.height == Screen.resolutions[i].height)
            {
                res = new Vector2(Screen.resolutions[Mathf.Clamp(i + 1, 0, Screen.resolutions.Length)].width, Screen.resolutions[Mathf.Clamp(i + 1, 0, Screen.resolutions.Length)].height);
                Screen.SetResolution((int)res.x, (int)res.y, fullScreen);
                text.text = res.x.ToString() + "X" + res.y.ToString();
            }
        }
    }
    public void ResDown()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.currentResolution.width == Screen.resolutions[i].width && Screen.currentResolution.height == Screen.resolutions[i].height)
            {
                res = new Vector2(Screen.resolutions[Mathf.Clamp(i - 1, 0, Screen.resolutions.Length)].width, Screen.resolutions[Mathf.Clamp(i - 1, 0, Screen.resolutions.Length)].height);
                Screen.SetResolution((int)res.x, (int)res.y, fullScreen);
                text.text = res.x.ToString() + "X" + res.y.ToString();
            }
        }
    }
    public void SwitchFullScreen()
    {
        fullScreen = !fullScreen;
        Screen.fullScreen = fullScreen;
        textFullScreen.text = fullScreen.ToString();
    }

    public void HealthDown()
    {
        health -= 100;
        health = Mathf.Clamp(health, 100, 1000);
        text.text = health.ToString();
        SyncData.health = health;
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void HealthUp()
    {
        health += 100;
        health = Mathf.Clamp(health, 100, 1000);
        text.text = health.ToString();
        SyncData.health = health;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
