using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardDisplay : MonoBehaviour {

    public VerticalLayoutGroup content;

    private static LeaderboardDisplay instance;
    public static LeaderboardDisplay Instance { get => instance; }
    private LeaderboardDisplay() { } //block the use of new()
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }
    public void DisplayLeaderBoard(Dictionary<float, string> dictionary) {
        List<float> orderedKeys = new List<float>(dictionary.Keys);
        orderedKeys.Sort();

        foreach (float key in orderedKeys) {
            GameObject newLine = new GameObject("Line");
            newLine.transform.SetParent(content.transform, false);
            string oneTime = string.Format("{0:#0}:{1:00}.{2:0}",
                     Mathf.Floor(key / 60),//minutes
                     Mathf.Floor(key) % 60,//seconds
                     Mathf.Floor((key * 10) % 10));//miliseconds;
            newLine.AddComponent<Text>().text = string.Format("{0,6} - - {1,-20}", oneTime, dictionary[key]);
            newLine.GetComponent<Text>().fontSize = 25;
            newLine.GetComponent<Text>().color = Color.white;
            newLine.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            RectTransform rt = newLine.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(600, 30);
        }
    }
}
