using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeatCube : MonoBehaviour
{
    public float releaseDuration = 0.5f;
    public Vector3 from;
    public Vector3 to;

    private Tweener _t;

    void Start()
    {
        transform.localScale = to;
    }

    public virtual void PlayOneShot()
    {
        if (!Application.isPlaying) return;
        if (_t != null) _t.Kill();
        transform.localScale = from;
        _t = transform.DOScale(to, releaseDuration).SetEase(Ease.OutCubic);
    }
}
