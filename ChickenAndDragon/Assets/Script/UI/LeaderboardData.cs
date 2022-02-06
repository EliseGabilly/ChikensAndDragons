using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderboardData {
    public string[] names;
    public float[] times;

    public LeaderboardData (LeaderboardManager leaderboard) {
        times = new float[leaderboard.dictionary.Keys.Count];
        leaderboard.dictionary.Keys.CopyTo(times, 0);

        names = new string[leaderboard.dictionary.Values.Count];
        leaderboard.dictionary.Values.CopyTo(names, 0);
    }
}
