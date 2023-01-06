using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

public class OSCLevelMeter : MonoBehaviour
{
    OscServer _server;

    public Transform meter;

    public float level = 0f;

    void Start()
    {
        _server = new OscServer(9000); // Port number

        _server.MessageDispatcher.AddCallback(
            "/level/1", // OSC address
            (string address, OscDataHandle data) => {
                var val = Mathf.Clamp(data.GetElementAsFloat(0), -60f, 0f);
                level = 1f - val / -60f;
            }
        );
    }

    void Update()
    {
        meter.localScale = new Vector3(1f, level, 1f);
    }

    void OnDestroy()
    {
        _server?.Dispose();
        _server = null;
    }
}
