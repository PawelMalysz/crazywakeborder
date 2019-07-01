using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("score"));

        GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = " "+PlayerPrefs.GetInt("score");
    }

    public void Again()
    {
        Debug.Log("dziala");
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
