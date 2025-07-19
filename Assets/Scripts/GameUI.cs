using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text CoinText;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject MobileUI;

    [SerializeField] BasicTimer LeftSpawnTimer, RightSpawnTimer, UpSpawnTimer;
    
    string currentScene;
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
        GameEvents.ResetDefaults();
        SceneManager.LoadScene(currentScene);
    }

    public void BackToMainMenu()
    {
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
