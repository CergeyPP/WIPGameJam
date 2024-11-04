using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pan : MonoBehaviour
{
    [SerializeField] private PanIndicator _panIndicator;
    [Tooltip("Время, пока сковородка горит предупредительным цветом")]
    [SerializeField] private float _warningTime;
    [Tooltip("Время, когда на сковородку нельзя наступать")]
    [SerializeField] private float _dangerTime;

    public bool IsActive => _isActive;
    public event Action OnPlayerKilled;

    private GameObject _player;
    private bool _isActive;

    private bool _isDanger;

    private void Awake()
    {
        _player = null;
        _isActive = false;
        _isDanger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = other.gameObject;
        Debug.Log($"Enter {gameObject.name}");
        if (_isDanger)
            OnPlayerKilled.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        _player = null;
        Debug.Log($"Exit {gameObject.name}");
    }

    public void Activate()
    {
        StartCoroutine(ActivatePanAndDeactivate());
    }

    public IEnumerator ActivatePanAndDeactivate()
    {
        _isActive = true;

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
        }
        yield return new WaitForSeconds(_dangerTime);
        _isDanger = false;
        _panIndicator.PlayDefaultTransition();
        yield return new WaitForSeconds(_panIndicator.DangerTransitionTime);

        _isActive = false;
    }

    private void KillPlayer()
    {
        OnPlayerKilled.Invoke();
    }
}
