using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

    public InputField pseudoInput;
    public Button saveScoreBtn;
    public Slider life;
    public GameObject endPanel;
    public GameObject gamePanel;
    [Header("Context")]
    public GameObject endChrono;
    public GameObject endLife;
    public GameObject saveScore;
    public GameObject dontSaveScore;

    private SpawnManager spawnMgr;

    private static GameUIManager instance;
    private GameUIManager() { } //block the use of new()

    // Access point
    public static GameUIManager Instance { get => instance; }

    public void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        spawnMgr = SpawnManager.Instance;
    }
    private void Start() {
        SpawnObjects();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Return) && saveScoreBtn.interactable) {
            SaveScore();
        }

        if (Input.GetKeyDown(KeyCode.M)) { //menu
            if (!pseudoInput.isFocused) {
                BackToMenu();
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) { //replay
            if (!pseudoInput.isFocused) {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    public void SpawnObjects() {
        //remove past zones
        zone.Zone.ResetList();
        //spawn
        for (int i = 0; i < GameState.ChickenNb; i++) {
            spawnMgr.SpawnChicken();
        }
        for (int i = 0; i < GameState.CowNb; i++) {
            spawnMgr.SpawnCow();
        }
        for (int i = 0; i < GameState.FoodNb; i++) {
            spawnMgr.SpawnFood();
        }
        for (int i = 0; i < GameState.WaterNb; i++) {
            spawnMgr.SpawnWater();
        }
        spawnMgr.SpawnRedDragon();
    }

    public void SaveScore() {
        float t = Chrono.GameTime;
        string n = pseudoInput.text;
        LeaderboardManager.Instance.AddScoreToLeaderBoard(t, n);
        saveScoreBtn.interactable = false;
        pseudoInput.interactable = false;
    }
    public void InputInvalidate() {
        if (pseudoInput.text.Length >= 2) {
            saveScoreBtn.interactable = true;
        } else {
            saveScoreBtn.interactable = false;
        }
    }

    public void OpenEndPanel() {
        if (life.value == 0) {
            endChrono.SetActive(false);
            endLife.SetActive(true);
            saveScore.SetActive(false);
            dontSaveScore.SetActive(true);
        } else if (!GameState.IsGameClassique) {
            endChrono.SetActive(true);
            endLife.SetActive(false);
            saveScore.SetActive(false);
            dontSaveScore.SetActive(true);
        } else {
            endChrono.SetActive(true);
            endLife.SetActive(false);
            saveScore.SetActive(true);
            dontSaveScore.SetActive(false);
        }
        endPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
    
    public void Restart() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void BackToMenu() {
        GameState.IsGameState = false;
        SceneManager.LoadScene("MenuScene");
    }
}
