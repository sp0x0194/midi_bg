using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using DG.Tweening;

public class Particle : MonoBehaviour
{
    [SerializeField] private GameObject _objectRoot;

    public bool isActive { get; private set; }

    public int channel;
    public int note;

    public Vector3 to;
    private Vector3 _defaultPosition;
    public float duration = 1f;

    public float angle = 15f;
    public float rotateDuration = 1f;

    private bool _r = false;

    void Start()
    {
        _defaultPosition = transform.localPosition;
        Stop();
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (!Application.isPlaying) return;
            if (change != InputDeviceChange.Added) return;

            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillNoteOn += (note, velocity) => {
                if (midiDevice.channel == channel && this.note == note.noteNumber)
                {
                    if (!isActive) Init();
                    PlayOneShot();
                }
            };

            midiDevice.onWillNoteOff += (note) => {
            };
        };
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Stop();
        }
    }

    public void Stop()
    {
        _objectRoot.SetActive(false);
        isActive = false;
    }

    public void Init()
    {
        _objectRoot.SetActive(true);
        isActive = true;
    }

    public void PlayOneShot()
    {
        if (!Application.isPlaying) return;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(to, duration / 2f).SetEase(Ease.OutQuart));
        seq.Append(transform.DOLocalMove(_defaultPosition, duration / 2f).SetEase(Ease.Linear));
        seq.PlayForward();

        transform.DOLocalRotate(new Vector3(0f, 0f, _r ? angle : -angle), rotateDuration, RotateMode.Fast).SetEase(Ease.OutQuart);
        _r = !_r;
    }
}
