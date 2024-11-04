using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    private void Awake()
    {
        _textField.text = "0";
    }

    public void OnScoreChanged(int newScore)
    {
        _textField.text = newScore.ToString();
    }
}
