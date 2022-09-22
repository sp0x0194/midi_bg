using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public class MIDIDebug : MonoBehaviour
{
    public bool log = true;

    void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (!Application.isPlaying) return;
            if (change != InputDeviceChange.Added) return;

            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillNoteOn += (note, velocity) => {
                if (log)
                    Debug.Log(string.Format("ch:{0} note:{1}", midiDevice.channel, note.noteNumber));
            };

            midiDevice.onWillNoteOff += (note) => {
            };
        };
    }
}

/*
Inst            Tx Note Number / Rx Note Number
BASS DRUM       36 / 35, 36
SNARE DRUM      38 / 38, 40
HAND CLAP       50 / 48, 50
TOM             47 / 45, 47
CLOSED HIHAT    42 / 42, 44
OPEN HIHAT      46 / 46
 */