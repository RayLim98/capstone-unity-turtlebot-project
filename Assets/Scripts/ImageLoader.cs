using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
public class ImageLoader : MonoBehaviour
{
    // Ip end point to be loaded
    public string ip;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        // If input field is empty runs default script 
        ip = MainMenuScript.IP != null ?  MainMenuScript.IP : MainMenuScript.DefaultIP;
        // Calls the fuction multiple times. Call rate is based the on targeted refresh rate
        InvokeRepeating("RunTexture",0.0167f,0.1f);
    }
    void Update()
    {
     
    }

    // Handles getting texture 
    private void RunTexture()
    {
        GetTexture(ip, (string error) => {
            //On Error
        }, (Texture2D texture2D) => {
            //On Sucess
            Sprite sprite = Sprite.Create(texture2D, new Rect(0,0,texture2D.width,texture2D.height), new Vector2(.5f,.5f));
            spriteRenderer.sprite = sprite;
        });

    }
    private void GetTexture(string url, Action<string> onError, Action<Texture2D> onSucess) {
        StartCoroutine(GetCoroutineTexture(url, onError, onSucess));
    }

    private IEnumerator GetCoroutineTexture(string url, Action<string> onError, Action<Texture2D> onSucess)
    {
        using (UnityWebRequest id = UnityWebRequestTexture.GetTexture(url)) {
            yield return id.SendWebRequest();

            if (id.isNetworkError || id.isHttpError) {
                onError(id.error);
            } else {
                DownloadHandlerTexture downloadHandlerTexture = id.downloadHandler as DownloadHandlerTexture;
                onSucess(downloadHandlerTexture.texture);
            }
        }
    }
}
