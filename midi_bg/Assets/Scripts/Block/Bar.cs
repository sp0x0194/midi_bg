using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

// ControlCallback.cs - This script shows how to define a callback to get
// notified on MIDI control change (CC) events.

sealed class Bar : MonoBehaviour
{
    public int channel;
    public int ccNumber;

    public float minX = -1f;
    public float maxX = 1f;

    void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change != InputDeviceChange.Added) return;

            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillControlChange += (cc, value) => {
                if (!Application.isPlaying) return;
                if (midiDevice.channel == channel && cc.controlNumber == ccNumber)
                {
                    transform.localPosition = new Vector3(
                        minX + (maxX - minX) * value,
                        transform.localPosition.y,
                        transform.localPosition.z);
                }
            };
        };
    }
}