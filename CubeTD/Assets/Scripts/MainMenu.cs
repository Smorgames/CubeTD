using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "1_Level";

    public SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //        PlayerPrefs.SetInt("levelReached", 1);
    //}
}
