using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chrono : MonoBehaviour {


    public Text txtChrono;
    public Text txtEnd;
    private static float chrono;
    public static float GameTime { get => chrono; }

    private void Start() {
        chrono = 0;
    }

    private void Update() {
        chrono += Time.deltaTime;
        DisplayTime();
    }
    private void DisplayTime() {
        txtChrono.text = string.Format("{0:#0}:{1:00}.{2:0}",
                     Mathf.Floor(chrono / 60),//minutes
                     Mathf.Floor(chrono) % 60,//seconds
                     Mathf.Floor((chrono * 10) % 10));//miliseconds;
        txtEnd.text = "Final time : " + string.Format("{0:#0}:{1:00}.{2:0}",
                     Mathf.Floor(chrono / 60),//minutes
                     Mathf.Floor(chrono) % 60,//seconds
                     Mathf.Floor((chrono * 10) % 10));//miliseconds;
    }
}
