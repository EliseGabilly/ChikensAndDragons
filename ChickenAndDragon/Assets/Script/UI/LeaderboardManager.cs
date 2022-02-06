using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {

    public Dictionary<float, string> dictionary;

    private static LeaderboardManager instance;
    // Access point
    public static LeaderboardManager Instance { get => instance; }
    private LeaderboardManager() { } //block the use of new()
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveLeaderBoard() {
        SaveSysteme.SaveLeaderboard(this);
    }
    public void SaveEmptyLeaderBoard() {
        dictionary = new Dictionary<float, string>();
        SaveSysteme.SaveLeaderboard(this);
    }

    public void LoadLeaderBoard() {
        LeaderboardData data = SaveSysteme.LoadLeaderBoard();
        dictionary = new Dictionary<float, string>();
        for (int i = 0; i < data.times.Length; i++) {
            dictionary.Add(data.times[i], data.names[i]);
        }
    }
    public void DisplayLeaderBoard() {
        if (dictionary == null) {
            LeaderboardManager.Instance.LoadLeaderBoard();
        }
        LeaderboardDisplay.Instance.DisplayLeaderBoard(dictionary);
    }

        public void AddScoreToLeaderBoard(float time, string name){
        if(dictionary == null) {
            LoadLeaderBoard();
        }
        try {
            dictionary.Add(time, name);
            SaveLeaderBoard();
        } catch (ArgumentException) {
            AddScoreToLeaderBoard(time+ 1e-6F, name);
        }
    }

}
