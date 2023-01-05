using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchObjects : MonoBehaviour
{
    public GameObject[] objects;
    public int index = 0;

    void Start()
    {
        if (objects == null || objects.Length == 0) return;
        index = 0;
        _refresh();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Next();
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Prev();
        }
    }

    public void Next()
    {
        ++index;
        if (index >= objects.Length) index = 0;
        _refresh();
    }

    public void Prev()
    {
        --index;
        if (index < 0) index = objects.Length - 1;
        _refresh();
    }

    private void _refresh()
    {
        for (var i = 0; i < objects.Length; ++i)
        {
            objects[i].SetActive(i == index);
        }
    }
}
