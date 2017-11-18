using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneThree : MonoBehaviour {

    public void Button()
    {
        Invoke("oneThree", 0.3f);
    }
    private void oneThree()
    {
        SceneManager.LoadScene("1-3");
    }
}
