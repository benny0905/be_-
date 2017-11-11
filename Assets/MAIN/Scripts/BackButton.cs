using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    public void Button()
    {
        Invoke("Back", 0.3f);
    }
    private void Back()
    {
        SceneManager.LoadScene("StartScene");
    }
}