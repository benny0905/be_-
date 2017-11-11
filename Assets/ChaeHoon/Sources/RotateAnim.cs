using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour
{
    IEnumerator SmoothRotation()
    {
        for (int num = 0; num < 30; num++)
        {
            yield return new WaitForSeconds(QuadraticFormula(num));
            transform.Rotate(0, 0, 2);
        }
    }

    private float QuadraticFormula(float x)
    {
        float y;
        y = 0.000215f * (x - 15.0f) * (x - 15.0f) + 0.0001f;
        return y;
    }
    private void OnMouseDown()
    {
        StartCoroutine("SmoothRotation");
    }
}