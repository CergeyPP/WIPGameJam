using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;

    private void Start()
    {
        mainPanel.SetActive(true);
    }

    public void OnClickBack(GameObject panelToClose)
    {
        mainPanel.SetActive(true);
        panelToClose.SetActive(false);
    }

    // ========================================= MAIN PANEL BUTTONS ========================================= //
    public void OnClickStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnClickRules(GameObject panelToOpen)
    {
        mainPanel.SetActive(false);
        panelToOpen.SetActive(true);
    }

    public void OnClickCredits(GameObject panelToOpen)
    {
        mainPanel.SetActive(false);
        panelToOpen.SetActive(true);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
