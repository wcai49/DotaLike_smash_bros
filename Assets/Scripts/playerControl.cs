using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControl : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction movement;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
}
