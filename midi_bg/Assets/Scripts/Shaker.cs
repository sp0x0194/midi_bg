using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using DG.Tweening;

public class Shaker : MonoBehaviour
{
    public int channel;
    public int note;

    public Transform shakeTarget;
    public float duration;
    // doscale
    public Vector3 defaultScale;
    public Vector3 targetScale;
    // shake
    public float maxRotation = -50f;
    public float minRotation = 0f;

    public int v = 1;
    public int r = 90;

    void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (!Application.isPlaying) return;
            if (change != InputDeviceChange.Added) return;

            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillNoteOn += (note, velocity) => {

                if (midiDevice.channel == channel && this.note == note.noteNumber)
                {
                    PlayOneShot();
                }
            };

            midiDevice.onWillNoteOff += (note) => {
            };
        };
    }

    public void PlayOneShot()
    {
        if (!Application.isPlaying) return;
        Debug.Log("play one shot");
        shakeTarget.localScale = targetScale;
        shakeTarget.DOScale(defaultScale, duration).SetEase(Ease.InQuart);
        // shakeTarget.DOShakeRotation(duration, new Vector3(0f, 0f, 45f), 1, 90);
        shakeTarget.DOShakePosition(duration, new Vector3(0.2f, 0.2f, 0f), v, r);
    }
}
