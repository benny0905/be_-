using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour
{
    public float state;

    private void Awake()
    {
        int randnum = Random.Range(0, 6);
        for(int n = 0; n < randnum; n++) StartCoroutine("SmoothRotation");
    }

    public IEnumerator SmoothRotation()
    {
        for (int num = 0; num < 24; num++)
        {
            yield return new WaitForSeconds(QuadraticFormula(num));
            transform.Rotate(0, 0, 2.5f);
        }
        state += 60;
        if (state >= 360) state -= 360;
    }

    private float QuadraticFormula(float x)
    {
        float y;
        y = 0.00015f * (x - 15.0f) * (x - 15.0f) + 0.0001f;
        return y;
    }
    private void OnMouseDown()
    {
        StartCoroutine("SmoothRotation");
    }
}