﻿using System;
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
    PlayerShoot pShoot;
    PlayerHealth pHealth;

    string currentLevel;
    int numberOfAttempts;

    public List<LevelTutorials> levelTutorials = new List<LevelTutorials>();
    public List<LevelTutorials> levelHints = new List<LevelTutorials>();

    public List<int> levelAttempts = new List<int>();

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
    public Text attempts;

    [Header("HintsUI")]
    public GameObject hints;
    public Text hintText;

    [Header("GameOver")]
    public GameObject gameOver;
    public Text level1Attempts;
    public Text level1Rating;
    public Text level2Attempts;
    public Text level2Rating;
    public Text level3Attempts;
    public Text level3Rating;
    public Button quit;

    [Header("New Stuff")]

    [SerializeField]
    GameObject functionPanel;

    [SerializeField]
    GameObject functionButton;

    int functionID; 

    int tutTextCount;
    int level;
    GridGeneration grid;
    CodeInput code;

    bool playerDead;

    int hintsCount = 0; 

    Text uiText;

    string rating1;
    string rating2;
    string rating3; 

    // Start is called before the first frame update
    void Start()
    {
        playerDead = false; 
        DontDestroyOnLoad(this);
        functionID = 0; 

        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(pHealth != null && code != null)
            if ((pHealth.playerHealth <= 0 && !playerDead) || (code.playerFailed && code.instructionComplete && !playerDead))
                Dead();
        

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
        hints.SetActive(true);
        hintText.text = levelHints[level].tutorialText[hintsCount];

        if(hintsCount < levelHints[level].tutorialText.Count - 1)
            hintsCount++;
    }

    void UpdateHUD()
    {
        if(pHealth != null)
            health.text = "Health: " + pHealth.playerHealth.ToString();
        if(pShoot != null)
            ammo.text = "Ammo: " + pShoot.Ammo.ToString();
        
        if(grid != null)
            attempts.text = "Level attempted " + (levelAttempts[grid.level] - 1) + " times.";
    }

    void Setup()
    {
        rating1 = "Room for improvement";
        rating2 = "Good!";
        rating3 = "Excellent!"; 

        tutTextCount = 0;
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
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
        hints.SetActive(false);

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
        quit.onClick.AddListener(ExitGame);
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

        playerDead = false;
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
        hints.SetActive(false);
        ChangeTutorialText();
    }

    public void GetPlayer(PlayerController p, PlayerShoot pS, PlayerHealth pH)
    {
        pController = p;
        pShoot = pS;
        pHealth = pH; 
    }

    void ChangeTutorialText()
    {
        if (tutTextCount < levelTutorials[level].tutorialText.Count)
        {
            tutTextCount++;
            uiText.text = levelTutorials[level].tutorialText[tutTextCount - 1];
        }
        else
        {
            CloseTutorial(); 
        }
    }

    void CloseTutorial()
    {
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            var manager = GameObject.FindGameObjectWithTag("GameController");
            grid = manager.GetComponent<GridGeneration>();
            code = manager.GetComponent<CodeInput>();
        }

        if (grid != null)
            level = grid.level;

        //pController.HasControl = true;
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
        hints.SetActive(false);
        playerDead = false;
        tutTextCount = 0;
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
        if (scene.name != "3")
        {
            if (currentLevel != scene.name)
            {
                currentLevel = scene.name;
                hintsCount = 0;
                numberOfAttempts = 0;
                levelAttempts.Add(0);
                codeRecieve.text = "";
            }

            Debug.Log("OnSceneLoaded: " + scene.name);
            if (scene.name != "Menu" && numberOfAttempts == 0)
                SetupTutorialPanel();
            else
                CloseTutorial();

            if (currentLevel == scene.name && scene.name != "Menu")
            {
                numberOfAttempts++;
                levelAttempts[grid.level]++;
            }
        }
        else
            FinishGame(); 
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OpenCommands()
    {
        helpPanel.SetActive(true);
        commandList.gameObject.SetActive(false);
        openCodeWindow.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        HUD.SetActive(false);
    }

    void SubmitCommands()
    {
        Debug.Log(codeRecieve.text);
        code.GetCode(codeRecieve.text);
        //pController.HasControl = true;
        codeInputHolder.SetActive(false);
        HUD.SetActive(true);
        if (!codeInputHolder.activeInHierarchy)
            openCodeWindow.gameObject.SetActive(true);
    }

    void closeCommands()
    {
        helpPanel.SetActive(false);
        commandList.gameObject.SetActive(true);
        if (!codeInputHolder.activeInHierarchy)
            openCodeWindow.gameObject.SetActive(true);
        instructions.gameObject.SetActive(true);
        HUD.SetActive(true);
    }

    void FinishGame()
    {
        gameOver.SetActive(true);
        pauseMenu.SetActive(false);
        restart.gameObject.SetActive(false);
        inGameMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        tutorialUIHolder.SetActive(false);
        tutorialUIText.SetActive(false);
        tutorialUIBackground.SetActive(false);
        mainMenu.SetActive(false);
        mainmenuPanel.SetActive(false);
        optionsMenu.SetActive(false);
        optionsmenupanel.SetActive(false);
        helpPanel.SetActive(false);
        commandList.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        codeInputHolder.SetActive(false);
        HUD.SetActive(false);
        hints.SetActive(false);

        level1Attempts.text = "Number of Attempts: " + levelAttempts[0].ToString();
        level2Attempts.text = "Number of Attempts: " + levelAttempts[1].ToString();
        level3Attempts.text = "Number of Attempts: " + levelAttempts[2].ToString();

        if (levelAttempts[0] <= 3)
            level1Rating.text = rating3;
        else if (levelAttempts[0] <= 5)
            level1Rating.text = rating2;
        else if(levelAttempts[0] > 5)
            level1Rating.text = rating1;

        if (levelAttempts[1] <= 3)
            level2Rating.text = rating3;
        else if (levelAttempts[1] <= 5)
            level2Rating.text = rating2;
        else if (levelAttempts[1] > 5)
            level2Rating.text = rating1;

        if (levelAttempts[2] <= 3)
            level3Rating.text = rating3;
        else if (levelAttempts[2] <= 5)
            level3Rating.text = rating2;
        else if (levelAttempts[2] > 5)
            level3Rating.text = rating1;

    }

    void ExitGame()
    {
        Application.Quit(); 
    }

    public void CreateFunctionButton(string code)
    {
        var funcButton = GameObject.Instantiate(functionButton, functionPanel.transform);
        funcButton.transform.position = funcButton.transform.position - new Vector3(0, (functionID * funcButton.GetComponent<RectTransform>().rect.height), 0); 

        funcButton.GetComponent<FunctionHolder>().SetValues(functionID, code);
        functionID++; 
    }
}

