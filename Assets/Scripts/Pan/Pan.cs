using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pan : MonoBehaviour
{
    [SerializeField] private PanIndicator _panIndicator;
    [Tooltip("�����, ���� ���������� ����� ����������������� ������")]
    [SerializeField] private float _warningTime;
    [Tooltip("�����, ����� �� ���������� ������ ���������")]
    [SerializeField] private float _dangerTime;

    [SerializeField] private AudioSource source;

    public bool IsActive => _isActive;
    public event Action<Pan> Activated;
    public event Action<Pan> Deactivated;
    public event Action PlayerKilled;

    private GameObject _player;
    public GameObject StayingPlayer => _player;
    private bool _isActive;

    private bool _isDanger;

    private void Awake()
    {
        _player = null;
        _isActive = false;
        _isDanger = false;
    }

    private void Start()
    {
        source.volume = AudioManager.Instance.GetVolume();
        Debug.Log($"Pan volume: {source.volume}");
        if (source.volume >= 0.2f)
            source.volume = 1f;
        else
            source.volume = 0f;
        source.loop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = other.gameObject;
        Debug.Log($"Enter {gameObject.name}");
        if (_isDanger)
            PlayerKilled.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        _player = null;
        Debug.Log($"Exit {gameObject.name}");
    }

    public void SetWarningTime(float warningTime)
    {
        _warningTime = warningTime;
    }

    public void SetDangerTime(float dangerTime)
    {
        _dangerTime = dangerTime;
    }

    public void Activate()
    {
        StartCoroutine(ActivatePanAndDeactivate());
    }

    public IEnumerator ActivatePanAndDeactivate()
    {
        _isActive = true;
        Activated.Invoke(this);
        _panIndicator.PlayWarningTransition();
        yield return new WaitForSeconds(_warningTime - _panIndicator.DangerTransitionTime);
        _panIndicator.PlayDangerTransition();
        yield return new WaitForSeconds(_panIndicator.DangerTransitionTime);
        if (_player != null)
        {
            KillPlayer();
        }
        else
        {
            _isDanger = true;
            source.Play(); // play hot sound
        }
        yield return new WaitForSeconds(_dangerTime);
        _isDanger = false;
        _panIndicator.PlayDefaultTransition();
        yield return new WaitForSeconds(_panIndicator.DangerTransitionTime);

        _isActive = false;
        Deactivated.Invoke(this);
    }

    private void KillPlayer()
    {
        PlayerKilled.Invoke();
    }
}
