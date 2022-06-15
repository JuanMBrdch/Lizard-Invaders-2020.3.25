using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //Distintos Botones para las transiciones a las diferentes escenas
    public void PlayButton(string Level1)
    {
        SceneManager.LoadScene(Level1);
    }

    public void CreditsButton(string credits)
    {
        SceneManager.LoadScene(credits);
    }

    public void RetryButton(string retry)
    {
        SceneManager.LoadScene(retry);
    }

    public void BackButton(string prevScene)
    {
        SceneManager.LoadScene(prevScene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
