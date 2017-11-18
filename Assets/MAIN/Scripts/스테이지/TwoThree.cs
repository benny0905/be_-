using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoThree : MonoBehaviour {

    public void Button()
    {
        Invoke("twoThree", 0.3f);
    }
    private void twoThree()
    {
        SceneManager.LoadScene("2-3");
    }
}
