using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeThree : MonoBehaviour {

    public void Button()
    {
        Invoke("threeThree", 0.3f);
    }
    private void threeThree()
    {
        SceneManager.LoadScene("3-3");
    }
}
