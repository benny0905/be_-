using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour {

	void Start () {
        Invoke("LoadTitle", 6.5f);
	}

    private void LoadTitle()
    {
        SceneManager.LoadScene("StartScene");
    }
}
