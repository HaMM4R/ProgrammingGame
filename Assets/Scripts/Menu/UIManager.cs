using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    PlayerController pController;

    public GameObject inGamePanel;
    public GameObject helpPanel;
    public GameObject HUD; 

    public GameObject Level1Tutorial1;
    public GameObject Level1Tutorial2;
    public GameObject Level1Tutorial3;

    public Button Level1Cont1;
    public Button Level1Cont2;
    public Button Level1Cont3;

    public Button commandList;
    public Button instructions;

    public Button closeCommands; 

    // Start is called before the first frame update
    void Start()
    {
        inGamePanel.SetActive(true);
        Level1Tutorial1.SetActive(true);
        Level1Tutorial2.SetActive(false);
        Level1Tutorial3.SetActive(false);
        helpPanel.SetActive(false);
        HUD.SetActive(false);

        commandList.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);

        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pController.HasControl = false;

        Level1Cont1.onClick.AddListener(OnContinueOne);
        Level1Cont2.onClick.AddListener(OnContinueTwo);
        Level1Cont3.onClick.AddListener(OnContinueThree);
        commandList.onClick.AddListener(OpenCommands);
        instructions.onClick.AddListener(SetInstructions);
        closeCommands.onClick.AddListener(CloseCommands);
    }

    void SetInstructions()
    {
        pController.HasControl = false;
        inGamePanel.SetActive(true);
        Level1Tutorial1.SetActive(true);
        HUD.SetActive(false);
    }

    void OnContinueOne()
    {
        Level1Tutorial1.SetActive(false);
        Level1Tutorial2.SetActive(true);
    }

    void OnContinueTwo()
    {
        Level1Tutorial2.SetActive(false);
        Level1Tutorial3.SetActive(true);
    }

    void OnContinueThree()
    {
        Level1Tutorial3.SetActive(false);
        inGamePanel.SetActive(false);
        HUD.SetActive(true);
        commandList.gameObject.SetActive(true);
        instructions.gameObject.SetActive(true);

        pController.HasControl = true;
    }

    void OpenCommands()
    {
        pController.HasControl = false;
        helpPanel.SetActive(true);
        HUD.SetActive(false);
    }

    void CloseCommands()
    {
        pController.HasControl = true;
        Debug.Log("Test");
        helpPanel.SetActive(false);
        HUD.SetActive(true);
    }
}
