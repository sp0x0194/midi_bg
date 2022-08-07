using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Guide : MonoBehaviour
{
    [SerializeField] private GameObject _guideObject;

    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            _guideObject.SetActive(!_guideObject.activeSelf);
        }
    }
}
