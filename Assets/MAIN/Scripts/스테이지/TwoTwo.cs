using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoTwo : MonoBehaviour {

    public void Button()
    {
        Invoke("twoTwo", 0.3f);
    }
    private void twoTwo()
    {
        SceneManager.LoadScene("2-2");
    }
}
