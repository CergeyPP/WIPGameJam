using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class ForRestartOnDemo : MonoBehaviour
{
    private void Update()
    {
        RestartButton();
    }
    private void RestartButton()
    {
        if (Input.GetKey(KeyCode.R))
        {
           GameWinScene();
        }
    }
    private void GameWinScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Taimer.sec = 0;
        TaimingTrup.namePlatform = "";
        TaimingTrup.namePlatform2 = "";
        TaimingTrup.Time = 2f;
        TaimingTrup.namePlatformM = "";
        TaimingTrup.namePlatform2M = "";
        TaimingTrup.randomPlatforma = -10;
        TaimingTrup.randomPlatforma2 = -10;
        Time.timeScale = 1f;
    }
}
