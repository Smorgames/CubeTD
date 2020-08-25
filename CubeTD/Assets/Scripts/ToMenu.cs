using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
    public void Menu()
    {
        Time.timeScale = 1;
        PlayerStats.rounds = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
