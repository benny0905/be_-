using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{

    public void Button()
    {
        Invoke("startgame", 0.3f);
    }
    private void startgame()
    {
        SceneManager.LoadScene("ChooseLevel");
    }
}