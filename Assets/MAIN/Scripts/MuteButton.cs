using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {
    public Image image;
    public Sprite mute;
    public Sprite on;
    public string Key;
    public bool SoundOn = true;

    void Start()
    {
        if (PlayerPrefs.GetInt(Key) == 0)
        {
            image.sprite = on;
            SoundOn = true;
        }
        else
        {
            image.sprite = mute;
            SoundOn = false;
        }
    }

    public void OnMouseDown()
    {
        PlayerPrefs.SetInt(Key, (PlayerPrefs.GetInt(Key) + 1) % 2);
        PlayerPrefs.Save();

        if (PlayerPrefs.GetInt(Key) == 0)
        {
            image.sprite = on;
            SoundOn = true;
        }
        else
        {
            image.sprite = mute;
            SoundOn = false;
        }
    }
}
