using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    PlayerController pController;

    public GameObject GamePanel;
    public GameObject MainMenuPanel;
    public GameObject OptionsMenuMainPanel;
    public GameObject OptionsMenuResumePanel;
    public GameObject CanvasPanel;
    public GameObject ResumeGamePanel;
    public GameObject inGamePanel;
    public GameObject helpPanel;
    public GameObject HUD;
    public GameObject Level1Tutorial1;
    public GameObject Level1Tutorial2;
    public GameObject Level1Tutorial3;
    public GameObject Level2Tutorial1;
    public GameObject Level2Tutorial2;
    public GameObject Level2Tutorial3;
    public GameObject Level3Tutorial1;
    public GameObject Level3Tutorial2;
    public GameObject Level3Tutorial3;


    public Button MainMenu;
    public Button OptionsMenuMain;
    public Button OptionsMenuResume;
    public Button Play;
    public Button Quit;
    public Button Back;
    public Button Restart;
    public Button Level1Cont1;
    public Button Level1Cont2;
    public Button Level1Cont3;
    public Button Level2Cont1;
    public Button Level2Cont2;
    public Button Level2Cont3;
    public Button Level3Cont1;
    public Button Level3Cont2;
    public Button Level3Cont3;
    public Button commandList;
    public Button instructions;
    public Button closeCommands;

    public static bool GameIsPaused = false;

    void Start()
    {
        GamePanel.SetActive(true);
        MainMenuPanel.SetActive(true);
        ResumeGamePanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        Level1Tutorial2.SetActive(false);
        Level1Tutorial3.SetActive(false);
        Level2Tutorial1.SetActive(false);
        Level2Tutorial2.SetActive(false);
        Level2Tutorial3.SetActive(false);
        Level3Tutorial1.SetActive(false);
        Level3Tutorial2.SetActive(false);
        Level3Tutorial3.SetActive(false);
        helpPanel.SetActive(false);
        HUD.SetActive(false);

        commandList.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);

        MainMenu.onClick.AddListener(MainMenuInstructions);
        OptionsMenuMain.onClick.AddListener(OptionsMenuMainInstructions);
        OptionsMenuResume.onClick.AddListener(OptionsMenuResumeInstructions);
        Play.onClick.AddListener(PlayGameInstructions);
        Quit.onClick.AddListener(QuitGameInstructions);
        Back.onClick.AddListener(BackButtonInstructions);
        
        Level1Cont1.onClick.AddListener(OnLevel1ContinueOne);
        Level1Cont2.onClick.AddListener(OnLevel1ContinueTwo);
        Level1Cont3.onClick.AddListener(OnLevel1ContinueThree);
        Level2Cont1.onClick.AddListener(OnLevel2ContinueOne);
        Level2Cont2.onClick.AddListener(OnLevel2ContinueTwo);
        Level2Cont3.onClick.AddListener(OnLevel2ContinueThree);
        Level3Cont1.onClick.AddListener(OnLevel2ContinueOne);
        Level3Cont2.onClick.AddListener(OnLevel2ContinueTwo);
        Level3Cont3.onClick.AddListener(OnLevel2ContinueThree);
    }

    void SetInstructions()
    {
       //pController.HasControl = false;
        GamePanel.SetActive(true);
        MainMenuPanel.SetActive(true);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        HUD.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void MainMenuInstructions()
    {
        //pController.HasControl = false;
        GamePanel.SetActive(true);
        MainMenuPanel.SetActive(true);
        OptionsMenuMainPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        HUD.SetActive(false);
    }

    void OptionsMenuMainInstructions()
    {
        //pController.HasControl = false;
        MainMenuPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        HUD.SetActive(false);
        GamePanel.SetActive(true);
        OptionsMenuMainPanel.SetActive(true);

    }
    void BackButtonInstructions()
    {
        //pController.HasControl = false;
        GamePanel.SetActive(true);
        MainMenuPanel.SetActive(true);
        OptionsMenuMainPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        HUD.SetActive(false);
    }


    void QuitGameInstructions()
    {
        //pController.HasControl = false;
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        HUD.SetActive(false);
        Application.Quit();
    }


    void Resume()
    {
        //pController.HasControl = true;
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        HUD.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
       // pController.HasControl = false;
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(true);
        ResumeGamePanel.SetActive(true);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        HUD.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void OptionsMenuResumeInstructions()
    {
        //pController.HasControl = false;
        GamePanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(true);
        inGamePanel.SetActive(false);
        Level1Tutorial1.SetActive(false);
        HUD.SetActive(false);
    }

    void PlayGameInstructions()
    {
        //pController.HasControl = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        inGamePanel.SetActive(true);
        Level1Tutorial1.SetActive(true);
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        HUD.SetActive(false);
     
     }

    void OnLevel1ContinueOne()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(true);
        Level1Tutorial1.SetActive(false);
        Level1Tutorial2.SetActive(true);
        HUD.SetActive(false);
    }

    void OnLevel1ContinueTwo()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(true);
        Level1Tutorial2.SetActive(false);
        Level1Tutorial3.SetActive(true);
        HUD.SetActive(false);
    }

    void OnLevel1ContinueThree()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level1Tutorial3.SetActive(false);
        HUD.SetActive(true);
        commandList.gameObject.SetActive(true);
        instructions.gameObject.SetActive(true);

        pController.HasControl = true;
    }
    void OnLevel2ContinueOne()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(true);
        Level2Tutorial1.SetActive(false);
        Level2Tutorial2.SetActive(true);
        HUD.SetActive(false);
    }

    void OnLevel2ContinueTwo()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(true);
        Level2Tutorial2.SetActive(false);
        Level2Tutorial3.SetActive(true);
        HUD.SetActive(false);
    }

    void OnLevel2ContinueThree()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level2Tutorial3.SetActive(false);
        HUD.SetActive(true);
        commandList.gameObject.SetActive(true);
        instructions.gameObject.SetActive(true);

        pController.HasControl = true;
    }

    void OnLevel3ContinueOne()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(true);
        Level3Tutorial1.SetActive(false);
        Level3Tutorial2.SetActive(true);
        HUD.SetActive(false);
    }

    void OnLevel3ContinueTwo()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(true);
        Level3Tutorial2.SetActive(false);
        Level3Tutorial3.SetActive(true);
        HUD.SetActive(false);
    }

    void OnLevel3ContinueThree()
    {
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        inGamePanel.SetActive(false);
        Level3Tutorial3.SetActive(false);
        HUD.SetActive(true);
        commandList.gameObject.SetActive(true);
        instructions.gameObject.SetActive(true);

        pController.HasControl = true;
    }
    void OpenCommands()
    {
        pController.HasControl = false;

        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        helpPanel.SetActive(true);
        HUD.SetActive(false);
    }

    void CloseCommands()
    {
        pController.HasControl = true;
        Debug.Log("Test");
        GamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OptionsMenuMainPanel.SetActive(false);
        CanvasPanel.SetActive(false);
        ResumeGamePanel.SetActive(false);
        OptionsMenuResumePanel.SetActive(false);
        helpPanel.SetActive(false);
        HUD.SetActive(true);
    }
}




