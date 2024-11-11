using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    //[SerializeField, Range(0, 1f)] private float audioVolume = 1f;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        source.volume = GetVolume();
        volumeSlider.value = GetVolume();
        mainPanel.SetActive(true);
        source.Play();
    }

    private float GetVolume()
    {
        float temp;

        if (PlayerPrefs.HasKey("Volume"))
        {
            temp = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            temp = 0.85f; // default volume - not max, but close to it
        }

        return temp;
    }

    public void OnClickBack(GameObject panelToClose)
    {
        mainPanel.SetActive(true);
        panelToClose.SetActive(false);
    }

    // ========================================= MAIN PANEL ELEMENTS ========================================= //
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

    public void OnChangeVolume()
    {
        float newValue = volumeSlider.value;
        source.volume = newValue;
        PlayerPrefs.SetFloat("Volume", newValue);
    }
}
