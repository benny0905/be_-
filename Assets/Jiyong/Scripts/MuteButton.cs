using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {
    public Image image;
    public Sprite mute;
    public Sprite on;

    public void OnMouseDown()
    {
        if (image.sprite == mute)
        {
            image.sprite = on;
        }
        else
        {
            image.sprite = mute;
        }
        
    }
}
