using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Serializable]
    public struct LevelTutorials
    {
        public List<string> tutorialText;
    }

    PlayerController pController;
    public PlayerShoot pShoot;
    public PlayerHealth pHealth; 

    public List<LevelTutorials> levelTutorials = new List<LevelTutorials>();

    [Header("Main Panels")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject inGameMenu;
    public GameObject instructionsMenu;
    public GameObject HUD; 


    [Header("Main menu Panels")]
    public GameObject mainmenuPanel;
    public GameObject optionsmenupanel;
    public GameObject GamePanel;
    public GameObject optionsMenu;

    public Button Play;
    public Button optionsButton;
    public Button Quit;
    public Button Back;

    [Header("Tutorial Panels")]
    public GameObject tutorialUIHolder;
    public GameObject tutorialUIText;
    public GameObject tutorialUIBackground; 
    public Button tutorialContinue;
    public GameObject helpPanel;
    public GameObject inGamePanel;
    public Button commandList;
    public Button instructions;
    public Button CloseCommands;

    [Header("Code Input Panels")]
    public GameObject codeInputHolder;
    public InputField codeRecieve;
    public Button submitCode;
    public Button closeCodeWindow;
    public Button openCodeWindow;

    [Header("IngameUI")]
    public Text health;
    public Text ammo;
    public Button restart; 


    int tutTextCount;
    int level;
    GridGeneration grid;
    CodeInput code;

    bool playerDead; 

    Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        playerDead = false; 
        DontDestroyOnLoad(this);
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (pHealth.playerHealth <= 0 && !playerDead)
        {
            Dead(); 
        }

        UpdateHUD(); 
    }

    void Dead()
    {
        playerDead = true;
        HUD.SetActive(false);
        commandList.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        openCodeWindow.gameObject.SetActive(false);
        codeInputHolder.SetActive(false);
        restart.gameObject.SetActive(true); 
    }

    void UpdateHUD()
    {
        if(pHealth != null)
            health.text = "Health: " + pHealth.playerHealth.ToString();
        if(pShoot != null)
            ammo.text = "Ammo: " + pShoot.Ammo.ToString(); 
    }

    void Setup()
    {
        tutTextCount = 0;
        pauseMenu.SetActive(false);
        restart.gameObject.SetActive(false);
        inGameMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        tutorialUIHolder.SetActive(false);
        tutorialUIText.SetActive(false);
        tutorialUIBackground.SetActive(false);
        mainMenu.SetActive(true);
        mainmenuPanel.SetActive(true);
        optionsMenu.SetActive(false);
        optionsmenupanel.SetActive(false);
        helpPanel.SetActive(false);
        uiText = tutorialUIText.GetComponent<Text>();
        commandList.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        codeInputHolder.SetActive(false);
        HUD.SetActive(false); 

        tutorialContinue.onClick.AddListener(ChangeTutorialText);
        optionsButton.onClick.AddListener(Options);
        Play.onClick.AddListener(PlayGame);
        Quit.onClick.AddListener(QuitGame);
        Back.onClick.AddListener(BackButton);
        commandList.onClick.AddListener(OpenCommands);
        instructions.onClick.AddListener(SetupTutorialPanel);
        CloseCommands.onClick.AddListener(closeCommands);
        openCodeWindow.onClick.AddListener(OpenCodeWindow);
        closeCodeWindow.onClick.AddListener(CloseCodeWindow);
        submitCode.onClick.AddListener(SubmitCommands);
        restart.onClick.AddListener(Respawn);
    }

    void Respawn()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void SetupTutorialPanel()
    {
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            var manager = GameObject.FindGameObjectWithTag("GameController");
            grid = manager.GetComponent<GridGeneration>();
            code = manager.GetComponent<CodeInput>();
        }

        if (grid != null)
            level = grid.level;

        tutTextCount = 0;
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        inGameMenu.SetActive(false);
        tutorialUIHolder.SetActive(true);
        tutorialUIBackground.SetActive(true);
        tutorialUIText.SetActive(true);
        mainMenu.SetActive(false);
        mainmenuPanel.SetActive(true);
        optionsMenu.SetActive(false);
        optionsmenupanel.SetActive(false);
        restart.gameObject.SetActive(false);
        ChangeTutorialText();
    }

    public void GetPlayer(PlayerController p, PlayerShoot pS, PlayerHealth pH)
    {
        pController = p;
        pShoot = pS;
        pHealth = pH; 
        pController.HasControl = false;
    }

    void ChangeTutorialText()
    {
        if (tutTextCount < levelTutorials[level].tutorialText.Count)
        {
            if(pController != null)
                pController.HasControl = false;
            tutTextCount++;
            uiText.text = levelTutorials[level].tutorialText[tutTextCount - 1];
        }
        else
        {
            pController.HasControl = true;
            inGameMenu.SetActive(true);
            instructionsMenu.SetActive(false);
            tutorialUIHolder.SetActive(false);
            tutorialUIBackground.SetActive(false);
            tutorialUIText.SetActive(false);
            commandList.gameObject.SetActive(true);
            HUD.SetActive(true);
            if (!codeInputHolder.activeInHierarchy)
                openCodeWindow.gameObject.SetActive(true);
            instructions.gameObject.SetActive(true);
        }
    }
    
    void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //pController.HasControl = false;

    }

    void OpenCodeWindow()
    {
        codeInputHolder.SetActive(true);
        HUD.SetActive(false);
        openCodeWindow.gameObject.SetActive(false);
    }

    void CloseCodeWindow()
    {
        codeInputHolder.SetActive(false);
        HUD.SetActive(true);
        openCodeWindow.gameObject.SetActive(true);
    }

    void Options()
    {
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        inGameMenu.SetActive(false);
        tutorialUIHolder.SetActive(false);
        tutorialUIText.SetActive(false);
        mainMenu.SetActive(false);
        mainmenuPanel.SetActive(false);
        optionsMenu.SetActive(true);
        optionsmenupanel.SetActive(true);
        GamePanel.SetActive(true);
        optionsMenu.SetActive(true);

    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    void BackButton()
    {
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        inGameMenu.SetActive(false);
        tutorialUIHolder.SetActive(false);
        tutorialUIText.SetActive(false);
        mainMenu.SetActive(true);
        mainmenuPanel.SetActive(true);
        optionsMenu.SetActive(false);
        optionsmenupanel.SetActive(false);
    }

    void  OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if(scene.name != "Menu")
            SetupTutorialPanel();
        codeRecieve.text = "";
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OpenCommands()
    {
        pController.HasControl = false;
        helpPanel.SetActive(true);
        commandList.gameObject.SetActive(false);
        openCodeWindow.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        HUD.SetActive(false);
    }

    void SubmitCommands()
    {
        code.GetCode(codeRecieve.text);
        pController.HasControl = true;
        codeInputHolder.SetActive(false);
        HUD.SetActive(true);
        if (!codeInputHolder.activeInHierarchy)
            openCodeWindow.gameObject.SetActive(true);
    }

    void closeCommands()
    {
        pController.HasControl = true;
        helpPanel.SetActive(false);
        commandList.gameObject.SetActive(true);
        if (!codeInputHolder.activeInHierarchy)
            openCodeWindow.gameObject.SetActive(true);
        instructions.gameObject.SetActive(true);
        HUD.SetActive(true);
    }
}

