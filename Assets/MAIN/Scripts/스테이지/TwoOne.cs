using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoOne : MonoBehaviour {

    public void Button()
    {
        Invoke("twoOne", 0.3f);
    }
    private void twoOne()
    {
        SceneManager.LoadScene("2-1");
    }
}
