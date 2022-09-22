using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Play();
        }
    }

    public void Play()
    {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody>().velocity = new Vector3(speed, speed, 0f);
    }
}
