using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;

    public string menuSceneName = "MainMenu";
    public string nextLevel = "level02";
    public int nextLevelIndex = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", nextLevelIndex);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        PlayerPrefs.SetInt("levelReached", nextLevelIndex);
        sceneFader.FadeTo(menuSceneName);
    }
}
