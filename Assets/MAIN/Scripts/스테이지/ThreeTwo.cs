using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeTwo : MonoBehaviour {

    public void Button()
    {
        Invoke("threeTwo", 0.3f);
    }
    private void threeTwo()
    {
        SceneManager.LoadScene("3-2");
    }
}
