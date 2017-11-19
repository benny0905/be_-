using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour
{
    public AudioClip Sfx;
    public float state = 0;
    public bool FixedBlock = false;
    public bool StriaghtBlock = false;
    public bool EffectToAnswer = false;
    private bool BlockMoving = false;

    private void Start()
    {
        EffectToAnswer = false;
        if (FixedBlock == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            int randnum = Random.Range(1, 7);
            for (int n = 0; n < randnum; n++) StartCoroutine("SmoothRotation");
        }
    }

    public IEnumerator SmoothRotation()
    {
        BlockMoving = true;
        for (int num = 0; num < 24; num++)
        {
            yield return new WaitForSeconds(QuadraticFormula(num));
            transform.Rotate(0, 0, 2.5f);
        }
        BlockMoving = false;
        state += 60;
        state %= 360;
    }

    private float QuadraticFormula(float x)
    {
        float y;
        y = 0.000015f * (x - 15.0f) * (x - 15.0f) + 0.0001f;
        return y;
    }

    private void OnMouseDown()
    {
        if (GameObject.Find("GameManager").GetComponent<RealGame>().AlreadyWon == false && FixedBlock == false)
        {
            StartCoroutine("SmoothRotation");
            GameObject.Find("AudioSource").GetComponent<AudioSource>().PlayOneShot(Sfx, 0.15f);

            /*GameObject EffectButton = GameObject.Find("EffectMute");
            if (EffectButton != null)
            {
                MuteButton MuteScript = EffectButton.GetComponent<MuteButton>();
                if (MuteScript.SoundOn == true) audioSource.PlayOneShot(Sfx, 0.2f);
            }
            else Debug.LogError("으악!!!");*/
        }
    }

    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<RealGame>().enabled == false && FixedBlock == false)
        {
            if (EffectToAnswer == true)
            {
                EffectToAnswer = true;
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                EffectToAnswer = false;
                this.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }

    public bool CheckInitializing()
    {
        return BlockMoving;
    }

}