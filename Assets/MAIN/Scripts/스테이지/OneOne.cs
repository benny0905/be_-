using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneOne : MonoBehaviour {

    public void Button()
    {
        Invoke("oneOne", 0.3f);
    }
    private void oneOne()
    {
        SceneManager.LoadScene("1-1");
    }
}
