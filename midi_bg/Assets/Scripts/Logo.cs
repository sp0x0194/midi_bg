using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Logo : MonoBehaviour
{
    [SerializeField] private GameObject _logoObject;
    [SerializeField] private Kino.DigitalGlitch _digitalGlitch;
    [SerializeField] private Kino.AnalogGlitch _analogGlitch;

    [SerializeField] private LineRenderer _spectrumLine;
    [SerializeField] private GameObject[] _shakerObjects;
    [SerializeField] private GameObject _particleObject;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Reset();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Play();
        }
    }

    public void Reset()
    {
        _logoObject.SetActive(false);
    }

    public void Play()
    {
        StartCoroutine(_logoEffect());
    }

    private IEnumerator _logoEffect()
    {
        foreach (var obj in _shakerObjects)
        {
            obj.SetActive(false);
        }
        float duration = 3f;
        DOTween.To(() => _analogGlitch.colorDrift, (x) => _analogGlitch.colorDrift = x, 0.5f, duration).SetEase(Ease.OutCubic);
        DOTween.To(() => _analogGlitch.verticalJump, (x) => _analogGlitch.verticalJump= x, 0.5f, duration).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(2f);
        _logoObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _spectrumLine.enabled = false;
        _particleObject.SetActive(false);
        duration = 0.5f;
        DOTween.To(() => _analogGlitch.colorDrift, (x) => _analogGlitch.colorDrift = x, 0f, duration).SetEase(Ease.OutCubic);
        DOTween.To(() => _analogGlitch.verticalJump, (x) => _analogGlitch.verticalJump= x, 0f, duration).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(1.5f);
        duration = 1f;
        DOTween.To(() => _digitalGlitch.intensity, (x) => _digitalGlitch.intensity = x, 0.5f, duration).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(duration);
        _logoObject.SetActive(false);
        duration = 0.25f;
        DOTween.To(() => _digitalGlitch.intensity, (x) => _digitalGlitch.intensity = x, 0f, duration).SetEase(Ease.OutCubic);
    }
}
