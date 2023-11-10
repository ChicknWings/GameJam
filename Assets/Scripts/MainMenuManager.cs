using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGameButton()
    {
        Debug.Log("PLAY GAME");
        SceneManager.LoadScene(1);
    }

    public void QuitGameButton()
    {
        Debug.Log("QUIT GAME");
    }
}
