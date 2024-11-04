using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField, Range(0, 1f)] private float audioVolume = 1f;
    [SerializeField] private GameObject mainPanel;

    private void Start()
    {
        source.volume = audioVolume;
        mainPanel.SetActive(true);
        source.Play();
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
        source.Stop();
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
