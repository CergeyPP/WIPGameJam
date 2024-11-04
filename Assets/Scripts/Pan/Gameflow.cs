using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct GameEpisode
{
    public int maxPanActive;
    public float episodeDuration;
    public float activatePanInterval;
    public float panWarningDuration;
    public float panDangerDuration;
}
public class Gameflow : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private List<GameEpisode> _gameEpisodes;
    [SerializeField] private List<Pan> _pans;

    private float _timerValue = 0;

    private float _internalEpisodeTimer = 0;
    private int _episodeIndex = 0;
    private int _currentPanActiveCount = 0;
    private GameEpisode CurrentEpisode => _gameEpisodes[_episodeIndex];
    private int PanInactiveCount => CurrentEpisode.maxPanActive - _currentPanActiveCount;
    private void Start()
    {
        foreach (var pan in _pans)
        {
            pan.Activated += OnPanActivated;
            pan.Deactivated += OnPanDeactivated;
            pan.PlayerKilled += OnPlayerKilled;
        }

        SetupNextGameEpisode();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ÕÅÐ ÇÍÀÅÒ, ÊÀÊ ÝÒÎ ÑÄÅËÀÒÜ ×ÅÐÅÇ ÍÎÂÓÞ ÈÍÏÓÒ ÑÈÑÒÅÌ, ÒÀÊ ×ÒÎ ÏÓÑÒÜ ÁÓÄÅÒ ÒÀÊ
        {
            SceneManager.LoadScene(0);
        }

        _timerValue += Time.deltaTime;
        UpdateTimerText();

        _internalEpisodeTimer += Time.deltaTime;
        if (_internalEpisodeTimer > CurrentEpisode.episodeDuration)
        {
            _internalEpisodeTimer -= CurrentEpisode.episodeDuration;
            if (_episodeIndex < _gameEpisodes.Count - 1)
            {
                _episodeIndex++;
                SetupNextGameEpisode();
            }
        }

        UpdatePans();
    }

    private void OnPanActivated(Pan pan)
    {
        _currentPanActiveCount++;
    }
    private void OnPanDeactivated(Pan pan)
    {
        _currentPanActiveCount--;
    }

    private void OnPlayerKilled()
    {
        _gameOverScreen.SetActive(true);
        AudioManager.Instance.StopMusic();
        Time.timeScale = 0;
    }

    private void UpdateTimerText()
    {
        int sec = (int)Math.Floor(_timerValue);
        int milisec = (int)((_timerValue - (float)sec) * 100);
        int min = sec / 60;
        sec = sec % 60;
        _timerText.text = min.ToString("D2") + ":" + sec.ToString("D2") + ":" + milisec.ToString("D2");
    }

    private void UpdatePans()
    {
        if (PanInactiveCount > 0)
        {
            Pan playerPan = null;
            foreach (var pan in _pans)
            {
                if (pan.StayingPlayer && !pan.IsActive)
                {
                    playerPan = pan;
                    break;
                }
            }

            if (playerPan)
            {
                playerPan.Activate();
            } else
            {
                List<Pan> inactivePans = _pans.FindAll(pan => !pan.IsActive);
                Pan randomInactivePan = inactivePans[UnityEngine.Random.Range(0, inactivePans.Count-1)];
                randomInactivePan.Activate();
            }
        }
    }

    private void SetupNextGameEpisode()
    {
        foreach (var pan in _pans)
        {
            pan.SetDangerTime(CurrentEpisode.panDangerDuration);
            pan.SetWarningTime(CurrentEpisode.panWarningDuration);
        }
    }


}
