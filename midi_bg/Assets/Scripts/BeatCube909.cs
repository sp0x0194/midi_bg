using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeatCube909 : BeatCube
{
    private Tweener _t;
    [SerializeField] private MeshRenderer led;
    [SerializeField] private Material matLedOff;
    [SerializeField] private Material matLedOn;

    public float lightDuration = 0.1f;

    void Start()
    {
        transform.localScale = to;
    }

    public override void PlayOneShot()
    {
        if (!Application.isPlaying) return;
        base.PlayOneShot();
        StartCoroutine(_flahsLED());
    }

    private IEnumerator _flahsLED()
    {
        led.material = matLedOn;
        yield return new WaitForSeconds(lightDuration);
        led.material = matLedOff;
    }
}
