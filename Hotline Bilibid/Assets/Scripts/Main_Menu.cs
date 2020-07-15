using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        FindObjectOfType<AudioManager>().Play("Select");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        //PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
