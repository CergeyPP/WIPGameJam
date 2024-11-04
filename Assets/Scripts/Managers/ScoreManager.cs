using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ScoreChangeEvent : UnityEvent<int>
{

}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _startScore = 0;
    private int _score = 0;

    [Tooltip("При изменении очков вызывается это событие. Передает количество текущих очков")]
    public ScoreChangeEvent ScoreChanged;

    public void AddScore(int score)
    {
        _score += score;
        ScoreChanged.Invoke(_score);
    }

    public void SubtractScore(int score)
    {
        _score -= score;
        ScoreChanged.Invoke(_score);
    }

    private void Start()
    {
        _score = _startScore;
        ScoreChanged.Invoke(_score);
    }
}
