using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Guide : MonoBehaviour
{
    [SerializeField] private GameObject _guideObject;
    [SerializeField] private GameObject _focusWarningObject;

    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            _guideObject.SetActive(!_guideObject.activeSelf);
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        _focusWarningObject.SetActive(!hasFocus);
    }
}
