using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterThree : MonoBehaviour
{

    public void Button()
    {
        Invoke("chapterThree", 0.3f);
    }
    private void chapterThree()
    {
        SceneManager.LoadScene("Chap 3");
    }
}
