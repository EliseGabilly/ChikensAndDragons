using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSysteme {
    public static void SaveLeaderboard(LeaderboardManager leaderboard) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/leaderboardInfo.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LeaderboardData data = new LeaderboardData(leaderboard);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LeaderboardData LoadLeaderBoard() {
        string path = Application.persistentDataPath + "/leaderboardInfo.fun";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LeaderboardData data = formatter.Deserialize(stream) as LeaderboardData;
            stream.Close();
            return data;
        } else {
            Debug.LogError("file not fount at " + path);
            LeaderboardManager.Instance.SaveEmptyLeaderBoard();
            return LoadLeaderBoard();
        }
    }
}
