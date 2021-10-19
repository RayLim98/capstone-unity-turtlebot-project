using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Screen Manager
    public void QuitGame() {
        // Close TCP connection
        MainMenuScript.stream.Close();
        MainMenuScript.socketConnection.Close();
        // Quits Game
        Application.Quit();
        Debug.Log("Quit");
    }
    //Change Screen based on index. Check build settings to see index
    public void LoadBCIScreen() {
        SceneManager.LoadScene(2);
    }
    public void LoadMainScreen() {
        SceneManager.LoadScene(1);
    }
    public void LoadMenuScreen() {
        SceneManager.LoadScene(0);
    }
}
