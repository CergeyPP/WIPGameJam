using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScoreIncrementByTimer : MonoBehaviour
{
    [SerializeField] private int _scoreToAdd = 1;
    [SerializeField] private float _increaseDeltaSeconds = 1;

    [SerializeField] private ScoreChangeEvent OnIncreaseScore;

    private Coroutine _scoreIncreaseCoroutine;

    private void Start()
    {
        _scoreIncreaseCoroutine = StartCoroutine(IncreaseByTime());
    }

    private IEnumerator IncreaseByTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(_increaseDeltaSeconds);
            OnIncreaseScore.Invoke(_scoreToAdd);
        }
    }
}
