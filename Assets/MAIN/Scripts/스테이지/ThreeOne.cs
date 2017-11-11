using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeOne : MonoBehaviour {

    public void Button()
    {
        Invoke("threeOne", 0.3f);
    }
    private void threeOne()
    {
        SceneManager.LoadScene("3-1");
    }
}
