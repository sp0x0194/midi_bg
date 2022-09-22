using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textMeshPro;

    public bool isActive = false;

    private float _time = 0f;

    void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (!Application.isPlaying) return;
            if (change != InputDeviceChange.Added) return;

            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillNoteOn += (note, velocity) => {
                isActive = true;
            };

            midiDevice.onWillNoteOff += (note) => {
            };
        };
    }

    void Update()
    {
        if (isActive)
        {
            _time += Time.deltaTime;
            textMeshPro.text = _time.ToString("0.0");
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _time = 0f;
            isActive = false;
            textMeshPro.text = _time.ToString("0.0");
        }
    }
}
