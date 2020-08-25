using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    public SceneFader sceneFader;
    public string sceneMenuName = "MainMenu";

    private void OnEnable()
    {
        roundsText.text = (PlayerStats.rounds - 1).ToString();
        Time.timeScale = 0;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        PlayerStats.rounds = 0;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        PlayerStats.rounds = 0;
        sceneFader.FadeTo(sceneMenuName);
    }
}
