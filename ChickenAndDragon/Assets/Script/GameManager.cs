using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private GameManager() { } //block the use of new()

    // Access point
    public static GameManager Instance { get => instance; }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void StartGame() {
        GameState.IsGameState = true;
        if (UIManager.Instance.gameTypeId ==0) {
            Succes.AsPlayedFlash = 1;
        } else if (UIManager.Instance.gameTypeId == 1) {
            Succes.AsPlayedNormal = 1;
        } else if (UIManager.Instance.gameTypeId ==2){
            Succes.AsPlayedHard = 1;
        }
        StartCoroutine(nameof(InitList));
    }

    private IEnumerator InitList() {
        yield return new WaitForSeconds(0.01f);
        an.Annimal.speciesList = SpawnManager.Instance.GetAnnimalList();
        zone.Zone.zonesList = SpawnManager.Instance.GetZonesList();
    }

    public void EndGame() {
        GameUIManager.Instance.OpenEndPanel();
        if (GameState.IsGameState) { 
            Succes.IcreaseNbGame();
            if (GameState.IsGameClassique) {
                Succes.IsDragonRuler = 1;
                Succes.GameTime(Chrono.GameTime);
            }
        }
    }
}
