using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Rigidbody dragon;
    [Header("Personalization")]
    public Slider chickenSlider;
    public Slider cowSlider;
    public Slider foodSlider;
    public Slider waterSlider;
    public Text gameType;
    [Header("Succes")]
    public Image imgFlash;
    public Image imgNormal;
    public Image imgHard;
    public Image imgchiken;
    public Image imgCow;
    public Image imgDragon;
    public Image img1min;
    public Image img5Min;
    public Image img25;
    public Image img50;
    public Image img75;
    public Image img100;
    [Header("Panel")]
    public GameObject menuPanel;
    public GameObject optionPanel;
    public GameObject personalizePanel;
    public GameObject succesPanel;
    public enum Panel { menu, personalize, option, succes};
    public int gameTypeId;

    private static UIManager instance;
    private UIManager() { } //block the use of new()

    // Access point
    public static UIManager Instance { get => instance; }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void Start() {
        Initialization();
    }
    private void Update() {
        dragon.angularVelocity = new Vector3(0, (float)-1.2, 0);        
    }
    public void Initialization() {
        chickenSlider.value = GameState.ChickenNb;
        cowSlider.value = GameState.CowNb;
        foodSlider.value = GameState.FoodNb;
        waterSlider.value = GameState.WaterNb;
        ChikenSlider();
        gameTypeId = 1;
        UpdateGameType();
    }

    public void ChikenSlider() {
        gameTypeId = 3;
        UpdateGameType();
    }
    public void CowSlider() {
        gameTypeId = 3;
        UpdateGameType();
    }
    public void FoodSlider() {
        gameTypeId = 3;
        UpdateGameType();
    }
    public void WaterSlider() {
        gameTypeId = 3;
        UpdateGameType();
    }


    public void IncreaseGameType() {
        gameTypeId++;
        if (gameTypeId > 2) {
            gameTypeId = 0;
        }
        UpdateGameType();
    }
    public void DecreaseGameType() {
        gameTypeId--;
        if (gameTypeId <0) {
            gameTypeId = 2;
        }
        UpdateGameType();
    }
    private void UpdateGameType() {
        string gameTypeName = "";
        switch (gameTypeId) {
            case 0:
                chickenSlider.SetValueWithoutNotify(2);
                cowSlider.SetValueWithoutNotify(2);
                waterSlider.SetValueWithoutNotify(0);
                foodSlider.SetValueWithoutNotify(0);
                gameTypeName = "No food";
                break;
            case 1:
                chickenSlider.SetValueWithoutNotify(4);
                cowSlider.SetValueWithoutNotify(4);
                waterSlider.SetValueWithoutNotify(1);
                foodSlider.SetValueWithoutNotify(1);
                gameTypeName = "Normal";
                break;
            case 2:
                chickenSlider.SetValueWithoutNotify(8);
                cowSlider.SetValueWithoutNotify(8);
                waterSlider.SetValueWithoutNotify(1);
                foodSlider.SetValueWithoutNotify(1);
                gameTypeName = "Overcrowded";
                break;
            case 3:
                gameTypeName = "Personalized";
                break;
        }

        gameType.text = gameTypeName;
    }

    public void OpenSucces() {
        UpdateSucces();
        LeaderboardManager.Instance.DisplayLeaderBoard();
        OpenPanel(UIManager.Panel.succes);
    }
    public void OpenOption() {
        OpenPanel(UIManager.Panel.option);
    }
    public void OpenPersonalisation() {
        OpenPanel(UIManager.Panel.personalize);
    }
    public void OpenMenuNoReload() {
        OpenPanel(UIManager.Panel.menu);
    }
    public void OpenMenu() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        GameState.IsGameState = false;
    }
    public void Quit() {
        Application.Quit();
    }
    public void OpenPanel(Panel panel) {
        menuPanel.SetActive(panel.Equals(Panel.menu));
        optionPanel.SetActive(panel.Equals(Panel.option));
        personalizePanel.SetActive(panel.Equals(Panel.personalize));
        succesPanel.SetActive(panel.Equals(Panel.succes));
    }

    private void UpdateSucces() {
        if (Succes.AsPlayedFlash == 1)
            imgFlash.color = Color.white;
        if (Succes.AsPlayedNormal == 1)
            imgNormal.color = Color.white;
        if (Succes.AsPlayedHard == 1)
            imgHard.color = Color.white;
        if (Succes.IsChickenLover == 1)
            imgchiken.color = Color.white;
        if (Succes.IsCowLover == 1)
            imgCow.color = Color.white;
        if (Succes.IsDragonRuler == 1)
            imgDragon.color = Color.white;
        if (Succes.NbGamePlayed >= 25)
            img25.color = Color.white;
        if (Succes.NbGamePlayed >= 50)
            img50.color = Color.white;
        if (Succes.NbGamePlayed >= 75)
            img75.color = Color.white;
        if (Succes.NbGamePlayed >= 100)
            img100.color = Color.white;
        if (Succes.ShortestGame <= 15)
            img1min.color = Color.white;
        if (Succes.LongestGame >= 60)
            img5Min.color = Color.white;
    }

    public void StartGame() {
        GameState.IsGameState = true;
        GameState.ChickenNb = (int)chickenSlider.value;
        GameState.CowNb = (int)cowSlider.value;
        GameState.WaterNb = (int)waterSlider.value;
        GameState.FoodNb = (int)foodSlider.value;
        GameManager.Instance.StartGame();

        SceneManager.LoadScene("GameScene");
    }
}
