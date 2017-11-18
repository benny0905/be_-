using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour
{
    public AudioClip Sfx;
    public AudioSource audioSource;
    public float state = 0;
    public bool StriaghtBlock = false;
    public bool EffectToAnswer = false;
    private bool BlockMoving = false;

    private void Start()
    {
        state = transform.rotation.z;
        int randnum = Random.Range(1, 7);
        for(int n = 0; n < randnum; n++) StartCoroutine("SmoothRotation");
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
        y = 0.000015f * (x - 15.0f) * (x - 15.0f) + 0.0001f; // 0.000215 or 0.000015
        return y;
    }

    private void OnMouseDown()
    {
        StartCoroutine("SmoothRotation");
        audioSource.PlayOneShot(Sfx, 0.2f);
    }

    public bool CheckInitializing()
    {
        return BlockMoving;
    }
}