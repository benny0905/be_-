using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour {

    public string LevelName;
    public int RequireStageNum;
    public GameObject Lock;
    private void Start()
    {
        if (PlayerPrefs.GetInt("LevelPassed") >= RequireStageNum)
        {
            Lock.SetActive(false);
        }
    }
    public void Button()
    {
        Invoke("Next", 0.3f);
    }
    private void Next()
    {
        if (PlayerPrefs.GetInt("LevelPassed") >= RequireStageNum)
        {
           SceneManager.LoadScene(LevelName);
        }
          
    }
}
