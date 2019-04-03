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
    public List<LevelTutorials> levelTutorials = new List<LevelTutorials>();

    [Header("Main Panels")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject inGameMenu;
    public GameObject instructionsMenu;



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
    public Button tutorialContinue;
    public GameObject HUD;
    public GameObject helpPanel;
    public GameObject inGamePanel;
    public Button commandList;
    public Button instructions;
    public Button CloseCommands;


    int tutTextCount;
    int level;
    GridGeneration grid;

    Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void Setup()
    {
        tutTextCount = 0;
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        tutorialUIHolder.SetActive(false);
        tutorialUIText.SetActive(false);
        mainMenu.SetActive(true);
        mainmenuPanel.SetActive(true);
        optionsMenu.SetActive(false);
        optionsmenupanel.SetActive(false);
        helpPanel.SetActive(false);
        HUD.SetActive(false);
        uiText = tutorialUIText.GetComponent<Text>();
        commandList.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
      //  SetupTutorialPanel();
        tutorialContinue.onClick.AddListener(ChangeTutorialText);
        optionsButton.onClick.AddListener(Options);
        Play.onClick.AddListener(PlayGame);
        Quit.onClick.AddListener(QuitGame);
        Back.onClick.AddListener(BackButton);
        commandList.onClick.AddListener(OpenCommands);
        instructions.onClick.AddListener(Instructions);
        CloseCommands.onClick.AddListener(closeCommands);
    }

    void SetupTutorialPanel()
    {
        if (GameObject.FindGameObjectWithTag("GameController") != null)
            grid = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridGeneration>();

        if (grid != null)
            level = grid.level;

        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        inGameMenu.SetActive(true);
        tutorialUIHolder.SetActive(true);
        tutorialUIText.SetActive(true);
        mainMenu.SetActive(false);
        mainmenuPanel.SetActive(true);
        optionsMenu.SetActive(false);
        optionsmenupanel.SetActive(false);
        ChangeTutorialText();
    }

    public void GetPlayer(PlayerController p)
    {
        pController = p;
        pController.HasControl = false;
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
            pController.HasControl = true;
            inGameMenu.SetActive(true);
            instructionsMenu.SetActive(false);
            tutorialUIHolder.SetActive(false);
            tutorialUIText.SetActive(false);
            HUD.SetActive(true);
            commandList.gameObject.SetActive(true);
            instructions.gameObject.SetActive(true);
        }
    }


    //IEnumerator LoadingLevel()
    //{
    //    yield return new WaitUntil(isLevelLoaded);
    //    Debug.Log("LoadingLevel");
    //}

    //public bool isLevelLoaded()
    //{
    //    if (counter > 0)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
    void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //pController.HasControl = false;

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
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Instructions()
    {
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        inGameMenu.SetActive(false);
        tutorialUIHolder.SetActive(true);
        tutorialUIText.SetActive(true);
        mainMenu.SetActive(false);
        mainmenuPanel.SetActive(true);
        optionsMenu.SetActive(false);
        optionsmenupanel.SetActive(false);
        ChangeTutorialText();
    }

    void OpenCommands()
    {
        pController.HasControl = false;
        helpPanel.SetActive(true);
        HUD.SetActive(false);
    }

    void closeCommands()
    {
        pController.HasControl = true;
        helpPanel.SetActive(false);
        HUD.SetActive(true);
    }
}

