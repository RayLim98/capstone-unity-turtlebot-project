using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SsvepSpriteFlicker : MonoBehaviour {
    private Color c;
    private SpriteRenderer rend;
    private float starttime;
    public float freq;
    private float timewin;
    private bool logcounter = false;
    private int counter = 0;
    // private string path = "test.txt";
    // private StreamWriter writer;
    private List<string> sinevals;
    // Start is called before the first frame update
    void Start () {
        rend = GetComponent<SpriteRenderer> ();
        c = rend.material.color;
        // Debug.Log(blockmaterial.intensity);
        starttime = Time.time;
        // File.Create ("test.txt").Dispose ();
        // writer = new StreamWriter ("test.txt", true);

        //Write some text to the test.txt file
        // writer
        sinevals = new List<string> ();

    }

    // Update is called once per frame
    void FixedUpdate () {
        if (true) {
            float dt = Time.time - starttime;
            c.a = 0.5f + 0.5f * Mathf.Sin (2f * Mathf.PI * freq * dt);
            // if (freq == 7f) {
            //     sinevals.Add ((c.a).ToString ());
            //     // Debug.Log((c.a).ToString());
            // }
            rend.material.color = c;
            // logcounter = true;
        }
        // if (logcounter) {
        //     if (c.a >= 0.98f) {
        //         counter += 1;
        //     }
        // }
        if (false) {
            starttime = Time.time;
            c.a = 0.05f;
            rend.material.color = c;
            // if (logcounter) {

            //     counter = 0;
            //     string file = @"test.txt";
            //     System.IO.File.WriteAllLines(file,sinevals);
            // }

            // Debug.Log("firing");
        }
    }

    // void OnDisable(){
    //     // string file = @"test.txt";
    //     // System.IO.File.WriteAllLines(file,sinevals);
    //     Debug.Log("Application ending after " + Time.time + " seconds");

    // }
}