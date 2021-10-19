using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Global Variable for IP
    public static string IP;
    // if nothing is inputed
    public static string DefaultIP;

    void Start()
    {
        // For if Input field is left unchecked or empty
        // if you're not bothered type in the input alter this instead
        DefaultIP = "http://192.168.1.35:5000/image.jpg";
    }
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    // For input field Onchange
    public void SaveIpInput(string inputIp) {
       IP = "http://" + inputIp + "/image.jpg";
    }
}
