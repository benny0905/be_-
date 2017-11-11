using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterOne : MonoBehaviour
{

    public void Button()
    {
        Invoke("chapterOne", 0.3f);
    }
    private void chapterOne()
    {
        SceneManager.LoadScene("Chap 1");
    }
}
