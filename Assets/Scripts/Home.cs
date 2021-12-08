using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public void ContinueBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void LevelsBtn()
    {
        SceneManager.LoadScene("level");
    }

    public void QuitBtn()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Home");
    }
 
}
