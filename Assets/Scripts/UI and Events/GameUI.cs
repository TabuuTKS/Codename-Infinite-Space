using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text CoinText;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject MobileUI;
    [SerializeField] SaveSystem saveSystem;

    [Header("Timers")]
    [SerializeField] BasicTimer LeftSpawnTimer, RightSpawnTimer, UpSpawnTimer;

    [Header("Audios")]
    [SerializeField] AudioSource Click;
    [SerializeField] AudioSource BackClick;
    public AudioSource MeteorSound;
    
    string currentScene;

    #region Singleton
    public static GameUI Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        GameOverUI.SetActive(false);
        GameEvents.ResetDefaults();
        currentScene = SceneManager.GetActiveScene().name;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
    void Update()
    {
        ScoreText.text = $"Score: {GameEvents.score}";
        CoinText.text = $"Coins: {GameEvents.coins}";


        //Speed Multiplying Logic
        if (GameEvents.score - GameEvents.initScore == 100)
        {
            SpeedMultiplyer();
        }

        #if UNITY_EDITOR
            MobileUI.SetActive(!GameOverUI.activeSelf);
        #else
            MobileUI.SetActive((!GameOverUI.activeSelf) ? Input.touchSupported : false);
        #endif
    }

    public void GameOver() { GameOverUI.SetActive(true); }
    public void Replay()
    {
        Click.Play();
        saveSystem.Save();
        GameEvents.ResetDefaults();
        SceneManager.LoadScene(currentScene);
    }

    public void BackToMainMenu()
    {
        BackClick.Play();
        saveSystem.Save();
        SceneManager.LoadScene("MainMenuScene");
    }

    void SpeedMultiplyer()
    {
        GameEvents.initScore += 100;
        GameEvents.SpeedMultiplier += 0.5f;
        GameEvents.WaitTimeDivider -= 0.05f;

        LeftSpawnTimer.WaitTime *= GameEvents.WaitTimeDivider;
        RightSpawnTimer.WaitTime *= GameEvents.WaitTimeDivider;
        UpSpawnTimer.WaitTime *= GameEvents.WaitTimeDivider;
    }
}
