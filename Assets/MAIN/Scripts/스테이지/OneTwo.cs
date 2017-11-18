using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneTwo : MonoBehaviour {

    public void Button()
    {
        Invoke("oneTwo", 0.3f);
    }
    private void oneTwo()
    {
        SceneManager.LoadScene("1-2");
    }
}
