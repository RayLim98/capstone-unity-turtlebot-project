using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
public class ImageLoader : MonoBehaviour
{
    // Start is called before the first frame updateo
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        RunTexture();
        InvokeRepeating("RunTexture",0.0167f,0.1f);
    }
    // Update is called once per frame
    void Update()
    {
     
    }

    private void RunTexture()
    {
        string url = "http://192.168.1.35:5000/image.jpg";
 

        GetTexture(url, (string error) => {
            //Error
            Debug.Log("Error: " + error);
        }, (Texture2D texture2D) => {
            //Sucess
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
