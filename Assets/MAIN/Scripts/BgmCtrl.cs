using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmCtrl : MonoBehaviour {

    AudioSource source;
    int BgmTF;
    int pre_BgmTF;

    void Start()
    {
        source = GetComponent<AudioSource>();
        BgmTF = PlayerPrefs.GetInt("BGM");
        if (BgmTF == 0)
        {
            source.Play();
        }
        else
        {
            source.Stop();
        }
        pre_BgmTF = BgmTF;
    }

    void Update () {
        BgmTF = PlayerPrefs.GetInt("BGM");
        if (BgmTF == 0)
        {
            if (pre_BgmTF != BgmTF) source.Play();
        }
        else
        {
            if (pre_BgmTF != BgmTF) source.Stop();
        }
        pre_BgmTF = BgmTF;
    }
}
