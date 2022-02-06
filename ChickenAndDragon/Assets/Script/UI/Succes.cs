using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succes : MonoBehaviour {

    private static Succes instance;
    // Access point
    public static Succes Instance { get => instance; }
    private Succes() { } //block the use of new()
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);    // delete previous instance
        }
        instance = this;
    }
    public static int AsPlayedFlash { get => PlayerPrefs.GetInt("asPlayedFlash", 0); set => PlayerPrefs.SetInt("asPlayedFlash", value); }
    public static int AsPlayedNormal { get => PlayerPrefs.GetInt("asPlayedNormal", 0); set => PlayerPrefs.SetInt("asPlayedNormal", value); }
    public static int AsPlayedHard { get => PlayerPrefs.GetInt("asPlayedHard", 0); set => PlayerPrefs.SetInt("asPlayedHard", value); }
    public static int IsChickenLover { get => PlayerPrefs.GetInt("isChickenLover", 0); set => PlayerPrefs.SetInt("isChickenLover", value); }
    public static int IsCowLover { get => PlayerPrefs.GetInt("isCowLover", 0); set => PlayerPrefs.SetInt("isCowLover", value); }
    public static int IsDragonRuler { get => PlayerPrefs.GetInt("isDragonRuler", 0); set => PlayerPrefs.SetInt("isDragonRuler", value); }
    public static float ShortestGame { get => PlayerPrefs.GetFloat("shortestGame", 50); set => PlayerPrefs.SetFloat("shortestGame", value); }
    public static float LongestGame { get => PlayerPrefs.GetFloat("longestGame", 0); set => PlayerPrefs.SetFloat("longestGame", value); }
    public static int NbGamePlayed { get => PlayerPrefs.GetInt("nbGamePlayed", 0); set => PlayerPrefs.SetInt("nbGamePlayed", value); }

    public static void IcreaseNbGame() { PlayerPrefs.SetInt("nbGamePlayed", PlayerPrefs.GetInt("nbGamePlayed")+1);}
    public static void GameTime(float time) {
        if(time< PlayerPrefs.GetFloat("shortestGame", 50)) {
            PlayerPrefs.SetFloat("shortestGame", time);
        } else if (time > PlayerPrefs.GetFloat("longestGame")) {
            PlayerPrefs.SetFloat("longestGame", time);
        }
    }
}
