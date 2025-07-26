using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class StartMenuUI : MonoBehaviour
{
    [Header("StartMenu")]
    [SerializeField] Animator camAnimator;
    [SerializeField] GameObject StartMenuCanvas;
    [SerializeField] GameObject ControlsCanvas;

    [Header("ShipSelectMenu")]
    [SerializeField] SaveSystem saveSystem;
    public ShipObject[] shipObjects;
    [SerializeField] GameObject ShipSelectMenuCanvas;
    [SerializeField] GameObject Spaceship;
    [SerializeField] Button NextShipBTN;
    [SerializeField] Button PrevShipBTN;
    [SerializeField] GameObject EquipBTN;
    [SerializeField] GameObject PlayBTN;
    [SerializeField] GameObject BuyBTN;
    [SerializeField] GameObject InsufficentCoinsPanel;
    [SerializeField] TMP_Text BuyBTNText;
    [SerializeField] TMP_Text CoinsText;
    [SerializeField] TMP_Text HighScoreText;

    [Header("ShipSelectMenuPanel")]
    [SerializeField] TMP_Text ShipName;
    [SerializeField] TMP_Text ShipDesc;
    [SerializeField] TMP_Text ShipSpeed;
    [SerializeField] TMP_Text ShipBullitTimeDelay;

    [Header("Audio")]
    [SerializeField] AudioSource Sounds;
    [SerializeField] AudioClip Click, BackClick, Purchase;

    Vector3 SpaceshipRotate = new Vector3(0, 0.3f, 0);
    Vector3 SpaceshipScale = new Vector3(5, 5, 5);
    int currentShipTypeIndex = 0;
    BasicTimer timer;

    List<GameObject> ShipPool = new List<GameObject>();

    #region Singleton
    public static StartMenuUI Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        timer = GetComponent<BasicTimer>();
        saveSystem.Load();
        ShipPoolInit();
    }
    private void Update()
    {
        ShipSelectMenuUpdates();
    }

    #region StartMenu
    public void StartButton()
    {
        Sounds.clip = Click;
        Sounds.Play();
        camAnimator.SetBool("Next", true);
        StartMenuCanvas.SetActive(false);
        currentShipTypeIndex = GameEvents.ShipTypeIndex;
        ShipPool[currentShipTypeIndex].SetActive(true);
        SetPanelData(currentShipTypeIndex);
    }

    public void ControlsButton() {
        Sounds.clip = Click;
        Sounds.Play();
        ControlsCanvas.SetActive(true); 
    }
    public void CloseButton() {
        Sounds.clip = BackClick;
        Sounds.Play();
        ControlsCanvas.SetActive(false); 
    }

    public void Quit()
    {
        Sounds.clip = BackClick;
        Sounds.Play();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    #endregion

    #region ShipSelectMenu
    public void NextShip()
    {
        Sounds.clip = Click;
        Sounds.Play();
        ShipPool[currentShipTypeIndex].SetActive(false);
        currentShipTypeIndex++;
        ShipPool[currentShipTypeIndex].SetActive(true);
        SetPanelData(currentShipTypeIndex);
    }
    public void PrevShip()
    {
        Sounds.clip = Click;
        Sounds.Play();
        ShipPool[currentShipTypeIndex].SetActive(false);
        currentShipTypeIndex--;
        ShipPool[currentShipTypeIndex].SetActive(true);
        SetPanelData(currentShipTypeIndex);
    }

    void ShipPoolInit()
    {
        for (int i = 0; i < 4; i++)
        {
            ShipPool.Add(Instantiate(shipObjects[i].SpaceShipMesh, Spaceship.transform));
        }
        foreach (GameObject Ship in ShipPool)
        {
            Ship.transform.position = Spaceship.transform.position;
            Ship.transform.localScale = SpaceshipScale;
            Ship.SetActive(false);
        }
    }

    void ShipSelectMenuUpdates()
    {
        ShipSelectMenuCanvas.SetActive(StartCam.Instance.ShipSelect);
        StartMenuCanvas.SetActive(!StartCam.Instance.ShipSelect);
        if (camAnimator.GetBool("Next"))
        {
            Spaceship.transform.Rotate(SpaceshipRotate);
        }
        NextShipBTN.interactable = !(currentShipTypeIndex == 3);
        PrevShipBTN.interactable = !(currentShipTypeIndex == 0);
        CoinsText.text = $"{GameEvents.HaveCoins}";
        HighScoreText.text = $"High Score: {GameEvents.HighScore}";
        BuyBTN.SetActive(!GameEvents.BoughtShips[currentShipTypeIndex]);
        BuyBTNText.text = $"{shipObjects[currentShipTypeIndex].Price}";
        if (currentShipTypeIndex == GameEvents.ShipTypeIndex) { EquipBTN.SetActive(false); }
        else { EquipBTN.SetActive(true); }
    }

    void SetPanelData(int ShipTypeIndex)
    {
        ShipName.text = $"Name: { shipObjects[ShipTypeIndex].name}";
        ShipDesc.text = $"Description: {shipObjects[ShipTypeIndex].description}";
        ShipSpeed.text = $"Speed: {shipObjects[ShipTypeIndex].MoveSpeed}";
        ShipBullitTimeDelay.text = $"BullitTimeDelay: {shipObjects[ShipTypeIndex].BullitWaitTime}";
    }

    public void Play()
    {
        Sounds.clip = Click;
        Sounds.Play();
        SceneManager.LoadScene("SampleScene");
    }

    public void Equip()
    {
        Sounds.clip = Click;
        Sounds.Play();
        if (EquipBTN.activeSelf)
        {
            GameEvents.ShipTypeIndex = currentShipTypeIndex;
            EquipBTN.SetActive(false);
        }
    }

    public void Buy()
    {
        Sounds.clip = Purchase;
        Sounds.Play();
        if (GameEvents.HaveCoins >= shipObjects[currentShipTypeIndex].Price)
        {
            GameEvents.HaveCoins -= shipObjects[currentShipTypeIndex].Price;
            GameEvents.BoughtShips[currentShipTypeIndex] = true;
            BuyBTN.SetActive(false);
        }
        else
        {
            InsufficentCoinsPanel.SetActive(true);
            timer.StartTimer();
        }
    }

    public void InsufficentCoins()
    {
        InsufficentCoinsPanel.SetActive(false);
    }

    public void Back()
    {
        Sounds.clip = BackClick;
        Sounds.Play();
        ShipSelectMenuCanvas.SetActive(false);
        camAnimator.SetBool("Next", false);
    }
    #endregion
}
