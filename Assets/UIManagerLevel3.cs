using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManagerLevel3 : MonoBehaviour
{

    PlayerController pController;

    public GameObject inGamePanel;
    public GameObject helpPanel;
    public GameObject HUD;

    public GameObject Level3Tutorial1;
    public GameObject Level3Tutorial2;
    public GameObject Level3Tutorial3;

    public Button Level3Cont1;
    public Button Level3Cont2;
    public Button Level3Cont3;

    public Button commandList;
    public Button instructions;

    public Button closeCommands;

    // Start is called before the first frame update
    void Start()
    {
        inGamePanel.SetActive(true);
        Level3Tutorial1.SetActive(true);
        Level3Tutorial2.SetActive(false);
        Level3Tutorial3.SetActive(false);
        helpPanel.SetActive(false);
        HUD.SetActive(false);

        commandList.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);

        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pController.HasControl = false;

        Level3Cont1.onClick.AddListener(OnContinueOne);
        Level3Cont2.onClick.AddListener(OnContinueTwo);
        Level3Cont3.onClick.AddListener(OnContinueThree);
        commandList.onClick.AddListener(OpenCommands);
        instructions.onClick.AddListener(SetInstructions);
        closeCommands.onClick.AddListener(CloseCommands);
    }

    void SetInstructions()
    {
        pController.HasControl = false;
        HUD.SetActive(false);
        inGamePanel.SetActive(true);
        Level3Tutorial1.SetActive(true);

    }

    void OnContinueOne()
    {
        Level3Tutorial1.SetActive(false);
        Level3Tutorial2.SetActive(true);
    }

    void OnContinueTwo()
    {
        Level3Tutorial2.SetActive(false);
        Level3Tutorial3.SetActive(true);
    }

    void OnContinueThree()
    {
        Level3Tutorial3.SetActive(false);
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