using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using Lasp;

[RequireComponent(typeof(SpectrumAnalyzer), typeof(LineRenderer))]
public class SpectrumRenderer : MonoBehaviour
{
    public Gradient gradient;
    public Vector2 size;
    public bool vertical;
    public bool invert;
    private SpectrumAnalyzer _spectrumAnalyzer;
    private LineRenderer _lineRenderer;
    private Vector3[] _points;

    void Start()
    {
        _spectrumAnalyzer = GetComponent<SpectrumAnalyzer>();
        _points = new Vector3[_spectrumAnalyzer.resolution];
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _spectrumAnalyzer.resolution;
        _lineRenderer.colorGradient = gradient;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _lineRenderer.enabled = true;
        }
        for (var i = 0; i < _spectrumAnalyzer.resolution; ++i)
        {
            if (float.IsNaN(_spectrumAnalyzer.spectrumArray[i])) return;
            if (vertical)
            {
                _points[i] = new Vector3(
                    transform.position.x + (_spectrumAnalyzer.spectrumArray[i] < 0f ? 0f : _spectrumAnalyzer.spectrumArray[i]) * size.x - size.x / 2f,
                    transform.position.y + ((size.y / _spectrumAnalyzer.resolution * i) - size.y / 2f) * (invert ? -1 : 1),
                    transform.position.z);
            }
            else
            {
                _points[i] = new Vector3(
                    transform.position.x + (size.x / _spectrumAnalyzer.resolution * i) - size.x / 2f,
                    transform.position.y + (_spectrumAnalyzer.spectrumArray[i] < 0f ? 0f : _spectrumAnalyzer.spectrumArray[i]) * size.y - size.y / 2f,
                    transform.position.z);
            }
        }
        _lineRenderer.SetPositions(_points);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, size.y, 0f));
    }
}