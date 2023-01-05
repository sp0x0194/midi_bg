using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lasp;

[RequireComponent(typeof(AudioLevelTracker))]
public class LevelMeter : MonoBehaviour
{
    internal readonly float MIDLV_THRESHOLD = 9 / 60f;
    internal readonly float LOWLV_THRESHOLD = 20 / 60f;

    public float width = 1.5f;
    public float height = 12.5f;
    public int resolution = 16;
    public float margin = 0.1f;

    public float level;

    public Material barMaterial;

    public Color highLevelColor;
    public Color midLevelColor;
    public Color lowLevelColor;

    public bool isInitialized { get; private set; }
    private GameObject[] bars;

    private AudioLevelTracker _audioLevelTracker;

    public void Start()
    {
        _audioLevelTracker = GetComponent<AudioLevelTracker>();
        Init();
    }

    public void Init()
    {
        if (bars != null)
        {
            foreach (var b in bars)
            {
                Destroy(b);
            }
        }
        bars = new GameObject[resolution];
        var barHeight = (height + margin) / resolution - margin;
        for (var i = 0; i < resolution; ++i)
        {
            var b = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(b.GetComponent<Collider>());
            var trans = b.transform;
            trans.SetParent(transform);
            trans.localEulerAngles = Vector3.zero;
            trans.localScale = new Vector3(width, barHeight, 1f);
            trans.localPosition = new Vector3(0f, height / 2f - barHeight / 2f - (barHeight + margin) * i, 0f);
            var mesh = b.GetComponent<MeshRenderer>();
            mesh.material = barMaterial;
            if ((i + 1f) / resolution < MIDLV_THRESHOLD)
            {
                mesh.material.color = highLevelColor;
            }
            else if ((i + 1f) / resolution < LOWLV_THRESHOLD)
            {
                mesh.material.color = midLevelColor;
            }
            else
            {
                mesh.material.color = lowLevelColor;
            }
            bars[i] = b;
        }
        isInitialized = true;
    }

    void Update()
    {
        if (!isInitialized) return;
        level = _audioLevelTracker.normalizedLevel;
        var barsLength = bars.Length;
        for (var i = 0; i < barsLength; ++i)
        {
            bars[i].SetActive((i + 1f) / barsLength > 1f - level);
        }
    }
}
