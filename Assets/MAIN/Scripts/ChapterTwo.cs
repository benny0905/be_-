using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterTwo : MonoBehaviour
{

    public void Button()
    {
        Invoke("chapterTwo", 0.3f);
    }
    private void chapterTwo()
    {
        SceneManager.LoadScene("Chap 2");
    }
}
