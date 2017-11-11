using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    IEnumerator SmoothRotation()
    {
        for (int num = 0; num < 24; num++)
        {
            yield return new WaitForSeconds(QuadraticFormula(num));
            transform.Rotate(0, 0, 2.5f);
        }
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