using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PanIndicator : MonoBehaviour
{
    [SerializeField] private int _materialIndex = 0;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _warningColor;
    [SerializeField] private Color _dangerColor;

    [Header("Тайминги")]
    [SerializeField] private float _defaultTransitionTime = 0.1f;
    [SerializeField] private float _warningTransitionTime = 0.1f;
    [SerializeField] private float _dangerTransitionTime = 0.1f;

    public float DefaultTransitionTime => _defaultTransitionTime;
    public float WarningTransitionTime => _warningTransitionTime;
    public float DangerTransitionTime => _dangerTransitionTime;

    private Material _panMaterial;
    private int _emissionPropertyID;
    private Coroutine _currentTransitionCoroutine;
    private bool _isTransitionRunning = false;

    private void Awake()
    {
        _emissionPropertyID = Shader.PropertyToID("_EmissionColor");
    }

    private void Start()
    {
        _panMaterial = GetComponent<Renderer>().material;
        _panMaterial.SetColor(_emissionPropertyID, _defaultColor);
    }

    public void PlayWarningTransition()
    {
        if (_isTransitionRunning)
            StopCoroutine(_currentTransitionCoroutine);

        _currentTransitionCoroutine = StartCoroutine(TransitToColor(_warningColor, _warningTransitionTime));
    }

    public void PlayDangerTransition()
    {
        if (_isTransitionRunning)
            StopCoroutine(_currentTransitionCoroutine);

        _currentTransitionCoroutine = StartCoroutine(TransitToColor(_dangerColor, _dangerTransitionTime));
    }

    public void PlayDefaultTransition()
    {
        if (_isTransitionRunning)
            StopCoroutine(_currentTransitionCoroutine);

        _currentTransitionCoroutine = StartCoroutine(TransitToColor(_defaultColor, _defaultTransitionTime));
    }

    private IEnumerator TransitToColor(Color endColor, float transitionTime)
    {
        _isTransitionRunning = true;
        Color startColor = _panMaterial.GetColor(_emissionPropertyID);
        Color resultColor = endColor;
        float currentTransitTime = 0;
        while (currentTransitTime < transitionTime)
        {
            Color tempColor = Color.Lerp(startColor, resultColor, currentTransitTime / transitionTime);
            _panMaterial.SetColor(_emissionPropertyID, tempColor);
            currentTransitTime += Time.deltaTime;
            yield return null;
        }

        _panMaterial.SetColor(_emissionPropertyID, resultColor);
        _isTransitionRunning = false;
    }
}
